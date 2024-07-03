using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Court
    {
        public Court()
        {
            Bookings = new HashSet<Booking>();
        }

        public Guid CourtId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
