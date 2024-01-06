namespace LapTopStore_Client.Models.Product
{
    public class ProductWithCategory
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public DateTime ProductCreated { get; set; }
        public int CategoryId { get; set; }
        public double ProductDiscount { get; set; }
        public int? ProductInOrder { get; set; }
        public int? ProductInStock { get; set; }
        public bool HomeFlag { get; set; }
        public bool BestSeller { get; set; }
        public string? PrImage1 { get; set; }
        public string? PrImage2 { get; set; }
        public string? PrImage3 { get; set; }
        public string? PrImage4 { get; set; }
        public string? CategoryName { get; set; }
    }
}
