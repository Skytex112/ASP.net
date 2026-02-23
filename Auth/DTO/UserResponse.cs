namespace Auth.DTO
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public String Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
