using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class StaffCourt
    {
        public string StaffId { get; set; } = null!;
        public Guid CourtId { get; set; }

        public virtual Court Court { get; set; } = null!;
        public virtual Employee Staff { get; set; } = null!;
    }
}
