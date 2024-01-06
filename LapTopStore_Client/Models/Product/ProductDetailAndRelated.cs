namespace LapTopStore_Client.Models.Product
{
    public class ProductDetailAndRelated
    {
        public ProductDetails product { get; set; }
        public List<ProductDetails> listRelated { get; set; }
    }
}
