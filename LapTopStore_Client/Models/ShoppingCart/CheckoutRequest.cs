namespace LapTopStore_Client.Models.ShoppingCart
{
    public class CheckoutRequest
    {
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public float TotalPrice { get; set; }
        public int Status { get; set; }
    }
}
