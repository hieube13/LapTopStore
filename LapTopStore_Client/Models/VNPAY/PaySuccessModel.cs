using LapTopStore_Client.Models.ShoppingCart;

namespace LapTopStore_Client.Models.VNPAY
{
    public class PaySuccessModel
    {
        public int OrderID { get; set; }
        public string BankCode { get; set; }
        public List<CartProduct> list_products { get; set; }
    }
}
