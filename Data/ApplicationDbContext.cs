using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Models.Entities;

namespace WebApiTemplate.Data;

/// <summary>
/// Application database context
/// </summary>
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Products table
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Categories table
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Orders table
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Order items table
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Sku).IsRequired().HasMaxLength(50);
            
            // Create unique index on SKU
            entity.HasIndex(e => e.Sku).IsUnique();
            
            // Configure relationship with Category
            entity.HasOne(e => e.Category)
                  .WithMany(e => e.Products)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            
            // Create unique index on Name
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configure Order entity
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ShippingAddress).IsRequired().HasMaxLength(500);
            entity.Property(e => e.BillingAddress).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            
            // Create unique index on OrderNumber
            entity.HasIndex(e => e.OrderNumber).IsUnique();
            
            // Configure relationship with User
            entity.HasOne(e => e.User)
                  .WithMany(e => e.Orders)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure OrderItem entity
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18,2)");
            
            // Configure relationships
            entity.HasOne(e => e.Order)
                  .WithMany(e => e.OrderItems)
                  .HasForeignKey(e => e.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Product)
                  .WithMany(e => e.OrderItems)
                  .HasForeignKey(e => e.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure User entity (extending IdentityUser)
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    /// <summary>
    /// Seed initial data
    /// </summary>
    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Electronic devices and gadgets",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Category
            {
                Id = 2,
                Name = "Books",
                Description = "Books and publications",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Category
            {
                Id = 3,
                Name = "Clothing",
                Description = "Apparel and accessories",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "iPhone 15 Pro",
                Description = "Latest iPhone with advanced features",
                Price = 999.99m,
                StockQuantity = 50,
                Sku = "IPHONE-15-PRO",
                IsActive = true,
                CategoryId = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Product
            {
                Id = 2,
                Name = "Clean Code by Robert Martin",
                Description = "A handbook of agile software craftsmanship",
                Price = 49.99m,
                StockQuantity = 100,
                Sku = "BOOK-CLEAN-CODE",
                IsActive = true,
                CategoryId = 2,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Product
            {
                Id = 3,
                Name = "Cotton T-Shirt",
                Description = "Comfortable cotton t-shirt",
                Price = 19.99m,
                StockQuantity = 200,
                Sku = "TSHIRT-COTTON",
                IsActive = true,
                CategoryId = 3,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        );
    }

    /// <summary>
    /// Override SaveChanges to automatically set audit fields
    /// </summary>
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    /// <summary>
    /// Override SaveChangesAsync to automatically set audit fields
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Update audit fields (CreatedAt, UpdatedAt, etc.)
    /// </summary>
    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
} 