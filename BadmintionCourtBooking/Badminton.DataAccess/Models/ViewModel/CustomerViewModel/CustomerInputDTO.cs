﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.DataAccess.Models.ViewModel.CustomerViewModel
{
    public class CustomerInputDTO
    {
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
    }
}
