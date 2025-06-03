using SoloCRM.EFModels;
using SoloCRM.Pages.Customers;

namespace SoloCRM.Services
{
    /// <summary>
    /// Service interface for customer operations
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get customers created by specific user with optional search filtering
        /// </summary>
        /// <param name="userId">User account who created the customers</param>
        /// <param name="searchTerm">Optional search term for filtering</param>
        /// <returns>List of customer view models</returns>
        Task<IEnumerable<CustomerViewModel>> GetCustomersByCreatedByAsync(int userId, string searchTerm = "");

        /// <summary>
        /// Get single customer by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer view model or null if not found</returns>
        Task<Customer?> GetByIdAsync(int id);

        /// <summary>
        /// Get single customer by ID - Customer Detail Page
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer view model or null if not found</returns>
        Task<CustomerDetailViewModel?> GetDetailByIdAsync(int id);
        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="customer">Customer view model</param>
        /// <returns>Created customer view model</returns>
        Task<Customer> CreateAsync(Customer customer);

        /// <summary>
        /// Update existing customer
        /// </summary>
        /// <param name="customer">Customer view model with updates</param>
        /// <returns>Updated customer view model</returns>
        Task<Customer> UpdateAsync(Customer customer);


    }
}

