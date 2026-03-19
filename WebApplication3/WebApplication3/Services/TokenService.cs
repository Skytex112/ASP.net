using WebApplication3.Models;
using WebApplication3.Interfaces;

namespace WebApplication6.Services
{
    public class TokenService : ITokenService
    {
        public (string AccessToken, int ExpiresIn) GenerateToken(User user)
        {
            string token = Guid.NewGuid().ToString();
            int expiresIn = 3600;

            return (token, expiresIn);
        }
    }
}