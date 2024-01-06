using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Models
{
    public class AspNetUserRole
    {
        [Key]
        public string UserId { get; set; }

     
        public string RoleId { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public AspNetUser User { get; set; }

        //[ForeignKey(nameof(RoleId))]
        //public AspNetRole Role { get; set; }
    }
}
