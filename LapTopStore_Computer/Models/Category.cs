
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CategoryDescription { get; set; } = null!;
        public string? CategoryPicture1 { get; set; }

    }
}
