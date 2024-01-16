
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapTopStore_Computer.Models
{
    public partial class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public double? ProductPrice { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public double? Total { get; set; }

    }
}
