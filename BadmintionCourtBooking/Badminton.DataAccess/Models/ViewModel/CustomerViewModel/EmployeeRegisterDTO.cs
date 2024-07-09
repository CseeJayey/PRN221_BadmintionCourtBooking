using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.DataAccess.Models.ViewModel.CustomerViewModel
{
    public class EmployeeRegisterDTO
    {
        [Required]
        public string EmployeeId { get; set; } = null!;
        [Required]
        public string RoleId { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
