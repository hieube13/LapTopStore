namespace LapTopStore_Client.Models
{
    public class RegisterResponseData
    {
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }
        public string CustomerUserName { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
    }
}
