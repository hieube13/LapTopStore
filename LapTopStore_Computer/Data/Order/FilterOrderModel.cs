using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data.Order
{
    public class FilterOrderModel
    {
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? PaymentType { get; set; }
        public string? paymentTypeVN { get; set; }
    }
}
