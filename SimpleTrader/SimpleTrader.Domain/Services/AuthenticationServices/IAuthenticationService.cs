using System.Threading.Tasks;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<bool> Register(string email, string username, string password, string confirmPassword);
        Task<Account> Login(string username, string password);
    }
}
