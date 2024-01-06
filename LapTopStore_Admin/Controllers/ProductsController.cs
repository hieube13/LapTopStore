using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapTopStore_Common;
using LapTopStore_Computer.Data;
using LapTopStore_Admin.Models;
using LapTopStore_Computer.Models;
using AdminProductResponseData = LapTopStore_Admin.Models.AdminProductResponseData;
using Newtonsoft.Json;
using PagedList.Core;
using LapTopStore_Admin.Data;
using ProductWithCategory = LapTopStore_Admin.Data.ProductWithCategory;
using GetListAttribute = LapTopStore_Admin.Models.GetListAttribute;

namespace LapTopStore_Admin.Controllers
{
    public class ProductsController : Controller
    {

        // GET: Products
        public IActionResult Index(int page = 1, int CatID = 0)
        {
            var pageNumber = page;
            var pageSize = 10;

            List<ProductWithCategory> products = new List<ProductWithCategory>();

            var url = "https://localhost:7019/api";
            var baseUrl = "/Product/AdminProduct";

            var jsonData = CatID.ToString();

            var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData);

            if(data_from_server == null)
            {
                return NotFound();
            }

            products = JsonConvert.DeserializeObject<List<ProductWithCategory>>(data_from_server);

            PagedList<ProductWithCategory> models = new PagedList<ProductWithCategory>(products.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCatID = CatID;
            ViewBag.CurrentPage = pageNumber;

            var jsonData1 = "";
            var baseUrl1 = "/Product/GetListCategory";
            var data1_from_server = PostmanTools.WebPost(url, baseUrl1, jsonData1);
            var result = JsonConvert.DeserializeObject<List<Category>>(data1_from_server);

            Console.WriteLine(data1_from_server);
            ViewData["Danhmuc"] = new SelectList(result, "CategoryId", "CategoryName", CatID);


            return View(models);
        }

        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Products?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Products";
            }

            return Json(new { status = "success", RedirectUrl = url });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var result1 = new GetListAttribute();

            var url = "https://localhost:7019/api";
            var baseUrl = "/Product/GetListCategory";
            var jsonData = "";
            var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData);

            var result = JsonConvert.DeserializeObject<List<Category>>(data_from_server);
            ViewData["Danhmuc"] = new SelectList(result, "CategoryId", "CategoryName");

            var baseUrl1 = "/Product/GetListAttribute";
            var data1_from_server = PostmanTools.WebGet(url, baseUrl1);

            result1 = JsonConvert.DeserializeObject<GetListAttribute>(data1_from_server);


            ViewData["Color"] = result1.AttributeColor;
            ViewData["RAM"] = result1.AttributeRAM;
            ViewData["CPU"] = result1.AttributeCPU;
            ViewData["Driver"] = result1.AttributeDriver;
            ViewData["System"] = result1.AttributeSystem;
            ViewData["Screen"] = result1.AttributeScreen;
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateProducts(CreateProductRequest requestData)
        {
            var returnData = new CreateProductResponse();
            if(requestData == null)
            {
                return NotFound();
            }

            var url = "https://localhost:7019/api";

            var baseUrl = "Product/CreateProduct";

            string josnData = JsonConvert.SerializeObject(requestData);

            var data_from_server = PostmanTools.WebPostImage(url, baseUrl, josnData);

            returnData = JsonConvert.DeserializeObject<CreateProductResponse>(data_from_server);

            return Json(returnData);

        }

        [HttpPost]
        public IActionResult CreateProductAttributes(CreateProductRequest requestData)
        {
            var returnData = new CreateProductResponse();
            if (requestData == null)
            {
                return NotFound();
            }

            var url = "https://localhost:7019/api";

            var baseUrl = "Product/CreateProduct";

            string josnData = JsonConvert.SerializeObject(requestData);

            var data_from_server = PostmanTools.WebPostImage(url, baseUrl, josnData);

            returnData = JsonConvert.DeserializeObject<CreateProductResponse>(data_from_server);

            return Json(returnData);

        }

        public IActionResult Details(int? id)
        {
            var returnData = new ProductDetailResponse();

            var url = "https://localhost:7019/api";

            var baseUrl = "Product/GetProductDetails";

            string josnData = id.ToString();

            var data_from_server = PostmanTools.WebPost(url, baseUrl, josnData);

            var result = JsonConvert.DeserializeObject<ProductDetailResponse>(data_from_server);

            if(result == null)
            {
                return NotFound();
            }

            Console.WriteLine(result);

            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var result1 = new GetListAttribute();

            var url = "https://localhost:7019/api";
            var baseUrl = "/Product/GetListCategory";
            var jsonData = "";
            var data_from_server = PostmanTools.WebPost(url, baseUrl, jsonData);

            var result = JsonConvert.DeserializeObject<List<Category>>(data_from_server);
            ViewData["Danhmuc"] = new SelectList(result, "CategoryId", "CategoryName");

            var baseUrl1 = "/Product/GetListAttribute";
            var data1_from_server = PostmanTools.WebGet(url, baseUrl1);

            result1 = JsonConvert.DeserializeObject<GetListAttribute>(data1_from_server);


            ViewData["Color"] = result1.AttributeColor;
            ViewData["RAM"] = result1.AttributeRAM;
            ViewData["CPU"] = result1.AttributeCPU;
            ViewData["Driver"] = result1.AttributeDriver;
            ViewData["System"] = result1.AttributeSystem;
            ViewData["Screen"] = result1.AttributeScreen;

            var returnData = new ProductDetailResponse();

            var baseUrl2 = "Product/GetProductDetails";

            string josnData = id.ToString();

            var data2_from_server = PostmanTools.WebPost(url, baseUrl2, josnData);

            var result2 = JsonConvert.DeserializeObject<ProductDetailResponse>(data2_from_server);

            if (result2 == null)
            {
                return NotFound();
            }

            return View(result2);
        }

        [HttpPost]
        public IActionResult EditProduct(CreateProductRequest requestData)
        {
            var returnData = new CreateProductResponse();
            if (requestData == null)
            {
                return NotFound();
            }

            var url = "https://localhost:7019/api";

            var baseUrl = "Product/ProductEdit";

            string josnData = JsonConvert.SerializeObject(requestData);

            var data_from_server = PostmanTools.WebPostImage(url, baseUrl, josnData);

            returnData = JsonConvert.DeserializeObject<CreateProductResponse>(data_from_server);

            return Json(returnData);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var url = "https://localhost:7019/api";

            var baseUrl = "Product/GetProductByID";

            string josnData = id.ToString();

            var data_from_server = PostmanTools.WebGetDelete(url, baseUrl, josnData);

            var result = JsonConvert.DeserializeObject<ProductWithCategory>(data_from_server);

            return View(result);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int? id)
        {
            var url = "https://localhost:7019/api";

            var baseUrl = "Product/DeleteProduct";

            string josnData = id.ToString();

            var data_from_server = PostmanTools.WebPostImage(url, baseUrl, josnData);

            Console.WriteLine(data_from_server);

            var result = JsonConvert.DeserializeObject<string>(data_from_server);

            return Json(result);
        }
    }
}
