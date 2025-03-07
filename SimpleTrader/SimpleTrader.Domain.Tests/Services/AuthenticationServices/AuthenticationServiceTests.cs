﻿using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{


    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IPasswordHasher<string>> _mockPasswordHasher;
        private Mock<IAccountService> _mockAccountService;
        private AuthenticationService _authenticationService;

        //gonna happen before the tests
        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher<string>>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);

        }


        //[Method]_[Scenario]_[Return]
        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            //This is unit test, not integration test so we will make mocks   for constructor

            //Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";

            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            //Since we are not testing password hashing, we mock/pre-define the password hash result.
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);


            //Act
            Account account = await _authenticationService.Login(expectedUsername, password);

            //Assert
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            //This is unit test, not integration test so we will make mocks   for constructor

            //Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";

            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account()
            { AccountHolder = new User() { Username = expectedUsername } });

            //Since we are not testing password hashinh, we mock/pre-define the password hash result.

            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Failed);

            //Act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            //Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            //This is unit test, not integration test so we will make mocks   for constructor

            //Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";


            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Failed);

            //Act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            //Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            string password = "testpassword";
            string confirmPassword = "confirmPassword";

            RegistrationResult expected = RegistrationResult.PasswordDoNotMatch;

            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(),
                password, confirmPassword);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            string email = "test@gmail.com";
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());

            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;

            RegistrationResult actual = await _authenticationService.Register(email, It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExists()
        {
            string username = "testuser";
            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());

            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;

            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), username,
                It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPassword_returnsSuccess()
        {
            RegistrationResult expected = RegistrationResult.Success;

            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }
    }

}
