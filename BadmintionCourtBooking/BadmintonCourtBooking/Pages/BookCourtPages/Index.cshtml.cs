using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Badminton.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace RazorPages.Pages.BookCourtPages
{
    public class IndexModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;

        public IndexModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Booking = await _context.Bookings
                .Include(b => b.Court)
                .Include(b => b.Customer).ToListAsync();
        }
    }
}
