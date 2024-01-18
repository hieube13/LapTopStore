using LapTopStore_Client.Models;
using LapTopStore_Client.Models.Product;
using LapTopStore_Common;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize]
        public IActionResult Detail(int? id, TokenModel model)
        {
            var result = new ProductDetailAndRelated();

            var url = "https://localhost:7019/api";

            var baseUrl = "/Product/GetProductDetailsUser";

            model.AccessToken = Request.Cookies["LapTopJwtToken"] != null ? Request.Cookies["LapTopJwtToken"] : string.Empty;
            model.RefreshToken = Request.Cookies["LapTopRefreshToken"] != null ? Request.Cookies["LapTopRefreshToken"] : string.Empty;

            ProductDetailModel requestData = new ProductDetailModel()
            {
                Id= id,
                AccessToken= model.AccessToken,
                RefreshToken= model.RefreshToken,
            };

            var jsonData = JsonConvert.SerializeObject(requestData);

            var data_from_server = PostmanTools.WebGetDelete(url, baseUrl, jsonData);

            result = JsonConvert.DeserializeObject<ProductDetailAndRelated>(data_from_server);

            if(result.ResponseCode == 13)
            {
                var newToken = new TokenModel();
                var baseUrl1 = "/Customer/RefreshToken";
                var jsonData1 = JsonConvert.SerializeObject(model);
                var newTokenFromServer = PostmanTools.WebPost(url, baseUrl1, jsonData1);

                newToken = JsonConvert.DeserializeObject<TokenModel>(newTokenFromServer);

                requestData.AccessToken = newToken.AccessToken;
                requestData.RefreshToken = newToken.RefreshToken;

                var cookieOptions = new CookieOptions
                {
                    // Thiết lập các thuộc tính của cookie, ví dụ như thời gian sống, đường dẫn, domain, etc.
                    Expires = DateTime.Now.AddDays(1), // Đặt thời gian sống của cookie (ở đây là 1 ngày)
                    HttpOnly = true, // Không cho JavaScript truy cập cookie
                };

                // Gán giá trị vào cookie
                Response.Cookies.Append("LapTopJwtToken", requestData.AccessToken, cookieOptions);
                Response.Cookies.Append("LapTopRefreshToken", requestData.RefreshToken, cookieOptions);


                jsonData = JsonConvert.SerializeObject(requestData);

                data_from_server = PostmanTools.WebGetDelete(url, baseUrl, jsonData);

                result = JsonConvert.DeserializeObject<ProductDetailAndRelated>(data_from_server);

                return View(result);
            }

            return View(result);
        }
    }
}
