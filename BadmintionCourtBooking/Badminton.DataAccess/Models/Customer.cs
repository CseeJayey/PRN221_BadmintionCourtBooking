using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            Payments = new HashSet<Payment>();
        }

        public string CustomerId { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Dob { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
