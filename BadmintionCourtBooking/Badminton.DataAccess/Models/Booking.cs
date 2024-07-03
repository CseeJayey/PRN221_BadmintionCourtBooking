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

        public Guid BookingId { get; set; }
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
        public DateTime? CreatedAt { get; set; }
        public string? PhoneNumber { get; set; }
        public TimeSpan? ReservedDuration { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Court Court { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
