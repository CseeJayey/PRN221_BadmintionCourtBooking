using System;
using System.Collections.Generic;

namespace Badminton.DataAccess.Models
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public string RoleId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
