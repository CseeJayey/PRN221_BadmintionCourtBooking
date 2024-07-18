using Badminton.DataAccess.Models.ViewModel.CustomerViewModel;
using Badminton.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCourtBooking.Pages.PaymentPages
{
    public class PaymentProcessModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;
        public PaymentProcessModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ProcessPaymentDTO ProcessPaymentDTO { get; set; }

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

            // Ensure the booking exists
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == ProcessPaymentDTO.BookingId);
            if (booking == null)
            {
                ModelState.AddModelError(string.Empty, "Booking not found.");
                return Page();
            }

            // Ensure the customer exists
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == ProcessPaymentDTO.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "Customer not found.");
                return Page();
            }

            // Create a new payment record
            var payment = new Payment
            {
                PaymentId = ProcessPaymentDTO.PaymentId,
                BookingId = ProcessPaymentDTO.BookingId,
                CustomerId = ProcessPaymentDTO.CustomerId,
                SumTotal = ProcessPaymentDTO.SumTotal,
                Status = ProcessPaymentDTO.Status ?? "Pending",
                PaymentMethod = ProcessPaymentDTO.PaymentMethod,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index"); // Redirect to a success page or wherever appropriate
        }
    }
}
