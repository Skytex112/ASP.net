using WebApplication5.DTO;

namespace WebApplication5.Services.Interfaces
{
    public interface ITokenService
    {
        TokenResponseDto GenerateToken(TokenRequestDto request);
    }
}
