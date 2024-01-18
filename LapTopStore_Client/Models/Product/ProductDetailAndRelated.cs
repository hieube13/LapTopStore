namespace LapTopStore_Client.Models.Product
{
    public class ProductDetailAndRelated
    {
        public int ResponseCode { get; set; }
        public ProductDetails? product { get; set; }
        public List<ProductDetails>? listRelated { get; set; }
    }
}
