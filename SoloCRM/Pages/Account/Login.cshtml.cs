using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SoloCRM.Data;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using SoloCRM.Services;
using Microsoft.AspNetCore.Authorization;


namespace SoloCRM.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        //private readonly AppDbContext _context;
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public LoginInput Input { get; set; } = new();

        public string ReturnUrl { get; set; } = string.Empty;

        public void OnGet(string? returnUrl = null)
        {
            // Prevent infinite redirect loops by checking if returnUrl points to login page
            if (!string.IsNullOrEmpty(returnUrl) &&
                !returnUrl.Contains("/Account/Login", StringComparison.OrdinalIgnoreCase))
            {
                ReturnUrl = returnUrl;
            }
            else
            {
                ReturnUrl = Url.Content("~/");
            }
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            // Prevent infinite redirect loops by checking if returnUrl points to login page
            if (!string.IsNullOrEmpty(returnUrl) &&
                !returnUrl.Contains("/Account/Login", StringComparison.OrdinalIgnoreCase))
            {
                ReturnUrl = returnUrl;
            }
            else
            {
                ReturnUrl = Url.Content("~/");
            }

            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUserAsync(Input.Account, Input.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Account),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("FullName", user.FullName),
                        new Claim("AgentCode", user.AgentCode)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = Input.RememberMe,
                        ExpiresUtc = Input.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(24)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return LocalRedirect(ReturnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid account or password");
            }

            return Page();
        }


    }

    public class LoginInput
    {
        [Required(ErrorMessage = "Account is required")]
        [MaxLength(50, ErrorMessage = "Account cannot exceed 50 characters")]
        public string Account { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
