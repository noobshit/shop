using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class ShopContext : DbContext 
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
