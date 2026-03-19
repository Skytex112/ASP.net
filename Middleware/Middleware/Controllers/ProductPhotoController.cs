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

    public class ProductPhotoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductPhotoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductPhotoDto dto)
        {
            var photo = _mapper.Map<ProductPhoto>(dto);
            photo.Id = Guid.NewGuid();

            _context.ProductPhotos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = photo.Id }, _mapper.Map<ProductPhotoDto>(photo));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var photos = await _context.ProductPhotos.ToListAsync();
            return Ok(_mapper.Map<List<ProductPhotoDto>>(photos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var photo = await _context.ProductPhotos.FindAsync(id);
            if (photo == null) return NotFound();
            return Ok(_mapper.Map<ProductPhotoDto>(photo));
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, ProductPhotoDto dto)
        {
            var photo = await _context.ProductPhotos.FindAsync(id);
            if (photo == null) return NotFound();

            _mapper.Map(dto, photo);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ProductPhotoDto>(photo));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var photo = await _context.ProductPhotos.FindAsync(id);
            if (photo == null) return NotFound();

            _context.ProductPhotos.Remove(photo);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Photo {id} видалено" });
        }

    }
}
