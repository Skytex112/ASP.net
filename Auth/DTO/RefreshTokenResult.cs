namespace Auth.DTO
{
    public class RefreshTokenResult
    {
        public string Token { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
