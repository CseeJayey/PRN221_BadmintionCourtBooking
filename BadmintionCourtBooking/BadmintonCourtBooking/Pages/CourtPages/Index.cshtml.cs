using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Badminton.DataAccess.Models;

namespace BadmintonCourtBooking.Pages.CourtPages
{
    public class IndexModel : PageModel
    {
        private readonly Badminton.DataAccess.Models.BadmintonManagmentDBContext _context;

        public IndexModel(Badminton.DataAccess.Models.BadmintonManagmentDBContext context)
        {
            _context = context;
        }

        public IList<Court> Court { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Courts != null)
            {
                Court = await _context.Courts.ToListAsync();
            }
        }
    }
}
