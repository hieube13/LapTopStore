using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data.Order
{
    public class GetAllOrder
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public double? TotalPrice { get; set; }
        public int? Status { get; set; }
        public string? PaymentType { get; set; }
        public string? PaymentTypeVN { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? CustomerUserName { get; set; }
    }
}
