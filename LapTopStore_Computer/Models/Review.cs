using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public DateTime? ReviewTime { get; set; }
        public int? StartRating { get; set; }

        public virtual Product? Product { get; set; }
    }
}
