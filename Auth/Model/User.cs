namespace Auth.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
