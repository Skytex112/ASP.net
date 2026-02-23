namespace Auth.DTO
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessExpiresAtUtc { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshExpiresAtUtc { get; set; }
    }
}
