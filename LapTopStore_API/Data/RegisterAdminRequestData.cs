using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LapTopStore_API.Data
{
    public class RegisterAdminRequestData
    {
        
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }
    }
}
