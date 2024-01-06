
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string? OrderDate { get; set; }
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
        public int? PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
