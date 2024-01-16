namespace LapTopStore_Client.Models.ShoppingCart
{
    public class CartProduct
    {
        public int? OrderID { get; set; } 
        public int ProductId { get; set; } 
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public string ProductDiscount { get; set; }
        public DateTime ProductCreated { get; set; }
        public int ProductInStock { get; set; }
        public bool HomeFlag { get; set; }
        public bool BestSeller { get; set; }
        public string PrImage1 { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
    }
}
