namespace LapTopStore_Admin.Models
{
    public class CreateProductRequestData
    {
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public double ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public double ProductDiscount { get; set; }
        public int? ProductInOrder { get; set; }
        public int? ProductInStock { get; set; }
        public int? AttrId { get; set; }
        public int? PrImage1 { get; set; }
        public string? PrImage2 { get; set; }
        public string? PrImage3 { get; set; }
        public string? PrImage4 { get; set; }
        public bool HomeFlag { get; set; }
        public bool BestSeller { get; set; }
    }
}
