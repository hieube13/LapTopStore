
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Attribute
    {
        public Attribute()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int AttrId { get; set; }
        public string? AttrName { get; set; }
        public string? AttrValue { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
