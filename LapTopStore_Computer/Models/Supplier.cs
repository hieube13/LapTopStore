using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int SuppilerId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierDescription { get; set; }
        public string? SuppilerAddress { get; set; }
        public string? SuppilerCity { get; set; }
        public string? SuppilerCountry { get; set; }
        public string? SuppilerEmail { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
