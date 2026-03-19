using WebApplication2.Models;

namespace WebApplication2.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task CreateAsync(Car car);
        Task SaveChangesAsync();
    }
}