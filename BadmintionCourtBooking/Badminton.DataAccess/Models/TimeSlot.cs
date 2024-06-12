using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            Bookings = new HashSet<Booking>();
            Courts = new HashSet<Court>();
        }

        public int TimeSlotsId { get; set; }
        public string? CourtId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public virtual Court? Court { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Court> Courts { get; set; }
    }
}
