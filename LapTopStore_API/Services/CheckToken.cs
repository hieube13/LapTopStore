using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LapTopStore_API.Services
{
    public class CheckToken : ICheckToken
    {
        private readonly IConfiguration _configuration;
        public CheckToken(IConfiguration configuration) 
        { 
            _configuration= configuration;
        }

        public bool IsAccessTokenValid(string accessToken)
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

                var expirationClaim = principal.FindFirst("exp");

                if (expirationClaim != null && long.TryParse(expirationClaim.Value, out var expirationUnixTime))
                {
                    var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTime).UtcDateTime;
                    return DateTime.UtcNow < expirationDateTime;
                }

                return false;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
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
