
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class OrderDetail
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double? ProductPrice { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public double? Total { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
