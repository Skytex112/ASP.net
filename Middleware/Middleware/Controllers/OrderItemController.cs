using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Middleware.Data;
using Middleware.DTO;
using Middleware.Models;
using Microsoft.EntityFrameworkCore;

namespace Middleware.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderItemDto dto)
        {
            var item = _mapper.Map<OrderItem>(dto);
            item.Id = Guid.NewGuid();

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<OrderItemDto>(item));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.OrderItems.ToListAsync();
            return Ok(_mapper.Map<OrderItemDto[]>(items));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<OrderItemDto>(item));
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, OrderItemDto dto)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();

            _mapper.Map(dto, item);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<OrderItemDto>(item));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"OrderItem {id} видалено" });
        }
    }

}
