using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class ManagerCourt
    {
        public string ManagerId { get; set; } = null!;
        public string CourtId { get; set; } = null!;

        public virtual Court Court { get; set; } = null!;
        public virtual Employee Manager { get; set; } = null!;
    }
}
