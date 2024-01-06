using LapTopStore_Admin.Data;
using LapTopStore_Common;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LapTopStore_Admin.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public IActionResult FindProduct(string keyword, int catID)
        {
            List<ProductWithCategory> ls = new List<ProductWithCategory>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            
            var url = "https://localhost:7019/api";
            var baseUrl = "/Product/GetListProductSearch"; 
            
            var jsonData = JsonConvert.SerializeObject(new { Keyword = keyword, CatID = catID });

            var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData);

            ls = JsonConvert.DeserializeObject<List<ProductWithCategory>>(data_from_server);

            if (ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }

        }
    }
}
