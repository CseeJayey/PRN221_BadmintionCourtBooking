using Badminton.DataAccess.Models;
using Badminton.DataAccess.Models.ViewModel.CustomerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCourtBooking.Pages.BookCourtPages
{
    public class StaffToCourtModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;
        public StaffToCourtModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public AddStaffToCourtDTO StaffToCourtDTO { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ensure the court exists
            var court = _context.Courts.FirstOrDefault(c => c.CourtId == StaffToCourtDTO.CourtId);
            if (court == null)
            {
                ModelState.AddModelError(string.Empty, "Court not found.");
                return Page();
            }

            // Ensure the staff exists
            var staff = _context.Employees.FirstOrDefault(e => e.EmployeeId == StaffToCourtDTO.StaffId);
            if (staff == null)
            {
                ModelState.AddModelError(string.Empty, "Staff not found.");
                return Page();
            }

            // Create a new StaffCourt assignment
            var staffCourt = new StaffCourt
            {
                StaffId = StaffToCourtDTO.StaffId,
                CourtId = StaffToCourtDTO.CourtId,
                Court = court,
                Staff = staff
            };

            _context.StaffCourts.Add(staffCourt);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Success"); // Redirect to a success page or wherever appropriate
        }
    }
}
