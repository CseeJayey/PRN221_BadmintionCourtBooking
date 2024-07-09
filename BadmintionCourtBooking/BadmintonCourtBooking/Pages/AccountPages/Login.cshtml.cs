using Badminton.DataAccess.Models;
using Badminton.DataAccess.Models.ViewModel.CustomerViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BadmintonCourtBooking.Pages.AccountPages
{
    public class LoginModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;

        public LoginModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public AccountLoginDTO Input { get; set; } = default!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
               return Page();
            }

            // Check if the user is a customer
            var customer = await _context.Customers
                .SingleOrDefaultAsync(c => c.Username == Input.Username);

            if (customer != null && BCrypt.Net.BCrypt.Verify(Input.Password, customer.PasswordHash))
            {
                // Successful customer login
                var customerClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Customer"),
                    new Claim(ClaimTypes.Email, customer.Username),
                    new Claim("CustomerId", customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Role, "Customer")
                };

                var customerClaimsIdentity = new ClaimsIdentity(customerClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(customerClaimsIdentity));

                return RedirectToPage("/CourtPages/Index");
            }

            // Check if the user is an employee
            var employee = await _context.Employees
                .SingleOrDefaultAsync(e => e.Username == Input.Username);

            if (employee != null && BCrypt.Net.BCrypt.Verify(Input.Password, employee.PasswordHash))
            {
                // Successful employee login
                var employeeClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.FullName),
                    new Claim(ClaimTypes.Email, employee.Username),
                    new Claim("EmployeeId", employee.EmployeeId),
                    new Claim(ClaimTypes.Role, employee.RoleId == "1" ? "Staff" : "Manager")
                };

                var employeeClaimsIdentity = new ClaimsIdentity(employeeClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(employeeClaimsIdentity));

                // Set cookie with username
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    HttpOnly = true,
                    Secure = true
                };
                Response.Cookies.Append("Username", employee.Username, cookieOptions);

                if (employee.RoleId == "1")
                {
                    return RedirectToPage("/CustomerPages/Index");
                }
                else if (employee.RoleId == "2")
                {
                    return RedirectToPage("/EmployeePages/Index");
                }
            }

            // If login attempt fails
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return Page();
        }
    }
}
