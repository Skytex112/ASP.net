using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs;
using WebApplication3.Exceptions;
using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (_userRepository.GetByEmail(dto.Email) != null)
            {
                throw new ConflictException("Користувач з таким email вже існує.");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                UserName = dto.UserName,
                Password = dto.Password
            };

            _userRepository.Add(newUser);

            return Created(string.Empty, new
            {
                Id = newUser.Id,
                Email = newUser.Email,
                UserName = newUser.UserName
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _userRepository.GetByEmail(dto.Email);

            if (user == null || user.Password != dto.Password)
            {
                throw new UnauthorizedAppException("Невірний email або пароль.");
            }

            var (token, expiresIn) = _tokenService.GenerateToken(user);

            return Ok(new
            {
                accessToken = token,
                expiresIn = expiresIn
            });
        }
    }
}