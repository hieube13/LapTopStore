using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AspNetCoreHero.ToastNotification.Abstractions;
using LapTopStore_Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace LapTopStore_Admin.Controllers
{
    public class UserController : Controller
    {

        public readonly INotyfService _notyfService;

        public UserController( INotyfService notyfService)
        {
            _notyfService= notyfService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult NotiAddInfo()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register_Admin(RegisterRequestData requestData)
        {
            var url = "https://localhost:7019";

            var baseUrl = "/Register";

            string josnData = JsonConvert.SerializeObject(requestData);

            var data_from_server = LapTopStore_Common.PostmanTools.WebPost(url, baseUrl, josnData);

            if(data_from_server == null)
            {
                return NotFound();
            }

            var result = JsonConvert.DeserializeObject<RegisterResponseData>(data_from_server);

            return Json(result);
        }

        [HttpPost]
        public IActionResult Login_Admin(LoginRequestData requestData)
        {
            var url = "https://localhost:7019";

            var baseUrl = "/Login";

            string josnData = JsonConvert.SerializeObject(requestData);

            var data_from_server = LapTopStore_Common.PostmanTools.WebPost(url, baseUrl, josnData);

            if (data_from_server == null)
            {
                return NotFound();
            }

            var result = JsonConvert.DeserializeObject<LoginResponseData>(data_from_server);

            return Json(result);
        }

        [HttpGet]
        public IActionResult UserDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserDetailsChange(UserChangeRequestData requestData)
        {
            var returnData = new UserChangeResponseData();

            try
            {
                
                var url = "https://localhost:7019";

                var baseUrl = "/UserDetailsChange";

                string josnData = JsonConvert.SerializeObject(requestData);

                var data_from_server = LapTopStore_Common.PostmanTools.WebPostImage(url, baseUrl, josnData);

                if (data_from_server == null)
                {
                    return NotFound();
                }

                var result = JsonConvert.DeserializeObject<UserChangeResponseData>(data_from_server);

                return Json(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
