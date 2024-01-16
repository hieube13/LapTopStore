namespace LapTopStore_Client.Models.ShoppingCart
{
    public class OrderViewModel
    {
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public string? TotalPrice { get; set; }
        public int? Status { get; set; }
        public string? PaymentType { get; set; }
        public string? PaymentTypeVN { get; set; }
        public DateTime? PaymentDate { get; set; }

        public List<CartProduct> listProducts { get; set; }
    }
}
