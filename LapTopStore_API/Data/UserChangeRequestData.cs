namespace LapTopStore_API.Data
{
    public class UserChangeRequestData
    {
        public string UserName { get; set; }
        public string Base64Image { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string? Sign { get; set; }
        public int ImageChange { get; set; }
        public string Email { get; set; }

    }
}
