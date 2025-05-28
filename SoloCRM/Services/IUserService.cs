using SoloCRM.EFModels;
using SoloCRM.Pages.Account;

namespace SoloCRM.Services
{
    public interface IUserService
    {
        Task<AppUser?> GetUserByAccountAsync(string account);
        Task<AppUser?> ValidateUserAsync(string account, string password);
        Task<bool> CreateUserAsync(RegisterInput model);
        Task<bool> IsAccountExistsAsync(string account);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
