using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.DataAccess.Models.ViewModel.CustomerViewModel
{
    public class AddStaffToCourtDTO
    {
        public string StaffId { get; set; } = null!;
        public Guid CourtId { get; set; }
    }
}
