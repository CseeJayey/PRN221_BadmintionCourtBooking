using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Payment
    {
        public string PaymentId { get; set; } = null!;
        public string? BookingId { get; set; }
        public string? CustomerId { get; set; }
        public int? SumTotal { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
