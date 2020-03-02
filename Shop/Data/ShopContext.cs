using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class ShopContext : IdentityDbContext 
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

            modelBuilder.Entity<Product>().HasData(
                new Product { Id=1, ImagePath="sample_1.jpg", Name="Ladder", Price=125.0M},
                new Product { Id=2, ImagePath="sample_2.jpg", Name="Drill", Price=150M},
                new Product { Id=3, ImagePath="sample_3.jpg", Name="Grinder", Price=150M},
                new Product { Id=4, ImagePath="sample_4.jpg", Name="Gloves", Price=3.5M},
                new Product { Id=5, ImagePath="sample_5.jpg", Name="Thermometer", Price=19.99M},
                new Product { Id=6, ImagePath="sample_6.jpg", Name="Measure tape", Price=3.5M},
                new Product { Id=7, ImagePath="sample_7.jpg", Name="WD40", Price=7M},
                new Product { Id=8, ImagePath="sample_8.jpg", Name="Screwdrivers", Price=12.99M},
                new Product { Id = 9, ImagePath = "sample_9.jpg", Name = "Lawnmower", Price = 199.99M }
                );
        }
    }
}
