using Badminton.DataAccess.Models.ViewModel.CustomerViewModel;
using Badminton.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCourtBooking.Pages.BookCourtPages
{
    public class CourtBookModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;
        public CourtBookModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public BookCourtDTO BookCourtDTO { get; set; }

        public IActionResult OnGet()
        {
            // Initialize any data needed for the form here
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ensure the court exists
            var court = _context.Courts.FirstOrDefault(c => c.CourtId == BookCourtDTO.CourtId);
            if (court == null)
            {
                ModelState.AddModelError(string.Empty, "Court not found.");
                return Page();
            }

            // Ensure the customer exists
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == BookCourtDTO.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "Customer not found.");
                return Page();
            }

            // Create a new booking
            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                CustomerId = BookCourtDTO.CustomerId,
                CourtId = BookCourtDTO.CourtId,
                TimeSlotId = BookCourtDTO.TimeSlotId,
                BookingType = BookCourtDTO.BookingType,
                TotalHours = BookCourtDTO.TotalHours,
                StartTime = BookCourtDTO.StartTime,
                EndTime = BookCourtDTO.EndTime,
                BookingDate = BookCourtDTO.BookingDate,
                Status = BookCourtDTO.Status ?? "Pending", // Default status
                PeopleCount = BookCourtDTO.PeopleCount,
                CreatedAt = DateTime.Now,
                PhoneNumber = BookCourtDTO.PhoneNumber,
                ReservedDuration = BookCourtDTO.ReservedDuration,
                IsDeleted = false
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index"); // Redirect to a success page or wherever appropriate
        }
    }
}
