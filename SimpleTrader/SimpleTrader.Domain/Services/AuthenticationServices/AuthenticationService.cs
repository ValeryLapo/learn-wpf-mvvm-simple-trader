using System;
using System.Runtime.InteropServices;
using SimpleTrader.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;
        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            //we need to
            //1. Get the user's account from the database
            //2. Hash the password provided in the method
            //3. Compare the hashed provided password to the database hashed password

            Account storedAccount = await _accountService.GetByUsername(username);

            if (storedAccount == null)
            {
                throw new UserNotFoundException(username);
            }

            PasswordVerificationResult passwordResult =
                _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }

            Account emailAccount = await _accountService.GetByEmail(email);
            if (emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            Account usernameAccount = await _accountService.GetByUsername(username);
            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                //the reason why are we using this nuget package is because we just don't reenventing the wheel.
                string hashedPassword = _passwordHasher.HashPassword(password);

                User user = new User()
                {
                    Email = email,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DateJoined = DateTime.Now
                };

                Account account = new Account()
                {
                    AccountHolder = user,
                };

                await _accountService.Create(account);
            }


            return result;
        }
    }
}
