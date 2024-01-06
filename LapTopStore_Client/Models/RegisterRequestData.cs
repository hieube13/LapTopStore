using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LapTopStore_Client.Models
{
    public class RegisterRequestData
    {
        //[Display(Name = "Tên tài khoản")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "Phải nhập tên tài khoản")]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "{0} phải có độ dài từ {2} đến {1}")]
        public string CustomerUserName { get; set; }

        //[Display(Name = "Tên đầy đủ")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "Phải nhập tên đầy đủ")]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "{0} phải có độ dài từ {2} đến {1}")]
        public string CustomerFullName { get; set; }

        //[Required(ErrorMessage = "Phải nhập {0}")]
        //[Phone(ErrorMessage = "Sai định dạng sđt")]
        //[Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; }

        //[Required(ErrorMessage = "Phải nhập {0}")]
        //[EmailAddress(ErrorMessage = "Sai định dạng email")]
        //[Display(Name = "Email")]
        public string CustomerEmail { get; set; }

        //[Required(ErrorMessage = "Chưa nhập password")]
        //[StringLength(100, ErrorMessage = " {0} phải từ {2} đến {1} kí tự", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Mật khẩu")]
        public string CustomerPassword { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Xác nhận lại mật khẩu")]
        //[Compare("Password", ErrorMessage = "Nhập lại pass chưa chính xác")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Tên đầy đủ")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "Phải nhập tên đầy đủ")]
        //[StringLength(200, MinimumLength = 6, ErrorMessage = "{0} phải có độ dài từ {2} đến {1}")]
        public string CustomerAddress { get; set; }
    }
}
