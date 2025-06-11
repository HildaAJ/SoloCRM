using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoloCRM.Data;
using SoloCRM.EFModels;
using SoloCRM.Services;
using static SoloCRM.Services.ICustomerService;

namespace SoloCRM.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ICustomerService _customerService;
        public CreateModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public CreateCustomerViewModel CustomerViewModel { get; set; } = default!;
        
        // Dropdown list for Status
        public SelectList StatusList { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Initialize dropdown list for Status
            StatusList = new SelectList(Enum.GetValues(typeof(CustomerStatus))
                .Cast<CustomerStatus>()
                .Select(s => new { Value = (int)s, Text = s.ToString() }),
                "Value", "Text");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(GetCurrentUserID()))
            {
                // Reload dropdown list if validation fails
                StatusList = new SelectList(Enum.GetValues(typeof(CustomerStatus))
                    .Cast<CustomerStatus>()
                    .Select(s => new { Value = (int)s, Text = s.ToString() }),
                    "Value", "Text");
                return Page();
            }

            // Convert ViewModel to Database Model
            var customer = new Customer
            {
                // Map from ViewModel
                Name = CustomerViewModel.Name,
                Tel = CustomerViewModel.Tel,
                State = CustomerViewModel.State,
                Status = CustomerViewModel.Status,
                Email = CustomerViewModel.Email,
                Note = CustomerViewModel.Note,

                // Set hidden fields to default/null values
                MetWhere = CustomerViewModel.MetWhere,
                CurrentTeamId = null,
                MetWhen = CustomerViewModel.MetWhen,

                // Set system fields
                CreatedBy = GetCurrentUserAccount(),
                CreatedAt = DateTime.Now,
                UpdatedBy = GetCurrentUserAccount(),
                UpdateDate = DateTime.Now,
                UserId = Convert.ToInt32(GetCurrentUserID())

            };

            await _customerService.CreateAsync(customer);

            return RedirectToPage("./Create");
        }

        private string GetCurrentUserAccount()
        {
            // Get current login user Account
            var userAccount = User.Identity?.Name?? "System";
            return userAccount;
            //return User.FindFirst("Account")?.Value ?? "System";
        }

        private string GetCurrentUserID()
        {
            // Get current login user Account
            //var userAccount = User.Identity?.Name ?? "System";
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            return userId;
            //return User.FindFirst("Account")?.Value ?? "System";
        }
    }

    public class CreateCustomerViewModel
    {
        public CreateCustomerViewModel()
        {
            Status = CustomerStatus.Following; // 預設第一個
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Customer Name")]
        public string Name { get; set; } = string.Empty;

       
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        [Display(Name = "Phone")]
        public string? Tel { get; set; } 

        [StringLength(50)]
        [Display(Name = "State")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public CustomerStatus Status { get; set; }


        [EmailAddress(ErrorMessage = "Please enter a valid email format")]
        [StringLength(200)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(500)]
        [Display(Name = "Note")]
        public string? Note { get; set; }

        [StringLength(100)]
        [Display(Name = "Met Where")]
        public string? MetWhere { get; set; }

        [Display(Name = "Current Team ID")]
        public int? CurrentTeamId { get; set; }

        [Display(Name = "Met When")]
        [DataType(DataType.Date)]
        public DateTime? MetWhen { get; set; }

        // System fields - not displayed in UI

        [StringLength(50)]
        public string CreatedBy { get; set; } = string.Empty;

        [StringLength(50)]
        public string? UpdatedBy { get; set; }

         public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        
    }

    public enum CustomerStatus
    {
        [Display(Name = "Following")]
        Following = 1,

        [Display(Name = "Inactive")]
        Inactive = 2
    }
}
