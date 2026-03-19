using WebApplication3.Models;

namespace WebApplication3.Interfaces
{
    public interface ITokenService
    {
        (string AccessToken, int ExpiresIn) GenerateToken(User user);
    }
}