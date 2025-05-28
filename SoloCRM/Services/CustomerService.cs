using Microsoft.EntityFrameworkCore;
using SoloCRM.Data;
using SoloCRM.EFModels;
using SoloCRM.Pages.Account;
using SoloCRM.Pages.Customers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoloCRM.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomersByCreatedByAsync(string createdBy, string searchTerm = "")
        {
            var customers= _context.Customers.Where(x=>x.CreatedBy==createdBy);

            // Apply search filter if search term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTermLower = searchTerm.Trim().ToLower();

                customers = customers.Where(c =>
                    // Search in Name
                    (c.Name != null && c.Name.ToLower().Contains(searchTermLower)) ||
                    // Search in Tel
                    (c.Tel != null && c.Tel.ToLower().Contains(searchTermLower)) ||
                    // Search in Email
                    (c.Email != null && c.Email.ToLower().Contains(searchTermLower)) ||
                    // Search in Note
                    (c.Note != null && c.Note.ToLower().Contains(searchTermLower)) ||
                    // Search in MetWhere
                    (c.MetWhere != null && c.MetWhere.ToLower().Contains(searchTermLower))
                );
            }

            await customers.OrderByDescending(c => c.CreatedAt).ToListAsync();

            var cvmLst = new List<CustomerViewModel>();
            if (customers.Count()>0) 
            {
                foreach (var item in customers) 
                {
                    var vm = new CustomerViewModel();
                    vm.Name = item.Name;
                    vm.Tel = item.Tel??"";
                    vm.Note = item.Note ?? "";
                    vm.Email = item.Email ?? "";
                    vm.State = item.State ?? "";
                    vm.Status= item.Status.GetDisplayName();
                    vm.CurrentTeamId=item.CurrentTeamId;
                    vm.CreatedBy = item.CreatedBy;
                    vm.CreatedAt=item.CreatedAt;
                    vm.MetWhen = item.MetWhen;
                    vm.MetWhere = item.MetWhere ?? "";
                    vm.Note = item.Note ?? "";

                    cvmLst.Add(vm);
                }
            
            }

            return cvmLst;
        }


        /// <summary>
        /// Get customer by ID
        /// </summary>
        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        public async Task<Customer> CreateAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        /// <summary>
        /// Update existing customer
        /// </summary>
        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }

    }


    public static class CustomerStatusExtensions
    {
        public static int ToInt(this CustomerStatus status)
        {
            return (int)status;
        }

        public static CustomerStatus ToCustomerStatus(this int value)
        {
            return (CustomerStatus)value;
        }

        public static string GetDisplayName(this CustomerStatus status)
        {
            var field = status.GetType().GetField(status.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? status.ToString();
        }
    }
}
