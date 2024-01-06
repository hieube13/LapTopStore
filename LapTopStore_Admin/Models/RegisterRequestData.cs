using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LapTopStore_Admin.Models
{
    public class RegisterRequestData
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Phải nhập tên tài khoản")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} phải có độ dài từ {2} đến {1}")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Chưa nhập password")]
        [StringLength(100, ErrorMessage = " {0} phải từ {2} đến {1} kí tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phải nhập {0}")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Nhập lại pass chưa chính xác")]
        public string ConfirmPassword { get; set; }

        //public string UserName { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }
        //public string ConfirmPassword { get; set; }




    }
}
