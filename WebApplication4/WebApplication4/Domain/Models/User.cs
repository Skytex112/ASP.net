namespace WebApplication4.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
    }
}
