using AuthApi.DTO;

namespace AuthApi.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDto Authenticate(LoginRequestDto request);
    }
}
