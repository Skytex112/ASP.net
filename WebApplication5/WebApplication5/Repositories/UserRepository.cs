using WebApplication5.Models;
using WebApplication5.Repositories.Interfaces;

namespace WebApplication5.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
    {
        new User { Id = 1, Username = "kolya", PasswordHash = "12345" },
        new User { Id = 2, Username = "admin", PasswordHash = "admin123" }
    };

        public User GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

    }
}
