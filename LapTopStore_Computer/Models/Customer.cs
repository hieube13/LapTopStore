
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapTopStore_Computer.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int CustomerId { get; set; }
        public string? CustomerUserName { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerImage { get; set; }
        public string? CustomerPhone { get; set; }
        public bool? Status { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
