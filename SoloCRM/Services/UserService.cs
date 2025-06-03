using Microsoft.EntityFrameworkCore;
using SoloCRM.Data;
using SoloCRM.EFModels;
using SoloCRM.Pages.Account;
using System.Security.Cryptography;
using System.Text;

namespace SoloCRM.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetUserByAccountAsync(string account)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Account == account && u.IsActive);
        }

        public async Task<AppUser?> ValidateUserAsync(string account, string password)
        {
            var user = await GetUserByAccountAsync(account);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        public async Task<bool> CreateUserAsync(RegisterInput model)
        {
            if (await IsAccountExistsAsync(model.Account) || await IsAgentCodeExistsAsync(model.AgentCode))
            {
                return false;
            }

            var user = new AppUser
            {
                Account = model.Account,
                FullName = model.FullName,
                AgentCode = model.AgentCode,
                PasswordHash = HashPassword(model.Password),
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsAccountExistsAsync(string account)
        {
            return await _context.Users.AnyAsync(u => u.Account == account);
        }

        public async Task<bool> IsAgentCodeExistsAsync(string code)
        {
            return await _context.Users.AnyAsync(u => u.AgentCode == code);
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "YourSaltHere"));
            return Convert.ToBase64String(hashedBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }
    }

}
