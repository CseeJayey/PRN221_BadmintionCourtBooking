using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.DataAccess.Models.ViewModel.CustomerViewModel
{
    public class ProcessPaymentDTO
    {
        public string PaymentId { get; set; } = null!;
        public Guid BookingId { get; set; }
        public string CustomerId { get; set; } = null!;
        public int SumTotal { get; set; }
        public string Status { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
    }
}
