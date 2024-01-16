using LapTopStore_Computer.Data.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data.VNPAY
{
    public class PaySuccessModel
    {
        public int OrderID { get; set; }
        public string BankCode { get; set; }
        public List<CartProduct> list_products { get; set; }
    }
}
