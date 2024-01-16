namespace LapTopStore_Admin.Models.Order
{
    public class FilterOrderModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        public int? Status { get; set; }
        public int? PaymentType { get; set; }
        public string? paymentTypeVN { get; set; }
    }
}
