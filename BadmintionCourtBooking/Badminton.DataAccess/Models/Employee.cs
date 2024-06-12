using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Employee
    {
        public string EmployeeId { get; set; } = null!;
        public int? RoleId { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? Dob { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Role? Role { get; set; }
    }
}
