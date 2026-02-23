using Auth.Model;
using Microsoft.AspNetCore.Identity;

namespace Auth.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
