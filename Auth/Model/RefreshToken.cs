using Auth.Data;

namespace Auth.Model
{
    public class RefreshToken
    {
        public Guid id { get; set; }
        public string token { get; set; } = string.Empty;
        public DateTime ExpireAtUtc { get; set; }
        public bool IsRevoke { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}
