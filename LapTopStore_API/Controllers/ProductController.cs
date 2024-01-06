using LapTopStore_API.Data;
using LapTopStore_Common;
using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.Product;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace LapTopStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IStoreUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public ProductController(IStoreUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [HttpPost("AdminProduct")]
        public async Task<IActionResult> AdminProduct([FromBody] int CatID)
        {
            List<ProductWithCategory> products = new List<ProductWithCategory>();

            products = await _unitOfWork._productRepository.AdminIndex(CatID);


            return Ok(products);
        }

        [HttpPost("GetListCategory")]
        public async Task<IActionResult> GetListCategory()
        {
            List<Category> categories = new List<Category>();

            categories = await _unitOfWork._productRepository.GetListCategory();


            return Ok(categories);
        }

        [HttpPost("GetListProductSearch")]
        public async Task<IActionResult> GetListProductSearch([FromBody] ProductSearchRequest requestData)
        {
            List<ProductWithCategory> ls = new List<ProductWithCategory>();

            ls = await _unitOfWork._productRepository.AdminIndex(requestData.CatID);

            ls = ls.Where(p => p.ProductName.Contains(requestData.Keyword))
                    .OrderByDescending(p => p.ProductName)
                    .Take(10)
                    .ToList();

            return Ok(ls);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] APICreateProductRequest requestData)
        {

            var returnData = new ProductImageResponse();
            if(requestData == null)
            {
                returnData.ResponseCode = -1;
                returnData.Messenger = "Không tạo được sản phẩm";
                return Ok(returnData);
            }

            var secretKey = _config["Security:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
            var sign = LapTopStore_Common.Securiry.MD5Hash(requestData.PrImage1 + "|" + secretKey);

            requestData.Sign = sign;
            var listImage = new List<string>();
            listImage.Add(requestData.PrImage1);
            listImage.Add(requestData.PrImage2);
            listImage.Add(requestData.PrImage3);
            listImage.Add(requestData.PrImage4);

            var ImageRequest = new ProductImageRequest() {
                Images = listImage,
                Sign = sign
            };

            var url = "https://localhost:7190/api";

            var baseUrl = "/UploadFile/UploadProductImage";

            string josnData = JsonConvert.SerializeObject(ImageRequest);

            var data_from_server = PostmanTools.WebPostImage(url, baseUrl, josnData);

            returnData = JsonConvert.DeserializeObject<ProductImageResponse>(data_from_server);

            requestData.PrImage1 = returnData.ImageNames[0];
            requestData.PrImage2 = returnData.ImageNames[1];
            requestData.PrImage3 = returnData.ImageNames[2];
            requestData.PrImage4 = returnData.ImageNames[3];

            var newProduct = new Product()
            {
                ProductName = requestData.ProductName,
                ProductDescription = requestData.ProductDescription,
                ProductDiscount = requestData.ProductDiscount,
                ProductCreated = DateTime.Now,
                ProductPrice = requestData.ProductPrice,
                CategoryId = requestData.CategoryId,
                ProductInStock = requestData.ProductInStock,
                HomeFlag = requestData.HomeFlag,
                BestSeller = requestData.BestSeller,
                PrImage1 = requestData.PrImage1,
                PrImage2 = requestData.PrImage2,
                PrImage3 = requestData.PrImage3,
                PrImage4 = requestData.PrImage4,
                ProductInOrder = 0
            };

            //var listProduct = _unitOfWork._productRepository.

            var result = await _unitOfWork._productRepository.CreateProduct(newProduct);


            if(result == null)
            {
                returnData.ResponseCode = -1;
                returnData.Messenger = "Không tạo được sản phẩm";
                return Ok(returnData);
            }



            var productID = result;

            List<int> AttrIDList = new List<int> { 
                requestData.ColorCategory,
                requestData.RamCategory,
                requestData.CpuCategory,
                requestData.ScreenCategory,
                requestData.OsCategory,
                requestData.HddCategory,
            };

            var createProductAttr = new CreateProductAttribute() 
            { 
                ProductID = productID,
                AttrIDList = AttrIDList
            };

            await _unitOfWork._productRepository.CreateProductAtrtibute(createProductAttr);

            _unitOfWork.SaveChanges();

            returnData.ResponseCode = 1;
            returnData.Messenger = "Tạo sản phẩm thành công";
            return Ok(returnData);

        }

        [HttpGet("GetListAttribute")]
        public async Task<IActionResult> GetListAttribute()
        {
            var GetListAttribute = new GetListAttribute();

            GetListAttribute = await _unitOfWork._productRepository.GetListAttribute();

            return Ok(GetListAttribute);
        }

        [HttpPost("GetProductDetails")]
        public async Task<IActionResult> GetProductDetails([FromBody] int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var result = new ProductDetails();

            result = await _unitOfWork._productRepository.GetProductDetail(id);

            return Ok(result);
        }

        [HttpGet("GetProductDetailsUser")]
        public async Task<IActionResult> GetProductDetailsUser([FromBody] int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var result = new ProductDetailAndRelated();

            result = await _unitOfWork._productRepository.GetProductAndRelated(id);

            return Ok(result);
        }

        [HttpPost("ProductEdit")]
        public async Task<IActionResult> ProductEdit([FromBody] EditProductRequest requestData)
        {
            var returnData = new APICreateProductResponse();

            if (requestData == null)
            {
                returnData.ResponseCode = -1;
                returnData.Messenger = "không tạo được sản phẩm";
                return Ok(returnData);
            }

            if (requestData.PrImage1 == null && requestData.PrImage2 == null && requestData.PrImage3 == null && requestData.PrImage4 == null)
            {
                var result = _unitOfWork._productRepository.EditProductNoImage(requestData);
                if (result == null)
                {
                    returnData.ResponseCode = -1;
                    returnData.Messenger = "Chưa thêm sản phẩm được";
                    return Ok(returnData);
                }

                returnData.ResponseCode = 1;
                returnData.Messenger = "Đã sửa thành công";
                _unitOfWork.SaveChanges();
                return Ok(returnData);

            }

            var secretKey = _config["Security:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
            var sign = LapTopStore_Common.Securiry.MD5Hash(requestData.PrImage1 + "|" + secretKey);

            requestData.Sign = sign;

            var listImage = new List<string>();
            listImage.Add(requestData.PrImage1);
            listImage.Add(requestData.PrImage2);
            listImage.Add(requestData.PrImage3);
            listImage.Add(requestData.PrImage4);

            var ImageRequest = new ProductImageRequest()
            {
                Images = listImage,
                Sign = sign
            };

            var url = "https://localhost:7190/api";

            var baseUrl = "/UploadFile/UploadEditImage";

            string josnData = JsonConvert.SerializeObject(ImageRequest);

            var data_from_server = PostmanTools.WebPostImage(url, baseUrl, josnData);

            returnData = JsonConvert.DeserializeObject<APICreateProductResponse>(data_from_server);

            requestData.PrImage1 = returnData.ImageNames["Image1"];
            requestData.PrImage2 = returnData.ImageNames["Image2"];
            requestData.PrImage3 = returnData.ImageNames["Image3"];
            requestData.PrImage4 = returnData.ImageNames["Image4"];

            var result1 = await _unitOfWork._productRepository.EditProduct(requestData);

            if (result1 == null)
            {
                returnData.ResponseCode = -1;
                returnData.Messenger = "Chưa thêm sản phẩm được";
                return Ok(returnData);
            }

            returnData.ResponseCode = 1;
            returnData.Messenger = "Đã sửa thành công";
            _unitOfWork.SaveChanges();
            return Ok(returnData);
        }

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] int? id)
        {
            var returnData = new GenericResponse();
            var product = await _unitOfWork._productRepository.GetProductById(id);

            var image1 = product.PrImage1;
            var image2 = product.PrImage2;
            var image3 = product.PrImage3;
            var image4 = product.PrImage4;

            var listImage = new List<string>();
            listImage.Add(image1); 
            listImage.Add(image2);
            listImage.Add(image3);
            listImage.Add(image4);

            var ImageRequest = new ProductImageRequest()
            {
                Images = listImage,
            };

            var url = "https://localhost:7190/api";

            var baseUrl = "/UploadFile/MediaDeleteImage";

            string josnData = JsonConvert.SerializeObject(ImageRequest);

            var data_from_server = PostmanTools.WebPostImage(url, baseUrl, josnData);

            var resultMedia = JsonConvert.DeserializeObject<GenericResponse>(data_from_server);

            var result = await _unitOfWork._productRepository.DeleteProduct(id);

            if(result == -1)
            {
                return Ok("Khong xoa duoc");
            }

            _unitOfWork.SaveChanges();



            return Ok("Da xoa thanh cong");
        }

        [HttpGet("GetProductByID")]
        public async Task<IActionResult> GetProductByID([FromBody] int? id)
        {
           var result = await _unitOfWork._productRepository.GetProductById(id);

            return Ok(result);
        }

        [HttpGet("GetListProductForHome")]
        public async Task<IActionResult> GetListProductForHome()
        {
            var returnData = new GetListPrForHomeRes();

            returnData = await _unitOfWork._productRepository.GetListProductForHome();

            if (returnData == null)
            {
                return Ok("Co loi dai vuong oi");
            }

            return Ok(returnData);
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    CustomerID = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value),
                    CustomerUserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    CustomerEmail = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value
                };
            }

            return null;
        }

    }
}
