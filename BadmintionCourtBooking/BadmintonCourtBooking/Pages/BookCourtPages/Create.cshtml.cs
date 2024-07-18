using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Badminton.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPages.Pages.BookCourtPages
{
    public class CreateModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;

        public CreateModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CourtId"] = new SelectList(_context.Courts, "CourtId", "CourtId");
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
