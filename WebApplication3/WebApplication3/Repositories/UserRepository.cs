using WebApplication3.Models;
using WebApplication3.Interfaces;

namespace WebApplication6.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public User? GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

        public void Add(User user) => _users.Add(user);
    }
}