
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoloCRM.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using SoloCRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace SoloCRM.Pages.Customers
{
    public class IndexModel : PageModel
    {
        //private readonly AppDbContext _context;

        private readonly ICustomerService _customerService;

        public IndexModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// List of customer view models to display
        /// </summary>
        public IEnumerable<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();

        /// <summary>
        /// Search term for filtering customers
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public string CurrentUserAccount { get; set; }
        /// <summary>
        /// Get customers created by current user on page load
        /// </summary>
        /// <param name="searchTerm">Optional search parameter</param>
        public async Task OnGetAsync(string searchTerm = "")
        {
            SearchTerm = searchTerm;
            // Get current user's account
            CurrentUserAccount = GetCurrentUserAccount();

            if (string.IsNullOrEmpty(CurrentUserAccount))
            {
                // If no user is logged in, return empty list
                Customers = new List<CustomerViewModel>();
                return;
            }

            try
            {
                // Get customers created by current user
                Customers = await _customerService.GetCustomersByCreatedByAsync(CurrentUserAccount, SearchTerm);
            }
            catch (Exception ex)
            {
                // Log error and return empty list
                // Consider using ILogger here
                Customers = new List<CustomerViewModel>();

                // Optionally set an error message
                TempData["ErrorMessage"] = "An error occurred while loading customers.";
            }
        }

        
        private string GetCurrentUserAccount()
        {
            // Get current login user Account
            var userAccount = User.Identity?.Name ?? "System";
            return userAccount;
            //return User.FindFirst("Account")?.Value ?? "System";
        }
    }

    /// <summary>
    /// Customer view model for displaying customer data in the UI
    /// </summary>
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Phone")]
        public string Tel { get; set; } = string.Empty;

        [Display(Name = "State")]
        public string State { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public string Status { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Note")]
        public string Note { get; set; } = string.Empty;

        [Display(Name = "Met Where")]
        public string MetWhere { get; set; } = string.Empty;

        [Display(Name = "Met When")]
        public DateTime? MetWhen { get; set; }

        /// <summary>
        /// Current team ID if customer is a team member
        /// </summary>
        public int? CurrentTeamId { get; set; }

        /// <summary>
        /// Indicates if customer is a team member based on CurrentTeamId
        /// </summary>
        [Display(Name = "Team Member")]
        public string IsTeamMember => CurrentTeamId.HasValue ? "Yes" : "No";

        /// <summary>
        /// User account who created this customer record
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Creation timestamp
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
