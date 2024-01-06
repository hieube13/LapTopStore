using LapTopStore_API.Data;
using LapTopStore_API.Model;
using LapTopStore_Common;
using LapTopStore_Computer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text;
using System.Text.Encodings.Web;

namespace LapTopStore_API.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAdminRequestData requestData)
        {
            var returnData = new ReturnData();

            var user = new AppUser { UserName = requestData.UserName, Email = requestData.Email };
            var result = await _userManager.CreateAsync(user, requestData.Password);

            if (!result.Succeeded)
            {
                returnData.ResponseCode = -1;
                returnData.Messenger = "kHÔNG TẠO ĐC TÀI KHOẢN";
                return Ok(returnData);
            }

            user.EmailConfirmed = true;
            await _signInManager.SignInAsync(user, isPersistent: false);
            returnData.ResponseCode = 1;
            returnData.Messenger = "Tạo tài khoản thành công";
            returnData.Email = user.Email;
            returnData.UserName = user.UserName;

            return Ok(returnData);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAdminRequestData requestData)
        {
            if(ModelState.IsValid)
            {
                var returnData = new LoginAdminResponseData();
                var user = await _userManager.FindByEmailAsync(requestData.Email);
                var result = await _signInManager.PasswordSignInAsync(user.UserName , requestData.Password, true ,lockoutOnFailure: true);

                if(!result.Succeeded)
                {
                    returnData.ResponseCode = -1;
                    returnData.Messenger = "Đăng nhập thất bại";
                }

                if(result.Succeeded)
                {
                    returnData.ResponseCode = 1;
                    returnData.Messenger = "Đăng nhập thành công";
                }
                if (result.IsLockedOut)
                {
                    returnData.ResponseCode = -2;
                    returnData.Messenger = "Tài khoản bị tạm khoá";
                }

                returnData.ResponseCode = 1;
                returnData.Messenger = "Đăng nhập thành công";
                returnData.UserName = user.UserName;
                returnData.Avatar = user.Image;
                returnData.Email = user.Email;
                returnData.Phone = user.Phone;
                returnData.Sex = user.Sex;
                returnData.Address = user.Address;
                return Ok(returnData);
            }

            return BadRequest();
        }

        [HttpPost("UserDetailsChange")]
        public async Task<IActionResult> UserDetailsChange([FromBody] UserChangeRequestData requestData)
        {
            var returnData = new UserChangeResponseData();

            try
            {

                var user = await _userManager.FindByEmailAsync(requestData.Email);

                if (user != null)
                {
                    user.UserName = requestData.UserName;
                    user.Phone = requestData.Phone;
                    user.Address = requestData.Address;
                    user.Sex = requestData.Sex;

                    if (requestData.ImageChange == 1)
                    {
                        await Task.Delay(300);

                        var urlMedia = "https://localhost:7190/api";
                        var baseUrl = "/UploadFile/UploadUserImage";

                        var secretKey = _config["Security:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
                        var sign = LapTopStore_Common.Securiry.MD5Hash(requestData.Base64Image + "|" + secretKey);

                        requestData.Sign = sign;

                        var jsonData = JsonConvert.SerializeObject(requestData);

                        var resultData = PostmanTools.WebPost(urlMedia, baseUrl, jsonData);

                        if (resultData != null)
                        {
                            var rs = JsonConvert.DeserializeObject<MediaAPIToAPI>(resultData);
                            requestData.Base64Image = rs.Messenger;
                        }

                        user.Image = requestData.Base64Image;
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        returnData.ResponseCode = -1;
                        returnData.Messenger = "Không cập nhật được dữ liệu";
                    }

                    returnData.UserName = user.UserName;
                    returnData.Phone = user.Phone;
                    returnData.Address = user.Address;
                    returnData.Sex = user.Sex;
                    returnData.Base64Image = user.Image;
                    returnData.ResponseCode = 1;
                    returnData.Messenger = "Cập nhật thành công";

                    return Ok(returnData);
                }

                returnData.ResponseCode = 1;
                returnData.Messenger = "Cập nhật thành công";
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
           
            return Ok(returnData);
        }

    }
}
