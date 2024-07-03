using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Employee
    {
        public string EmployeeId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
