namespace LapTopStore_Media.Models
{
    public class MediaEditProduct
    {
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }

        public Dictionary<string, string> ImageNames { get; set; } 
    }
}
