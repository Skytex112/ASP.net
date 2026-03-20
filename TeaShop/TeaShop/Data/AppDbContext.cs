using Microsoft.EntityFrameworkCore;
using TeaShop.Models;

namespace TeaShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Tea> Teas => Set<Tea>();
    }
}
