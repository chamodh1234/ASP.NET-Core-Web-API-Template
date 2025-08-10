using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Data;
using WebApiTemplate.Models.Entities;
using WebApiTemplate.Repositories.Interfaces;

namespace WebApiTemplate.Repositories;

/// <summary>
/// Product repository implementation
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && !p.IsDeleted && p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted && p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsWithLowStockAsync(int threshold = 10)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted && p.StockQuantity <= threshold)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        var normalizedSearchTerm = searchTerm.ToLower();
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted && p.IsActive &&
                       (p.Name.ToLower().Contains(normalizedSearchTerm) ||
                        p.Description.ToLower().Contains(normalizedSearchTerm) ||
                        p.Sku.ToLower().Contains(normalizedSearchTerm)))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted && p.IsActive &&
                       p.Price >= minPrice && p.Price <= maxPrice)
            .OrderBy(p => p.Price)
            .ToListAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Sku == sku && !p.IsDeleted);
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantity)
    {
        var product = await _dbSet.FindAsync(productId);
        if (product == null || product.IsDeleted)
        {
            return false;
        }

        product.StockQuantity = Math.Max(0, product.StockQuantity + quantity);
        product.UpdatedAt = DateTime.UtcNow;

        return true;
    }

    public async Task<ProductStatistics> GetProductStatisticsAsync()
    {
        var products = await _dbSet.Where(p => !p.IsDeleted).ToListAsync();

        var statistics = new ProductStatistics
        {
            TotalProducts = products.Count,
            ActiveProducts = products.Count(p => p.IsActive),
            LowStockProducts = products.Count(p => p.StockQuantity <= 10),
            TotalInventoryValue = products.Sum(p => p.Price * p.StockQuantity),
            AveragePrice = products.Any() ? products.Average(p => p.Price) : 0
        };

        return statistics;
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }
} 