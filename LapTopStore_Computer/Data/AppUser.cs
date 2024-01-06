using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data
{
    public class AppUser : IdentityUser
    {
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Sex { get; set; }
        public string? Image { get; set; }
    }
}
