using WebApiTemplate.Models.Common;
using WebApiTemplate.Models.DTOs;
using WebApiTemplate.Repositories.Interfaces;

namespace WebApiTemplate.Services.Interfaces;

/// <summary>
/// Product service interface
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Get all products with pagination
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="searchTerm">Search term</param>
    /// <param name="categoryId">Category filter</param>
    /// <param name="minPrice">Minimum price</param>
    /// <param name="maxPrice">Maximum price</param>
    /// <returns>Paginated product list</returns>
    Task<PaginatedResponse<ProductDto>> GetProductsAsync(
        int pageNumber = 1,
        int pageSize = 10,
        string? searchTerm = null,
        int? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);

    /// <summary>
    /// Get product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product DTO or null</returns>
    Task<ProductDto?> GetProductByIdAsync(int id);

    /// <summary>
    /// Get product by SKU
    /// </summary>
    /// <param name="sku">Product SKU</param>
    /// <returns>Product DTO or null</returns>
    Task<ProductDto?> GetProductBySkuAsync(string sku);

    /// <summary>
    /// Create new product
    /// </summary>
    /// <param name="createProductDto">Product creation data</param>
    /// <returns>Created product DTO</returns>
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);

    /// <summary>
    /// Update existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="updateProductDto">Product update data</param>
    /// <returns>Updated product DTO</returns>
    Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto);

    /// <summary>
    /// Delete product (soft delete)
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> DeleteProductAsync(int id);

    /// <summary>
    /// Get products by category
    /// </summary>
    /// <param name="categoryId">Category ID</param>
    /// <returns>List of products in the category</returns>
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);

    /// <summary>
    /// Get active products
    /// </summary>
    /// <returns>List of active products</returns>
    Task<IEnumerable<ProductDto>> GetActiveProductsAsync();

    /// <summary>
    /// Get products with low stock
    /// </summary>
    /// <param name="threshold">Stock threshold</param>
    /// <returns>List of products with low stock</returns>
    Task<IEnumerable<ProductDto>> GetProductsWithLowStockAsync(int threshold = 10);

    /// <summary>
    /// Search products
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <returns>List of matching products</returns>
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);

    /// <summary>
    /// Get products by price range
    /// </summary>
    /// <param name="minPrice">Minimum price</param>
    /// <param name="maxPrice">Maximum price</param>
    /// <returns>List of products in the price range</returns>
    Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);

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

    /// <summary>
    /// Validate product SKU uniqueness
    /// </summary>
    /// <param name="sku">Product SKU</param>
    /// <param name="excludeId">Product ID to exclude from validation</param>
    /// <returns>True if SKU is unique</returns>
    Task<bool> IsSkuUniqueAsync(string sku, int? excludeId = null);
} 