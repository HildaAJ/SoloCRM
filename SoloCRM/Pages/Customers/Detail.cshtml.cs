using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoloCRM.EFModels;
using SoloCRM.Services;

namespace SoloCRM.Pages.Customers
{
    public class DetailModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public DetailModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Customer ID parameter from route or query string
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        /// <summary>
        /// Customer detail view model containing all display information
        /// </summary>
        public CustomerDetailViewModel Customer { get; set; } = new();

        /// <summary>
        /// Error message to display if customer not found or search fails
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Success message for operations
        /// </summary>
        public string? SuccessMessage { get; set; }

        /// <summary>
        /// Handles GET requests - loads customer data by ID parameter
        /// </summary>
        /// <returns>Page result</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Load customer by ID parameter
                if (Id > 0)
                {
                    var customerData = await _customerService.GetDetailByIdAsync(Id);
                    if (customerData != null)
                    {
                        // Map entity to view model
                        Customer = customerData;
                    }
                    else
                    {
                        ErrorMessage = $"Customer with ID {Id} not found.";
                    }
                }
                else
                {
                    ErrorMessage = "Invalid customer ID provided.";
                }

                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while loading customer information.";
                // Log the exception here
                return Page();
            }
        }

        
    }


    /// <summary>
    /// ViewModel for displaying customer detail information
    /// </summary>
    public class CustomerDetailViewModel
    {
        /// <summary>
        /// Customer unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer full name
        /// </summary>
        [Display(Name = "Customer Name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Customer telephone number
        /// </summary>
        [Display(Name = "Telephone")]
        public string Tel { get; set; } = string.Empty;

        /// <summary>
        /// Customer email address
        /// </summary>
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Customer location state
        /// </summary>
        [Display(Name = "State")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Location or method where the customer was first met
        /// </summary>
        [Display(Name = "Met Where")]
        public string MetWhere { get; set; } = string.Empty;

        /// <summary>
        /// Current customer status (Active, Inactive, Prospect, etc.)
        /// </summary>
        [Display(Name = "Status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Date when the customer was first met
        /// </summary>
        [Display(Name = "Met When")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MetWhen { get; set; }

        /// <summary>
        /// Additional notes about the customer
        /// </summary>
        [Display(Name = "Notes")]
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Team member responsible for this customer
        /// </summary>
        [Display(Name = "Team Member")]
        public string TeamMember { get; set; } = string.Empty;

        /// <summary>
        /// Date when the customer record was created
        /// </summary>
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Date when the customer record was last updated
        /// </summary>
        [Display(Name = "Updated Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Client company or organization the customer belongs to
        /// </summary>
        [Display(Name = "Client")]
        public string Client { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if customer information was found
        /// </summary>
        public bool IsFound => Id > 0;

        /// <summary>
        /// Gets formatted display text for status with appropriate styling class
        /// </summary>
        public string StatusDisplayClass => Status?.ToLower() switch
        {
            "following" => "badge bg-success",
            "inactive" => "badge bg-secondary",
            _ => "badge bg-primary"
        };

        public List<FollowUpRecordViewModel>? FollowUpRecords { get; set; }

        public List<PurchaseRecordViewModel>? PurchaseRecords { get; set; }
    }

    public class FollowUpRecordViewModel()
    {
        /// <summary>
        /// Type of follow-up (e.g., call, email, meeting)
        /// </summary>
        public string FollowUpType { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled date for next follow-up (nullable)
        /// </summary>
        public DateTime? NextFollowUpDate { get; set; }

        /// <summary>
        /// Notes about the follow-up, up to 1000 characters
        /// </summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Record creation timestamp, defaults to current date/time
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    public class PurchaseRecordViewModel
    {
        // <summary>
        /// Date of application (no time part)
        /// </summary>
        public DateTime ApplyDate { get; set; }
        /// <summary>
        /// Name of the product purchased
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Sum assured, nullable
        /// </summary>
        public decimal? SumAssured { get; set; }

        /// <summary>
        /// Fees associated with the purchase
        /// </summary>
        public decimal Fees { get; set; }

        /// <summary>
        /// Number of years, nullable
        /// </summary>
        public int? Years { get; set; }

        /// <summary>
        /// Record creation timestamp, default to current date/time
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
