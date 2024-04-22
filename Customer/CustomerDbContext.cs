using System.Collections.Generic;
using System.Reflection.Emit;
using Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer
{
    public class CustomerDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<CustomerModel> CustomerModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            modelBuilder.Entity<CustomerModel>(entity =>
            {
                // Configure entity properties, relationships, indexes, etc.
            });
        }

    }
}