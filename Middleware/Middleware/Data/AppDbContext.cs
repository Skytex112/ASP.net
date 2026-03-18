using Microsoft.EntityFrameworkCore;
using Middleware.DTO;
using Middleware.Models;

namespace Middleware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

    }
}