using Badminton.DataAccess.Models;
using Badminton.DataAccess.Models.ViewModel.CustomerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BadmintonCourtBooking.Pages.AccountPages
{
    public class RegisterCourtStaffModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;
        
        public RegisterCourtStaffModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public EmployeeRegisterDTO EmployeeRegisterDTO { get; set; } = default!;
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var existingEmployee = await _context.Employees
            .AnyAsync(e => e.Username == EmployeeRegisterDTO.Username);

            if (existingEmployee)
            {
                // Return an error message indicating that the username is already taken
                ModelState.AddModelError(string.Empty, "Username is already taken.");
                return Page();
            }

            //var employee = new Employee()
            //{

            //    EmployeeId = Guid.NewGuid().ToString(),
            //    RoleId = "1",
            //    FullName = EmployeeRegisterDTO.FullName,
            //    Username = EmployeeRegisterDTO.Username,
            //    PasswordHash = BCrypt.Net.BCrypt.HashPassword(EmployeeRegisterDTO.PasswordHash),
            //    Dob = EmployeeRegisterDTO.Dob,
            //    PhoneNumber = EmployeeRegisterDTO.PhoneNumber,
            //    Status = "ACTIVE",
            //    CreatedDate = DateTime.Now,
            //    IsDeleted = false
            //};
            //_context.Employees.Add(employee);
            //await _context.SaveChangesAsync();
            //return RedirectToPage("./Login");
            string newEmployeeId;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Retrieve the highest current EmployeeId
                    var lastEmployeeIdStr = await _context.Employees
                        .OrderByDescending(e => e.EmployeeId)
                        .Select(e => e.EmployeeId)
                        .FirstOrDefaultAsync();

                    int lastEmployeeId = 0;

                    if (!string.IsNullOrEmpty(lastEmployeeIdStr))
                    {
                        // Remove the "E" prefix and parse the remaining part as an integer
                        lastEmployeeId = int.Parse(lastEmployeeIdStr.Substring(1));
                    }

                    // Increment the employee ID
                    newEmployeeId = "E" + (lastEmployeeId + 1);

                    // Create the new employee with the incremented ID
                    var employee = new Employee()
                    {
                        EmployeeId = newEmployeeId,
                        RoleId = "1",
                        FullName = EmployeeRegisterDTO.FullName,
                        Username = EmployeeRegisterDTO.Username,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(EmployeeRegisterDTO.PasswordHash),
                        Dob = EmployeeRegisterDTO.Dob,
                        PhoneNumber = EmployeeRegisterDTO.PhoneNumber,
                        Status = "ACTIVE",
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };

                    _context.Employees.Add(employee);
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
