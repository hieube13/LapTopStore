using LapTopStore_Client.Models.ShoppingCart;
using LapTopStore_Client.Models.VNPAY;
using LapTopStore_Computer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Globalization;
using LapTopStore_Common;
using LapTopStore_Client.Models;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace LapTopStore_Client.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IConfiguration _configuration { get; set; }
        public LapTopStoreContext _context { get; set; }
        public ShoppingCartController(IConfiguration configuration, LapTopStoreContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult VnpayReturn()
        {
            var returnData = new GenericResponse();

            if (HttpContext.Request.Query.Count > 0)
            {
                //string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; 
                var vnPaySettings = _configuration.GetSection("VnPaySettings");
                string vnp_HashSecret = vnPaySettings.GetValue<string>("Vnp_HashSecret"); //Chuoi bi mat

                var vnpayData = HttpContext.Request.Query;

                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (var queryParameter in vnpayData)
                {
                    string key = queryParameter.Key;
                    string value = queryParameter.Value;

                    // Lấy tất cả dữ liệu query string
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, value);
                    }
                }


                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = HttpContext.Request.Query["vnp_SecureHash"];
                String TerminalID = HttpContext.Request.Query["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = HttpContext.Request.Query["vnp_BankCode"];
                string orderIDString = Convert.ToString(vnpay.GetResponseData("vnp_OrderInfo"));
                int orderID = 0;

                Match match = Regex.Match(orderIDString, @"\d+");

                if (match.Success)
                {
                    // Chuyển giá trị số từ chuỗi thành kiểu int
                    orderID = Convert.ToInt32(match.Value);

                    Console.WriteLine("OrderID: " + orderID);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy giá trị số trong chuỗi.");
                }

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var jsonData = new PaySuccessModel();
                        var url = "https://localhost:7019/api";

                        var list_product = new List<CartProduct>();
                        var cart = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"] : string.Empty;
                        list_product = JsonConvert.DeserializeObject<List<CartProduct>>(HttpUtility.UrlDecode(cart));

                        jsonData.OrderID = orderID;
                        jsonData.BankCode = bankCode;
                        jsonData.list_products = list_product;

                        var jsonData1 = JsonConvert.SerializeObject(jsonData);

                        var baseUrl = "/ShoppingCart/UpdateAfterPaySuccess";

                        var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData1);

                        returnData = JsonConvert.DeserializeObject<GenericResponse>(data_from_server);
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);

                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    //displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.ThanhToanThanhCong = "Số tiền thanh toán (VND):" + (vnp_Amount / 100).ToString("C0");
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                }
            }

            Response.Cookies.Delete("MyShoppingCart");
            //var a = UrlPayment(0, "DH3574");
            return View(returnData);
        }

        public IActionResult ShopCart()
        {
            var list_product = new List<CartProduct>();
            try
            {
                var cart = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"] : string.Empty;
                list_product = JsonConvert.DeserializeObject<List<CartProduct>>(HttpUtility.UrlDecode(cart));
            }
            catch (Exception)
            {

                throw;
            }

            return View(list_product);
        }

        public IActionResult Checkout()
        {
            var list_product = new List<CartProduct>();
            try
            {
                var cart = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"] : string.Empty;
                list_product = JsonConvert.DeserializeObject<List<CartProduct>>(HttpUtility.UrlDecode(cart));
            }
            catch (Exception)
            {

                throw;
            }

            return View(list_product);
        }

        [HttpPost]
        public IActionResult CheckoutPost(OrderViewModel requestData)
        {
            var returnOrder = new GenericResponse();

            //var CartShop = new List<CartProduct>();
            //var cart = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"] : string.Empty;
            //CartShop = JsonConvert.DeserializeObject<List<CartProduct>>(HttpUtility.UrlDecode(cart));
            
            var url = "https://localhost:7019/api";

            //add order
            var baseUrl1 = "/ShoppingCart/AddOrder";

            var jsonData1 = JsonConvert.SerializeObject(requestData);

            var data_from_server1 = PostmanTools.WebPost(url, baseUrl1, jsonData1);

            returnOrder = JsonConvert.DeserializeObject<GenericResponse>(data_from_server1);
            

            return Json(returnOrder);
        }

        
    }
}
