using LapTopStore_API.Data;
using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.Customer;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LapTopStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IStoreUnitOfWork _unitOfWork;
        private readonly LapTopStoreContext _context;
        private readonly IConfiguration _configuration;

        public CustomerController(IStoreUnitOfWork unitOfWork, LapTopStoreContext context, IConfiguration configuration) 
        { 
            _unitOfWork= unitOfWork;
            _context= context;
            _configuration= configuration;
        }

        [HttpPost("CustomerRegister")]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterRequest requestData)
        {
            try
            {
                var returnData = new CustomerRegisterReponseData();
                // dữ liệu có truyền đây đủ hay không 

                // check các dữ liệu này vi phạm các lỗi về XSS, CSRF 

                var check = _context.Customers.Where(c => c.CustomerUserName == requestData.CustomerUserName || c.CustomerEmail == requestData.CustomerEmail).FirstOrDefault();
                if (check != null)
                {
                    returnData.ResponseCode = -2;
                    returnData.Messenger = "đăng kí bị trùng";
                    return Ok(returnData);
                }

                var result = await _unitOfWork._customerRepository.Register(requestData);
                if(result !=1)
                {
                    returnData.ResponseCode = -1;
                    returnData.Messenger = "Đăng kí thất bại";
                    return Ok(returnData);
                }

                _unitOfWork.SaveChanges();


                returnData.ResponseCode = 1;
                returnData.Messenger = "Đăng kí thành công";
                returnData.CustomerUserName= requestData.CustomerUserName;
                returnData.CustomerFullName = requestData.CustomerFullName;
                returnData.CustomerEmail = requestData.CustomerEmail;
                returnData.CustomerPhone = requestData.CustomerPhone;
                returnData.CustomerAddress= requestData.CustomerAddress;


                return Ok(returnData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LapTopStore_Computer.Data.CustomerLoginRequestData requestData)
        {
            try
            {
                var returnData = new CustomerLoginResponseData();

                var customer = await _unitOfWork._customerRepository.Login(requestData);

                if(customer == null)
                {
                    returnData.ResponseCode = -1;
                    returnData.Messenger = "Đăng nhập thất bại";

                    return Ok(returnData);
                }

                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.PrimarySid, customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, customer.CustomerUserName),
                    new Claim(ClaimTypes.Email, customer.CustomerEmail),
                };

                var newAccessToken = CreateToken(authClaims);
                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                var refreshToken = GenerateRefreshToken();

                var expiredRefreshToken = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";

                var resultUpdateRefreshToken = await _unitOfWork._customerRepository.UpdateRefreshToken(new UpdateRefreshTokenRequest()
                {
                    UserID = customer.CustomerId,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToInt32(expiredRefreshToken))
                });

                _unitOfWork.SaveChanges();

                returnData.ResponseCode = 1;
                returnData.Messenger = "Đăng nhập thành công";
                returnData.Accesstoken = token; 
                returnData.RefreshToken = refreshToken;
                returnData.CustomerUsername = customer.CustomerUserName;

                return Ok(returnData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("RefreshToken")]
        //[Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string username = "";

            var nameIdentifierClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null)
            {
                username = nameIdentifierClaim.Value;
                // Bây giờ bạn có giá trị của NameIdentifier
            }
            else
            {
                // Claim không tồn tại trong principal
            }

            var user = _context.Customers.Where(x => x.CustomerUserName == username).FirstOrDefault();

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            var expiredRefreshToken = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
            await _unitOfWork._customerRepository.UpdateRefreshToken(new UpdateRefreshTokenRequest()
            {
                //UserID = Int32.Parse(principal.FindFirst(ClaimTypes.PrimarySid)?.Value),
                UserID = user.CustomerId,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToInt32(expiredRefreshToken))
            });
            _unitOfWork.SaveChanges();
            //await _userManager.UpdateAsync(user);

            //return new ObjectResult(new
            //{
            //    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            //    RefreshToken = newRefreshToken
            //});

            var result = new TokenModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };

            return Ok(result);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

    }
}
