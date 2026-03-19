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
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasMany(i => i.Photos)
                .WithOne(p => p.OrderItem)
                .HasForeignKey(p => p.OrderItemId);

        }
    }
}