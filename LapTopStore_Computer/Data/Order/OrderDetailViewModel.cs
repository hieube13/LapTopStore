using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data.Order
{
    public class OrderDetailViewModel
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public float ProductPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public string ProductName { get; set; }
    }
}
