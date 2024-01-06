
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int PaymentId { get; set; }
        public string? PaymentType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
