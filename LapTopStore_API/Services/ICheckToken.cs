using System.Security.Claims;

namespace LapTopStore_API.Services
{
    public interface ICheckToken
    {
        bool IsAccessTokenValid(string accessToken);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
