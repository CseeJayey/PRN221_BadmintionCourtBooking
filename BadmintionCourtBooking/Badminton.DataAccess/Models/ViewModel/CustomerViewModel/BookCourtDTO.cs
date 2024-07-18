using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.DataAccess.Models.ViewModel.CustomerViewModel
{
    public class BookCourtDTO
    {   
        public string CustomerId { get; set; } = null!;
        public Guid CourtId { get; set; }
        public int TimeSlotId { get; set; }
        public string? BookingType { get; set; }
        public string? TotalHours { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Status { get; set; }
        public int? PeopleCount { get; set; }
        public string? PhoneNumber { get; set; }
        public TimeSpan? ReservedDuration { get; set; }
    }
}
