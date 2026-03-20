using AuthApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AuthApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "kolya",
                    PasswordHash = "12345",
                    Role = "User"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    PasswordHash = "admin123",
                    Role = "Admin"
                }
            );
        }

    }
}
