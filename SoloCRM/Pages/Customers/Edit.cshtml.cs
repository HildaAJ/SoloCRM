using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoloCRM.EFModels;
using SoloCRM.Services;

namespace SoloCRM.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerService _customerService; 

        public EditModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public CustomerEditViewModel Customer { get; set; } = new();

        // Status dropdown options using enum
        public SelectList StatusOptions => new SelectList(
            Enum.GetValues<CustomerStatus>()
                .Select(e => new {
                    Value = (int)e,
                    Text = e.GetDisplayName()
                }),
            "Value", "Text", (int)Customer.Status);

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            // Load customer data from database
            Customer = await _customerService.GetCustomersOnEditByIdAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Update the UpdateDate to current time
                Customer.UpdateDate = DateTime.Now;

                // Set success message
                TempData["SuccessMessage"] = "Customer information updated successfully!";

                // Save changes to database
                await _customerService.UpdateOnEditAsync(Customer);
                // Redirect to customer list or details page
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Log the exception (you should implement proper logging here)
                // Logger.LogError(ex, "Failed to update customer with ID: {CustomerId}", Customer.Id);

                // Set error message for JavaScript alert
                TempData["ErrorMessage"] = $"Failed to update customer information. Error: {ex.Message}";
                return Page();
            }
        }

    }

    public class CustomerEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string Tel { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string? Email { get; set; } 

        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string? State { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Met Where cannot exceed 100 characters")]
        public string? MetWhere { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public CustomerStatus Status { get; set; } =CustomerStatus.Following;

        [DataType(DataType.Date)]
        public DateTime? MetWhen { get; set; }

        [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters")]
        public string? Note { get; set; } = string.Empty;

        // Read-only fields
        public string TeamMember { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Client { get; set; } = string.Empty;
    }
}
