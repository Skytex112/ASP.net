namespace Auth.DTO
{
    public class AccessTokenResult
    {
        public string Token { get; set; }
        public DateTime ExpiresAtUtc { get; set; }

    }
}
