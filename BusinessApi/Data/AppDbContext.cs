using BusinessApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessApi.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<User>().HasData(
        new User {
            Id = 1,
            Email = "admin@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
            Role = "Admin",
            CreatedAt = DateTime.UtcNow
        }
    );

        modelBuilder.Entity<Product>().HasData(
            new Product {
                Id = 1,
                Name = "Laptop",
                Description = "Business laptop",
                Price = 25000,
                StockQuantity = 10,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product {
                Id = 2,
                Name = "Mouse",
                Description = "Wireless mouse",
                Price = 500,
                StockQuantity = 50,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
