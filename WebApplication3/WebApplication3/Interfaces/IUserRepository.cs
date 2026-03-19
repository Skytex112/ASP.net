using WebApplication3.Models;

namespace WebApplication3.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        void Add(User user);
    }
}