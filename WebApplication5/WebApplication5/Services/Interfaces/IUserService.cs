namespace WebApplication5.Services.Interfaces
{
    public interface IUserService
    {
        bool ValidateUser(string username, string password);
    }
}
