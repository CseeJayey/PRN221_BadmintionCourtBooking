using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCourtBooking.Pages.BookCourtPages
{
    public class CourtCancelModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;
        public CourtCancelModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Guid BookingId { get; set; }

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

            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == BookingId);
            if (booking == null)
            {
                ModelState.AddModelError(string.Empty, "Booking not found.");
                return Page();
            }

            // Option 1: Mark the booking as canceled (soft delete)
            booking.IsDeleted = true;

            // Option 2: Remove the booking from the database (hard delete)
            // _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index"); // Redirect to a success page or appropriate page
        }
    }
}
