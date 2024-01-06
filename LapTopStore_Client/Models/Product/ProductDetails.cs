namespace LapTopStore_Client.Models.Product
{
    public class ProductDetails
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public float ProductDiscount { get; set; }
        public int CategoryId { get; set; }
        public int ProductInStock { get; set; }
        public bool HomeFlag { get; set; }
        public bool BestSeller { get; set; }
        public string PrImage1 { get; set; }
        public string PrImage2 { get; set; }
        public string PrImage3 { get; set; }
        public string PrImage4 { get; set; }
        public DateTime? ProductCreated { get; set; }
        public string Color { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Driver { get; set; }
        public string Screen { get; set; }
        public string System { get; set; }
        public string? CategoryName { get; set; }
    }
}
