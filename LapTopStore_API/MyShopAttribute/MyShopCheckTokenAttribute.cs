using LapTopStore_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LapTopStore_API.MyShopAttribute
{
    public class MyShopCheckTokenAttribute : TypeFilterAttribute
    {
        public MyShopCheckTokenAttribute() : base(typeof(CheckTokenAttribute))
        {
        }
    }

    public class CheckTokenAttribute : IAsyncAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        public CheckTokenAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
           
            var httpContext = context.HttpContext;

            // Lấy giá trị của AccessToken từ cookie
            var accessToken = httpContext.Request.Cookies["LapTopJwtToken"];

            // Lấy giá trị của RefreshToken từ cookie
            var refreshToken = httpContext.Request.Cookies["LapTopRefreshToken"];

            var principal = GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                context.Result = new JsonResult(new
                {
                    Code = HttpStatusCode.Unauthorized,
                    Message = "Bạn phải đăng nhập"
                });
            }


            if (!IsAccessTokenValid(accessToken))
            {
                // Token không hợp lệ, thực hiện xử lý tùy thuộc vào yêu cầu của bạn
                context.Result = new JsonResult(new
                {
                    Code = HttpStatusCode.Unauthorized,
                    Message = "AccessToken hết hạn"
                });

                return;
            }

        }

        private bool IsAccessTokenValid(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])), 
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero 
            };

            try
            {
                // Validate and decode the token
                var principal = tokenHandler.ValidateToken(accessToken, validationParameters, out _);

                // Extract the expiration time from the token's claims
                var expirationTime = principal.FindFirst(ClaimTypes.Expiration)?.Value;

                if (expirationTime != null && DateTime.TryParse(expirationTime, out var expirationDateTime))
                {
                    return DateTime.UtcNow < expirationDateTime;
                }

                return false;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
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
    }
}
