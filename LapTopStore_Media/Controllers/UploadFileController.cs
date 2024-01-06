using LapTopStore_Common;
using LapTopStore_Computer.Data;
using LapTopStore_Media.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapTopStore_Media.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        protected readonly IConfiguration _config;

        public UploadFileController(IConfiguration config)
        {
            _config= config;
        }

        [HttpPost("UploadUserImage")]
        public async Task<IActionResult> UploadUserImage([FromBody] UserImageRequestData requestData)
        {
            var returnData = new UserImageResponseData();

            try
            {
                await Task.Yield();

                string imgPath = string.Empty;
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.Base64Image)
                    || string.IsNullOrEmpty(requestData.Sign))
                {
                    returnData.ResponseCode = -1;
                    returnData.Messenger = "Dữ liệu đầu vào không hợp lệ";
                    return Ok(returnData);
                }

                var secretKey = _config["Sercurity:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
                var verifySign = Securiry.MD5Hash(requestData.Base64Image + "|" + secretKey);

                // xử lý che email , số điện thoại , link
                if (requestData.Sign != verifySign)
                {

                    returnData.ResponseCode = -3;
                    returnData.Messenger = "Chữ ký không hợp lệ";
                    return Ok(returnData);
                }


                var path = "Product"; //Path

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string imageName = Guid.NewGuid().ToString() + ".png";
                //set the image path
                imgPath = Path.Combine(path, imageName);
                if (requestData.Base64Image.Contains("data:image"))
                {
                    requestData.Base64Image = requestData.Base64Image.Substring(requestData.Base64Image.LastIndexOf(',') + 1);
                }
                byte[] imageBytes = Convert.FromBase64String(requestData.Base64Image);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);

                // Update Database 
                returnData.ResponseCode = 1;
                returnData.Messenger = imageName;
                return Ok(returnData);
            }
            catch (Exception ex)
            {
                returnData.ResponseCode = -969;
                returnData.Messenger = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Ok(returnData);
            }

            return Ok();
        }

        [HttpPost("UploadProductImage")]
        public async Task<IActionResult> UploadProductImage([FromBody] MediaProductImageRequest requestData)
        {
            var returnData = new MediaProductImageResponse();

            try
            {
                await Task.Yield();

                string imgPath = string.Empty;
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.Images[0])
                    || string.IsNullOrEmpty(requestData.Images[1])
                    || string.IsNullOrEmpty(requestData.Images[2])
                    || string.IsNullOrEmpty(requestData.Images[3])
                    || requestData.Images.Count != 4
                    || string.IsNullOrEmpty(requestData.Sign))
                {
                    returnData.ResponseCode = -1;
                    returnData.Messenger = "Dữ liệu đầu vào không hợp lệ";
                    return Ok(returnData);
                }

                var secretKey = _config["Sercurity:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
                var verifySign = Securiry.MD5Hash(requestData.Images[0] + "|" + secretKey);

                // xử lý che email , số điện thoại , link
                if (requestData.Sign != verifySign)
                {

                    returnData.ResponseCode = -3;
                    returnData.Messenger = "Chữ ký không hợp lệ";
                    return Ok(returnData);
                }


                var path = "Product"; //Path

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                List<string> imageNames = new List<string>();

                for (int i = 0; i < requestData.Images.Count; i++)
                {
                    string imageName = Guid.NewGuid().ToString() + ".png";
                    imgPath = Path.Combine(path, imageName);

                    if (requestData.Images[i].Contains("data:image"))
                    {
                        requestData.Images[i] = requestData.Images[i].Substring(requestData.Images[i].LastIndexOf(',') + 1);
                    }

                    byte[] imageBytes = Convert.FromBase64String(requestData.Images[i]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);

                    imageNames.Add(imageName);
                }

                // Update Database 
                returnData.ResponseCode = 1;
                returnData.Messenger = "Lưu ảnh thành công";
                returnData.ImageNames = imageNames;
                return Ok(returnData);
            }
            catch (Exception ex)
            {
                returnData.ResponseCode = -969;
                returnData.Messenger = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Ok(returnData);
            }

            return Ok();
        }

        [HttpPost("UploadEditImage")]
        public async Task<IActionResult> UploadEditImage([FromBody] MediaProductImageRequest requestData)
        {
            var returnData = new MediaEditProduct();

            try
            {
                await Task.Yield();

                string imgPath = string.Empty;

                var secretKey = _config["Sercurity:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
                var verifySign = Securiry.MD5Hash(requestData.Images[0] + "|" + secretKey);

                // xử lý che email , số điện thoại , link
                if (requestData.Sign != verifySign)
                {

                    returnData.ResponseCode = -3;
                    returnData.Messenger = "Chữ ký không hợp lệ";
                    return Ok(returnData);
                }


                var path = "Product"; //Path

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                Dictionary<string, string> imagePaths = new Dictionary<string, string>();

                for (int i = 0; i < requestData.Images.Count; i++)
                {
                    string? imageName = requestData.Images[i] != null && requestData.Images[i] != "undefined"
                        ? $"{Guid.NewGuid().ToString()}.png"
                        : null; 

                    if (imageName != null)
                    {
                        imgPath = Path.Combine(path, imageName);

                        if (requestData.Images[i].Contains("data:image"))
                        {
                            requestData.Images[i] = requestData.Images[i].Substring(requestData.Images[i].LastIndexOf(',') + 1);
                        }

                        byte[] imageBytes = Convert.FromBase64String(requestData.Images[i]);
                        using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                        {
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                            image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                        }

                        imagePaths.Add($"Image{i + 1}", imageName);
                    }
                    else
                    {
                        // Xử lý trường hợp requestData.Images[i] là null
                        imagePaths.Add($"Image{i + 1}", null);
                    }
                }

                // Update Database 
                returnData.ResponseCode = 1;
                returnData.Messenger = "Lưu ảnh thành công";
                returnData.ImageNames = imagePaths;
                return Ok(returnData);
            }
            catch (Exception ex)
            {
                returnData.ResponseCode = -969;
                returnData.Messenger = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Ok(returnData);
            }

            return Ok();
        }

        [HttpPost("MediaDeleteImage")]
        public async Task<IActionResult> MediaDeleteImage([FromBody] MediaProductImageRequest requestData)
        {
            var returnData = new GenericResponse();
            try
            {
                string imagePath = "Product";

                foreach (var imageName in requestData.Images)
                {
                    string fullPath = Path.Combine(imagePath, imageName);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                returnData.ResponseCode = 1;
                returnData.Messenger = "Xoa thanh cong o media";

                return Ok(returnData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
