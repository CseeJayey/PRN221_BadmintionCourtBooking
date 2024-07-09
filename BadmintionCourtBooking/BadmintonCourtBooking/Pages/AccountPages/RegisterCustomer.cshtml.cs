using Badminton.DataAccess.Models;
using Badminton.DataAccess.Models.ViewModel.CustomerViewModel;
using Badminton.DataAccess.Repository;
using Badminton.DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BadmintonCourtBooking.Pages.AccountPages
{
    public class RegisterCustomerModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;
        //private readonly IUserRepository _userRepository;
        public RegisterCustomerModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            //_userRepository = new UserRepository();
            _context = context;
        }
        [BindProperty]
        public CustomerRegisDTO Model { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //var customer = new Customer
            //{
            //    CustomerId = Guid.NewGuid().ToString(),
            //    FullName = Model.FullName,
            //    Username = Model.Username,
            //    PasswordHash = BCrypt.Net.BCrypt.HashPassword(Model.PasswordHash),
            //    Dob = Model.Dob,
            //    PhoneNumber = Model.PhoneNumber,
            //    Status = "ACITVE",
            //    CreatedDate = DateTime.Now,
            //    IsDeleted = false
            //};
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            //return RedirectToPage("./Login");

            var existingCustomer = await _context.Customers.AnyAsync(c => c.Username == Model.Username);

            if (existingCustomer)
            {
                // Return an error message indicating that the username is already taken
                ModelState.AddModelError(string.Empty, "Username is already taken.");
                return Page();
            }
            string newCustomerId;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Retrieve the highest current CustomerId
                    var lastCustomerIdStr = await _context.Customers
                        .OrderByDescending(c => c.CustomerId)
                        .Select(c => c.CustomerId)
                        .FirstOrDefaultAsync();

                    int lastCustomerId = 0;

                    if (!string.IsNullOrEmpty(lastCustomerIdStr))
                    {
                        // Remove the "C" prefix and parse the remaining part as an integer
                        lastCustomerId = int.Parse(lastCustomerIdStr.Substring(1));
                    }

                    // Increment the customer ID
                    newCustomerId = "C" + (lastCustomerId + 1);

                    // Create the new customer with the incremented ID
                    var customer = new Customer
                    {
                        CustomerId = newCustomerId,
                        FullName = Model.FullName,
                        Username = Model.Username,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(Model.PasswordHash),
                        Dob = Model.Dob,
                        PhoneNumber = Model.PhoneNumber,
                        Status = "ACTIVE",  // Corrected "ACITVE" to "ACTIVE"
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };

                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();

                    return RedirectToPage("./Login");
                }
                catch (Exception)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    throw;
                }
            }

        }
    }
}

