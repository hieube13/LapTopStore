using LapTopStore_Client.Models;
using LapTopStore_Client.Models.Product;
using LapTopStore_Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LapTopStore_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var result = new getListPrForHomeRes();

            var url = "https://localhost:7019/api";

            var baseUrl = "/Product/GetListProductForHome";

            var data_from_server = PostmanTools.WebGet(url, baseUrl);

            result = JsonConvert.DeserializeObject<getListPrForHomeRes>(data_from_server);

            return View(result);
        }

    }
}