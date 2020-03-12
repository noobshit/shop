using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;

namespace Shop.Data.Context
{
    public class ShopContext : IdentityDbContext<ShopUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CartProduct>().HasKey(cp => new { cp.CartId, cp.ProductId });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ImagePath = "sample_1.jpg", Name = "Ladder", Price = 125.0M },
                new Product { Id = 2, ImagePath = "sample_2.jpg", Name = "Drill", Price = 150M },
                new Product { Id = 3, ImagePath = "sample_3.jpg", Name = "Grinder", Price = 150M },
                new Product { Id = 4, ImagePath = "sample_4.jpg", Name = "Gloves", Price = 3.5M },
                new Product { Id = 5, ImagePath = "sample_5.jpg", Name = "Thermometer", Price = 19.99M },
                new Product { Id = 6, ImagePath = "sample_6.jpg", Name = "Measure tape", Price = 3.5M },
                new Product { Id = 7, ImagePath = "sample_7.jpg", Name = "WD40", Price = 7M },
                new Product { Id = 8, ImagePath = "sample_8.jpg", Name = "Screwdrivers", Price = 12.99M },
                new Product { Id = 9, ImagePath = "sample_9.jpg", Name = "Lawnmower", Price = 199.99M },
                new Product { Id = 10, ImagePath = "sample_10.jpg", Name = "Drainpipe", Price = 29M },
                new Product { Id = 11, ImagePath = "sample_11.jpg", Name = "Window", Price = 400M },
                new Product { Id = 12, ImagePath = "sample_12.jpg", Name = "Coal", Price = 22.99M },
                new Product { Id = 13, ImagePath = "sample_13.jpg", Name = "Plywood", Price = 7.50M },
                new Product { Id = 14, ImagePath = "sample_14.jpg", Name = "Brick", Price = 3.99M },
                new Product { Id = 15, ImagePath = "sample_15.jpg", Name = "Cement mixer", Price = 400.0M },
                new Product { Id = 16, ImagePath = "sample_16.jpg", Name = "Barrow", Price = 199.99M },
                new Product { Id = 17, ImagePath = "sample_17.jpg", Name = "Sink", Price = 169.99M },
                new Product { Id = 18, ImagePath = "sample_18.jpg", Name = "Board", Price = 35.0M },
                new Product { Id = 19, ImagePath = "sample_19.jpg", Name = "Lamp", Price = 79.99M },
                new Product { Id = 20, ImagePath = "sample_20.jpg", Name = "LED Strip", Price = 30.99M },
                new Product { Id = 21, ImagePath = "sample_21.jpg", Name = "Paper bag", Price = 0.99M },
                new Product { Id = 22, ImagePath = "sample_22.jpg", Name = "Saw", Price = 39.99M },
                new Product { Id = 23, ImagePath = "sample_23.jpg", Name = "Knife", Price = 19.99M },
                new Product { Id = 24, ImagePath = "sample_24.jpg", Name = "Scissors", Price = 9.99M }
                );
        }
    }
}
