namespace LapTopStore_Client.Models
{
    public class CustomerLoginResponseData
    {
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? CustomerUsername { get; set;}
    }
}
