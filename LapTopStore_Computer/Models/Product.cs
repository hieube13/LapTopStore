using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
            Attrs = new HashSet<Attribute>();
        }

        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public DateTime ProductCreated { get; set; }
        public int CategoryId { get; set; }
        public double ProductDiscount { get; set; }
        public int? ProductInOrder { get; set; }
        public int? ProductInStock { get; set; }
        public int? SupplierId { get; set; }
        public string? PrImage1 { get; set; }
        public string? PrImage2 { get; set; }
        public string? PrImage3 { get; set; }
        public string? PrImage4 { get; set; }
        public int? ProductQuantity { get; set; }
        public bool HomeFlag { get; set; } = false;
        public bool BestSeller { get; set; } = false;

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Attribute> Attrs { get; set; }
    }
}
