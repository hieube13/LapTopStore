namespace LapTopStore_Admin.Models.Order
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
