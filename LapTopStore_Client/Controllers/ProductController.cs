using LapTopStore_Client.Models.Product;
using LapTopStore_Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LapTopStore_Client.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int? id)
        {
            var result = new ProductDetailAndRelated();

            var url = "https://localhost:7019/api";

            var baseUrl = "/Product/GetProductDetailsUser";

            var jsonData = id.ToString();

            var data_from_server = PostmanTools.WebGetDelete(url, baseUrl, jsonData);

            result = JsonConvert.DeserializeObject<ProductDetailAndRelated>(data_from_server);

            return View(result);
        }
    }
}
