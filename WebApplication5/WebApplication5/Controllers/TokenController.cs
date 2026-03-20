using Microsoft.AspNetCore.Mvc;
using WebApplication5.DTO;
using WebApplication5.Services.Interfaces;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("generate")]
        public IActionResult Generate([FromBody] TokenRequestDto request)
        {
            try
            {
                var token = _tokenService.GenerateToken(request);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password");
            }
        }

    }
}
