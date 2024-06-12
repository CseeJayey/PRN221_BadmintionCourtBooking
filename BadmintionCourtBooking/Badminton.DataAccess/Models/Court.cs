using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Court
    {
        public Court()
        {
            TimeSlots = new HashSet<TimeSlot>();
        }

        public string CourtId { get; set; } = null!;
        public int? TimeSlotId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        public virtual TimeSlot? TimeSlot { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
