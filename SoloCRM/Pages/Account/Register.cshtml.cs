using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoloCRM.Data;
using SoloCRM.EFModels;
using SoloCRM.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SoloCRM.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {

        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterInput Input { get; set; }

        public void OnGet()
        {
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _userService.IsAccountExistsAsync(Input.Account))
                {
                    ModelState.AddModelError(nameof(Input.Account), "This account already exists");
                    return Page();
                }

                var result = await _userService.CreateUserAsync(Input);
                if (result)
                {
                    TempData["SuccessMessage"] = "Registration successful, please login";
                    return RedirectToPage("/Account/Login");
                }

                ModelState.AddModelError(string.Empty, "Registration failed, please try again later");
            }

            return Page();
        }


    }

    public class RegisterInput
    {
        [Required(ErrorMessage = "Account is required")]
        [MaxLength(50, ErrorMessage = "Account cannot exceed 50 characters")]
        public string Account { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
