using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }

        public string BookingId { get; set; } = null!;
        public string? CustomerId { get; set; }
        public int? TimeSlotId { get; set; }
        public string? BookingType { get; set; }
        public string? TotalHours { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? BookingDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual TimeSlot? TimeSlot { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
