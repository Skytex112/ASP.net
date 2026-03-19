using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
