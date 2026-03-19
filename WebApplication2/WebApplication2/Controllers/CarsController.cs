using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO.Cars;
using WebApplication2.Models;
using WebApplication2.Repositories.Interfaces;

namespace CarShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _repository;

        public CarsController(ICarRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _repository.GetAllAsync();


            var carsDto = cars.Select(c => new CarDto
            {
                Id = c.Id,
                FullName = $"{c.Brand} {c.Model}",
                Price = c.Price
            });

            return Ok(carsDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCar([FromBody] CreateCarDto dto)
        {
            var car = new Car
            {
                Brand = dto.Brand,
                Model = dto.Model,
                Price = dto.Price,
                Year = dto.Year
            };

            await _repository.CreateAsync(car);
            await _repository.SaveChangesAsync();

            return Ok(new { Message = "Автомобіль успішно додано", CarId = car.Id });
        }
    }
}