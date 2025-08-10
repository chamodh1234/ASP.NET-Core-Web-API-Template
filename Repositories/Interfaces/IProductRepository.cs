using WebApiTemplate.Models.Entities;

namespace WebApiTemplate.Repositories.Interfaces;

/// <summary>
/// Product repository interface with custom business logic methods
/// </summary>
public interface IProductRepository : IGenericRepository<Product>
{
    /// <summary>
    /// Get products by category
    /// </summary>
    /// <param name="categoryId">Category ID</param>
    /// <returns>List of products in the category</returns>
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

    /// <summary>
    /// Get active products
    /// </summary>
    /// <returns>List of active products</returns>
    Task<IEnumerable<Product>> GetActiveProductsAsync();

    /// <summary>
    /// Get products with low stock
    /// </summary>
    /// <param name="threshold">Stock threshold</param>
    /// <returns>List of products with low stock</returns>
    Task<IEnumerable<Product>> GetProductsWithLowStockAsync(int threshold = 10);

    /// <summary>
    /// Search products by name or description
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <returns>List of matching products</returns>
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);

    /// <summary>
    /// Get products by price range
    /// </summary>
    /// <param name="minPrice">Minimum price</param>
    /// <param name="maxPrice">Maximum price</param>
    /// <returns>List of products in the price range</returns>
    Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);

    /// <summary>
    /// Get product by SKU
    /// </summary>
    /// <param name="sku">Product SKU</param>
    /// <returns>Product or null if not found</returns>
    Task<Product?> GetBySkuAsync(string sku);

    /// <summary>
    /// Update product stock
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="quantity">Quantity to update</param>
    /// <returns>True if update was successful</returns>
    Task<bool> UpdateStockAsync(int productId, int quantity);

    /// <summary>
    /// Get product statistics
    /// </summary>
    /// <returns>Product statistics</returns>
    Task<ProductStatistics> GetProductStatisticsAsync();
}

/// <summary>
/// Product statistics
/// </summary>
public class ProductStatistics
{
    /// <summary>
    /// Total number of products
    /// </summary>
    public int TotalProducts { get; set; }

    /// <summary>
    /// Number of active products
    /// </summary>
    public int ActiveProducts { get; set; }

    /// <summary>
    /// Number of products with low stock
    /// </summary>
    public int LowStockProducts { get; set; }

    /// <summary>
    /// Total value of inventory
    /// </summary>
    public decimal TotalInventoryValue { get; set; }

    /// <summary>
    /// Average product price
    /// </summary>
    public decimal AveragePrice { get; set; }
} 