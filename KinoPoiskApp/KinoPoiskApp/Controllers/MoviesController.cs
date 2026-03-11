using Microsoft.AspNetCore.Mvc;
using KinoPoiskApp.Services;
using System.Threading.Tasks;

namespace KinoPoiskApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly OmdbService _omdbService;

        public MoviesController(OmdbService omdbService)
        {
            _omdbService = omdbService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovie(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Введіть назву фільму.");

            var movie = await _omdbService.GetMovieAsync(title);

            if (movie == null)
                return NotFound("Фільм не знайдено.");

            return Ok(movie);
        }
    }
}