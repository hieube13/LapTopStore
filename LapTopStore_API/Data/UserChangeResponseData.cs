﻿namespace LapTopStore_API.Data
{
    public class UserChangeResponseData
    {
        public string UserName { get; set; }
        public string Base64Image { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }
    }

    public class MediaAPIToAPI
    {
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }
    }
}