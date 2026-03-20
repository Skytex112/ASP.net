using WebApplication5.Models;
using WebApplication5.Repositories.Interfaces;
namespace WebApplication5.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
    }
}
