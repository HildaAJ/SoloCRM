using Microsoft.Data.SqlClient;
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
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get customer by ID on Create page
        /// </summary>
        public async Task<IEnumerable<CustomerViewModel>> GetCustomersOnCreatedByAsync(int UserId, string searchTerm = "")
        {
            var cvmLst = new List<CustomerViewModel>();

            try
            {
                var customers = _context.Customers.Where(x => x.UserId == UserId);

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

                if (customers.Count() > 0)
                {
                    foreach (var item in customers)
                    {
                        var vm = new CustomerViewModel();
                        vm.Id = item.Id;
                        vm.Name = item.Name;
                        vm.Tel = item.Tel ?? "";
                        vm.Note = item.Note ?? "";
                        vm.Email = item.Email ?? "";
                        vm.State = item.State ?? "";
                        vm.Status = item.Status.GetDisplayName();
                        vm.CurrentTeamId = item.CurrentTeamId;
                        vm.CreatedBy = item.CreatedBy;
                        vm.CreatedAt = item.CreatedAt;
                        vm.MetWhen = item.MetWhen;
                        vm.MetWhere = item.MetWhere ?? "";
                        vm.Note = item.Note ?? "";

                        cvmLst.Add(vm);
                    }

                }

                return cvmLst;
            }
            catch (SqlException sqlEx)
            {

                return cvmLst;
            }
            catch (Exception ex)
            {

                return cvmLst;
            }
           
        }


        /// <summary>
        /// Get customer by ID on Edit page
        /// </summary>
        public async Task<CustomerEditViewModel?> GetCustomersOnEditByIdAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer == null)
                    return null;
                else
                {
                    string isClient = await _context.PurchaseRecord.CountAsync(x => x.CustomerId == id && x.Status == "Approved") > 0 ?"Yes":"No";

                    var vm = new CustomerEditViewModel()
                    {
                        Id = id,
                        Name = customer.Name ?? string.Empty,
                        Tel = customer.Tel ?? string.Empty,
                        Email = customer.Email ?? string.Empty,
                        State = customer.State ?? string.Empty,
                        MetWhere = customer.MetWhere ?? string.Empty,
                        Status = customer.Status,
                        MetWhen = customer.MetWhen,
                        Note = customer.Note ?? string.Empty,
                        TeamMember = customer.CurrentTeamId == null ? "No" : "Yes",
                        CreateDate = customer.CreatedAt,
                        UpdateDate = customer.UpdateDate,
                        Client = isClient,
                        
                    };
                    return vm;
                }
            }
            catch (SqlException sqlEx)
            {

                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Get customer by ID
        /// </summary>
        public async Task<CustomerDetailViewModel?> GetDetailByIdAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer == null)
                    return null;
                else
                {
                    int purchaseCnt =await _context.PurchaseRecord.CountAsync(x => x.CustomerId == id && x.Status == "Approved");
                    var purchaseLst = new List<PurchaseRecordViewModel>();

                    bool isClient = false;
                    if (purchaseCnt > 0)
                    {
                        //Show 5 latest records
                        var PurchaseRecords = await _context.PurchaseRecord
                                            .Where(x => x.CustomerId == id && x.Status == "Approved")
                                            .Where(x => !_context.CancelProductRecord.Any(c => c.PurchaseId == x.Id && c.Status == "Approved"))
                                            .OrderByDescending(x=>x.UpdateDate)
                                            .Take(5)
                                            .ToListAsync();

                        if (PurchaseRecords.Count > 0) 
                        {
                            isClient = true;
                            foreach (var record in PurchaseRecords) 
                            {
                                var purchase = new PurchaseRecordViewModel()
                                {
                                    ApplyDate = record.ApplyDate,
                                    ProductName = record.ProductName,
                                    SumAssured = record.SumAssured,
                                    Fees = record.Fees,
                                    Years = record.Years,
                                    CreatedAt = record.CreatedAt
                                };
                                purchaseLst.Add(purchase);

                            }
                        }
                    }

                    var followUpLst=new List<FollowUpRecordViewModel>();
                    var followUps=await _context.FollowUpRecord
                                        .Where(x => x.CustomerId == id)
                                        .OrderByDescending(x => x.UpdateDate)
                                        .Take(5)
                                        .ToListAsync();
                    if (followUps.Count > 0)
                    {
                        foreach (var item in followUps)
                        {
                            var followup = new FollowUpRecordViewModel()
                            {
                                FollowUpType=item.FollowUpType,
                                NextFollowUpDate = item.NextFollowUpDate,
                                Note=item.Note,
                                CreatedAt=item.CreatedAt
                            };

                            followUpLst.Add(followup);
                        }
                    }
                       
                    var vm = new CustomerDetailViewModel()
                    {
                        Id = id,
                        Name = customer.Name ?? string.Empty,
                        Tel = customer.Tel ?? string.Empty,
                        Email = customer.Email ?? string.Empty,
                        State = customer.State ?? string.Empty,
                        MetWhere = customer.MetWhere ?? string.Empty,
                        Status = customer.Status.GetDisplayName(),
                        MetWhen = customer.MetWhen,
                        Note = customer.Note ?? string.Empty,
                        TeamMember = customer.CurrentTeamId == null ? "No" : "Yes",
                        CreateDate = customer.CreatedAt,
                        UpdateDate = customer.UpdateDate,
                        Client = isClient ? "Yes" : "No",
                        PurchaseRecords= purchaseLst,
                        FollowUpRecords= followUpLst
                    };
                    return vm;
                }
            }
            catch (SqlException sqlEx)
            {

                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
            
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

        public async Task UpdateOnEditAsync(CustomerEditViewModel customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customer.Id);

                if (existingCustomer == null)
                {
                    throw new InvalidOperationException($"Customer with ID {customer.Id} not found");
                }

                _context.Entry(existingCustomer).State = EntityState.Modified;//forced to modifiy

                existingCustomer.Name = customer.Name;
                existingCustomer.Tel = customer.Tel;
                existingCustomer.Email = customer.Email;
                existingCustomer.State = customer.State;
                existingCustomer.MetWhere = customer.MetWhere;
                existingCustomer.Status = customer.Status;
                existingCustomer.MetWhen = customer.MetWhen;
                existingCustomer.Note = customer.Note;
                existingCustomer.UpdateDate = DateTime.Now;

                var result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    throw new InvalidOperationException("Failed to update customer in database");
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("The customer was modified by another user. Please refresh and try again.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Database error occurred: {ex.Message}", ex);
            }

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
