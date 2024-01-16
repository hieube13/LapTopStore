using LapTopStore_API.Services;
using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.ShoppingCart;
using LapTopStore_Computer.Data.VNPAY;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System.Net.Http;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace LapTopStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public IStoreUnitOfWork _unitOfWork { get; set; }
        public IConfiguration _configuration { get; set; }
        public IWebHostEnvironment _webHost { get; set; }
        public IEmailSender _senEmail { get; set; }
        public ShoppingCartController(IStoreUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment webHost, IEmailSender sendEmai) 
        { 
            _unitOfWork= unitOfWork;
            _configuration= configuration;
            _webHost= webHost;
            _senEmail= sendEmai;
        }

        [HttpPost("GetOrderByID")]
        public IActionResult GetOrderByID(int? Id)
        {
            return Ok();
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderViewModel requestData)
        {
            var returnData = new GenericResponse();

            if (requestData == null)
            {
                returnData.ResponseCode = -1;
                returnData.Messenger = "SAIIIII";
                return Ok(returnData);
            }

            var result = await _unitOfWork._orderRepository.CreateOrderAndDetail(requestData);
            _unitOfWork.SaveChanges();

            //Send Mail cho khach hang
            var strSanPham = "";
            var thanhtien = decimal.Zero;
            var TongTien = decimal.Zero;
            foreach (var item in requestData.listProducts)
            {
                strSanPham += "<tr>";
                strSanPham += "<td>" + item.ProductName + "</td>";
                strSanPham += "<td>" + item.Quantity + "</td>";
                strSanPham += "<td>" + LapTopStore_Common.Securiry.FormatNumber(Int32.Parse(item.ProductPrice) * item.Quantity, 0) + "</td>";
                strSanPham += "</tr>";
                thanhtien += Int32.Parse(item.ProductPrice) * item.Quantity;
            }
            TongTien = thanhtien;
            string path = Path.Combine(_webHost.ContentRootPath, "Template", "send2.cshtml");
            string contentCustomer = System.IO.File.ReadAllText(path);
            contentCustomer = contentCustomer.Replace("{{MaDon}}", result.ToString());
            contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
            contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", "HieuBui");
            contentCustomer = contentCustomer.Replace("{{Phone}}", requestData.Phone);
            contentCustomer = contentCustomer.Replace("{{Email}}", requestData.Email);
            contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", requestData.Address + requestData.Ward + requestData.District + requestData.Province);
            contentCustomer = contentCustomer.Replace("{{ThanhTien}}", LapTopStore_Common.Securiry.FormatNumber(thanhtien, 0));
            contentCustomer = contentCustomer.Replace("{{TongTien}}", LapTopStore_Common.Securiry.FormatNumber(TongTien, 0));
            await _senEmail.SendEmailAsync(requestData.Email, "Đơn hàng #" + result.OrderId.ToString(), contentCustomer.ToString());

            var resultString = "";
            //thanh toán VNPAY
            if (Int32.Parse(requestData.PaymentType) == 2)
            {
                resultString = await UrlPayment(Int32.Parse(requestData.PaymentTypeVN), result.OrderId);
            }

            returnData.ResponseCode = result.OrderId;
            returnData.Messenger = resultString;
            return Ok(returnData);
        }

        [HttpPost("UpdateAfterPaySuccess")]
        public async Task<IActionResult> UpdateAfterPaySuccess(PaySuccessModel requestData)
        {
            var returnData = new GenericResponse();
            if(requestData == null)
            {
                returnData.ResponseCode = -1;
                returnData.Messenger = "";
            }

            await _unitOfWork._orderRepository.UpdateProductAfterPay(requestData);
            _unitOfWork.SaveChanges();

            returnData.ResponseCode = 1;
            returnData.Messenger = "Cập nhật thành công";

            return Ok(returnData);
        }


        private async Task<string> UrlPayment(int TypePaymentVN, int orderCode)
        {
            var urlPayment = "";
            var order = await _unitOfWork._orderRepository.GetOrderByID(orderCode);
            var tick = DateTime.Now.Ticks.ToString();
            //Get Config Info
            var vnPaySettings = _configuration.GetSection("VnPaySettings");

            string vnp_Url = vnPaySettings.GetValue<string>("Vnp_Url");
            string vnp_TmnCode = vnPaySettings.GetValue<string>("Vnp_TmnCode");
            string vnp_HashSecret = vnPaySettings.GetValue<string>("Vnp_HashSecret");
            string vnp_Returnurl = vnPaySettings.GetValue<string>("Vnp_Returnurl");

            VnPayLibrary vnpay = new VnPayLibrary();

            var Price = (long)order.TotalPrice * 100;

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            //vnpay.AddRequestData("vnp_OrderDate", order.OrderDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            var ipAddress = Utils.GetIpAddressTest(HttpContext);
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", tick); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
    }
}
