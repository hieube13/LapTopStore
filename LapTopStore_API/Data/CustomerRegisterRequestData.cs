namespace LapTopStore_API.Data
{
    public class CustomerRegisterRequestData
    {
        public string CustomerUserName { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CustomerAddress { get; set; }
    }
}
