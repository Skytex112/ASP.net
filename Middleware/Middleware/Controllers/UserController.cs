using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;
using Middleware.DTO;
using Middleware.Models;
using System;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Id = Guid.NewGuid();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, _mapper.Map<UserDto>(user));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(_mapper.Map<UserDto[]>(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, UserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _mapper.Map(dto, user);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"User {id} видалено" });
        }
    }
}