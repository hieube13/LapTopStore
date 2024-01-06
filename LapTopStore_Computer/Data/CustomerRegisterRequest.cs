using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data
{
    public class CustomerRegisterRequest
    {
        public string CustomerUserName { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CustomerAddress { get; set; }
    }
}
