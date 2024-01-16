using LapTopStore_Admin.Models.Order;
using LapTopStore_Common;
using LapTopStore_Computer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PagedList.Core;
using System.Drawing.Printing;
using System.Linq.Expressions;
using System.Web;

namespace LapTopStore_Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index(int page = 1)
        {

            var pageNumber = page;
            var pageSize = 10;

            var list_order = new List<OrderViewModel>();

            var url = "https://localhost:7019/api";

            var baseUrl = "/Order/GetAllOrder";

            //var jsonData = JsonConvert.SerializeObject(requestData);

            var data_from_server = PostmanTools.WebPost1(url, baseUrl);

            list_order = JsonConvert.DeserializeObject<List<OrderViewModel>>(data_from_server);

            PagedList<OrderViewModel> models = new PagedList<OrderViewModel>(list_order.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;

            return View(models);

        }

        public IActionResult FilterIndex(FilterOrderModel requestData, int page = 1)
        {
            var filterArr = Request.Cookies["FilterOrder"] != null ? Request.Cookies["FilterOrder"] : string.Empty;
            requestData = JsonConvert.DeserializeObject<FilterOrderModel>(HttpUtility.UrlDecode(filterArr));

            var pageNumber = page;
            var pageSize = 10;

            var list_order = new List<OrderViewModel>();

            var url = "https://localhost:7019/api";

            var baseUrl = "/Order/FilterOrderAPI";

            var jsonData = JsonConvert.SerializeObject(requestData);

            var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData);

            list_order = JsonConvert.DeserializeObject<List<OrderViewModel>>(data_from_server);

            PagedList<OrderViewModel> models = new PagedList<OrderViewModel>(list_order.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;

            return View(models);

        }

        public IActionResult Detail(int id)
        {
            var result = new OrderAndDetails();
            var url = "https://localhost:7019/api";

            var baseUrl = "/Order/GetOrderForDetail";

            var jsonData = id.ToString();

            var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData);

            result = JsonConvert.DeserializeObject<OrderAndDetails>(data_from_server);

            return View(result);
        }

        //[HttpPost]
        //public IActionResult FilterOrder(FilterOrderModel requestData)
        //{
        //    var result = new List<OrderViewModel>();

        //    var url = "https://localhost:7019/api";

        //    var baseUrl = "/Order/FilterOrderAPI";

        //    var jsonData = JsonConvert.SerializeObject(requestData);

        //    var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData);

        //    result = JsonConvert.DeserializeObject<List<OrderViewModel>>(data_from_server);

        //    PagedList<OrderViewModel> models = new PagedList<OrderViewModel>(result.AsQueryable(), pageNumber, pageSize);

        //    return View(result);
        //}
    }
}
