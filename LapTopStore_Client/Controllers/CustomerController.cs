using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Abstractions;
using LapTopStore_Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LapTopStore_Client.Controllers
{
    public class CustomerController : Controller
    {
        public readonly INotyfService _notyfService;

        public CustomerController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(RegisterRequestData requestData)
        {
            try
            {
                var returnData = new RegisterResponseData();

                if (requestData == null || 
                    string.IsNullOrEmpty(requestData.CustomerFullName))
                {
                    returnData.Messenger = "dữ liệu đầu vào không đc trống!";
                    return Json(returnData);
                }

                // CHECK DEUX LIÊU

                var url = "https://localhost:7019/api";

                var baseUrl = "/Customer/CustomerRegister";

                string josnData = JsonConvert.SerializeObject(requestData);

                requestData.CustomerPassword = LapTopStore_Common.Securiry.MD5Hash(requestData.CustomerPassword + "|" + "UxFkTt5siR5dibph8JdUIsixJ2mmhr");
                requestData.ConfirmPassword = LapTopStore_Common.Securiry.MD5Hash(requestData.ConfirmPassword + "|" + "UxFkTt5siR5dibph8JdUIsixJ2mmhr");

                var data_from_server = LapTopStore_Common.PostmanTools.WebPost(url, baseUrl, josnData);

                returnData = JsonConvert.DeserializeObject<RegisterResponseData>(data_from_server);

                return Json(returnData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult Login(CustomerLoginRequestData requestData)
        {
            var returnData = new CustomerLoginResponseData();

            var url = "https://localhost:7019/api";

            var baseUrl = "/Customer/Login";

            string josnData = JsonConvert.SerializeObject(requestData);

            var data_from_server = LapTopStore_Common.PostmanTools.WebPost(url, baseUrl, josnData);

            var result = JsonConvert.DeserializeObject<CustomerLoginResponseData>(data_from_server);

            if(result == null)
            {
                return Json(new { responseCode = -1, messenger = "đăng nhập thất bại" });
            }

            return Json(result);
        }
    }
}
