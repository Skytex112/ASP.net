using WebApplication2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllAsync() => await _context.Cars.ToListAsync();

        public async Task<Car?> GetByIdAsync(int id) => await _context.Cars.FindAsync(id);

        public async Task CreateAsync(Car car) => await _context.Cars.AddAsync(car);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}