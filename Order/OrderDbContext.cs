using Microsoft.EntityFrameworkCore;
using Order.Models;

namespace Order
{
    public class OrderDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<OrderModel> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            modelBuilder.Entity<OrderModel>(entity =>
            {
                // Configure entity properties, relationships, indexes, etc.
            });
        }

    }
}
