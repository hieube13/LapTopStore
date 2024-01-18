using System.Security;

namespace LapTopStore_API.Data
{
    public class CustomerLoginResponseData
    {
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }
        public string? Accesstoken { get; set; }
        public string? RefreshToken { get; set; }
        public string? CustomerUsername { get; set; }
    }
}
