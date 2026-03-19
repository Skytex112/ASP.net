using Microsoft.AspNetCore.Mvc;
using WebApplication4.Domain.Interfaces;
using WebApplication4.Domain.Models;
using WebApplication4.DTO;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
                return Conflict(new { message = "User with this email already exists" });

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = dto.Password,
                Age = dto.Age,
                UserName = dto.UserName
            };

            await _userRepository.AddAsync(user);

            return CreatedAtAction(nameof(Register), new { user.Id, user.Email, user.UserName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || user.PasswordHash != dto.Password)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = Guid.NewGuid().ToString();
            return Ok(new { accessToken = token, expiresIn = 3600 });
        }
    }
}