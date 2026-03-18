using ProductCQRS.Profiles;

namespace ProductCQRS.Services
{
    public class RoleService
    {
        private readonly IConfiguration _configuration;

        public RoleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool CheckAccess(string username, string password, string role)
        {
            var profile = _configuration.GetSection("AdminProfile").Get<AdminProfile>();

            if (profile == null)
                return false;

            return profile.Username == username &&
                   profile.Password == password &&
                   profile.Role == role;
        }
    }
}
