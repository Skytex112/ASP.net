using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Middleware.Data;
using Middleware.Models;
using Middleware.DTO;
using System;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.Id = Guid.NewGuid();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, _mapper.Map<OrderDto>(order));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(_mapper.Map<OrderDto[]>(orders));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, OrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _mapper.Map(dto, order);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Order {id} видалено" });
        }
    }
}