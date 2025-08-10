using AutoMapper;
using Microsoft.Extensions.Logging;
using WebApiTemplate.Models.Common;
using WebApiTemplate.Models.DTOs;
using WebApiTemplate.Models.Entities;
using WebApiTemplate.Repositories.Interfaces;
using WebApiTemplate.Services.Interfaces;

namespace WebApiTemplate.Services;

/// <summary>
/// Product service implementation
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResponse<ProductDto>> GetProductsAsync(
        int pageNumber = 1,
        int pageSize = 10,
        string? searchTerm = null,
        int? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        try
        {
            _logger.LogInformation("Getting products with pagination. Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            // Build predicate for filtering
            var predicate = BuildProductPredicate(searchTerm, categoryId, minPrice, maxPrice);

            // Get paginated data
            var (items, totalCount) = await _productRepository.GetPagedAsync(
                pageNumber,
                pageSize,
                predicate,
                p => p.Name);

            // Map to DTOs
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(items);

            // Create paginated response
            var paginatedResponse = PaginatedResponse<ProductDto>.Create(
                productDtos.ToList(),
                totalCount,
                pageNumber,
                pageSize);

            _logger.LogInformation("Retrieved {Count} products out of {TotalCount}", productDtos.Count(), totalCount);

            return paginatedResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products");
            throw;
        }
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Getting product by ID: {Id}", id);

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {Id} not found", id);
                return null;
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product by ID: {Id}", id);
            throw;
        }
    }

    public async Task<ProductDto?> GetProductBySkuAsync(string sku)
    {
        try
        {
            _logger.LogInformation("Getting product by SKU: {Sku}", sku);

            var product = await _productRepository.GetBySkuAsync(sku);
            if (product == null)
            {
                _logger.LogWarning("Product with SKU {Sku} not found", sku);
                return null;
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product by SKU: {Sku}", sku);
            throw;
        }
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        try
        {
            _logger.LogInformation("Creating new product: {Name}", createProductDto.Name);

            // Validate SKU uniqueness
            if (!await IsSkuUniqueAsync(createProductDto.Sku))
            {
                throw new InvalidOperationException($"Product with SKU '{createProductDto.Sku}' already exists");
            }

            // Map to entity
            var product = _mapper.Map<Product>(createProductDto);

            // Add to repository
            var createdProduct = await _productRepository.AddAsync(product);

            // Map to DTO
            var productDto = _mapper.Map<ProductDto>(createdProduct);

            _logger.LogInformation("Successfully created product with ID: {Id}", productDto.Id);

            return productDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating product: {Name}", createProductDto.Name);
            throw;
        }
    }

    public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
    {
        try
        {
            _logger.LogInformation("Updating product with ID: {Id}", id);

            // Get existing product
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException($"Product with ID {id} not found");
            }

            // Validate SKU uniqueness (excluding current product)
            if (!await IsSkuUniqueAsync(updateProductDto.Sku, id))
            {
                throw new InvalidOperationException($"Product with SKU '{updateProductDto.Sku}' already exists");
            }

            // Map updates to existing entity
            _mapper.Map(updateProductDto, existingProduct);

            // Update in repository
            _productRepository.Update(existingProduct);

            // Map to DTO
            var productDto = _mapper.Map<ProductDto>(existingProduct);

            _logger.LogInformation("Successfully updated product with ID: {Id}", id);

            return productDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating product with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {Id} not found for deletion", id);
                return false;
            }

            await _productRepository.RemoveAsync(id);

            _logger.LogInformation("Successfully deleted product with ID: {Id}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting product with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        try
        {
            _logger.LogInformation("Getting products by category ID: {CategoryId}", categoryId);

            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products by category ID: {CategoryId}", categoryId);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
    {
        try
        {
            _logger.LogInformation("Getting active products");

            var products = await _productRepository.GetActiveProductsAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting active products");
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetProductsWithLowStockAsync(int threshold = 10)
    {
        try
        {
            _logger.LogInformation("Getting products with low stock (threshold: {Threshold})", threshold);

            var products = await _productRepository.GetProductsWithLowStockAsync(threshold);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products with low stock");
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        try
        {
            _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);

            var products = await _productRepository.SearchProductsAsync(searchTerm);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching products with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        try
        {
            _logger.LogInformation("Getting products by price range: {MinPrice} - {MaxPrice}", minPrice, maxPrice);

            var products = await _productRepository.GetProductsByPriceRangeAsync(minPrice, maxPrice);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products by price range");
            throw;
        }
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantity)
    {
        try
        {
            _logger.LogInformation("Updating stock for product ID: {ProductId}, quantity: {Quantity}", productId, quantity);

            var result = await _productRepository.UpdateStockAsync(productId, quantity);

            if (result)
            {
                _logger.LogInformation("Successfully updated stock for product ID: {ProductId}", productId);
            }
            else
            {
                _logger.LogWarning("Failed to update stock for product ID: {ProductId}", productId);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating stock for product ID: {ProductId}", productId);
            throw;
        }
    }

    public async Task<ProductStatistics> GetProductStatisticsAsync()
    {
        try
        {
            _logger.LogInformation("Getting product statistics");

            var statistics = await _productRepository.GetProductStatisticsAsync();

            return statistics;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product statistics");
            throw;
        }
    }

    public async Task<bool> IsSkuUniqueAsync(string sku, int? excludeId = null)
    {
        try
        {
            var existingProduct = await _productRepository.GetBySkuAsync(sku);
            
            if (existingProduct == null)
            {
                return true; // SKU is unique
            }

            // If excludeId is provided, check if it's the same product
            if (excludeId.HasValue && existingProduct.Id == excludeId.Value)
            {
                return true; // SKU is unique for this product
            }

            return false; // SKU already exists
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking SKU uniqueness: {Sku}", sku);
            throw;
        }
    }

    /// <summary>
    /// Build predicate for product filtering
    /// </summary>
    private static System.Linq.Expressions.Expression<Func<Product, bool>>? BuildProductPredicate(
        string? searchTerm,
        int? categoryId,
        decimal? minPrice,
        decimal? maxPrice)
    {
        if (string.IsNullOrEmpty(searchTerm) && !categoryId.HasValue && !minPrice.HasValue && !maxPrice.HasValue)
        {
            return null; // No filtering needed
        }

        return product =>
            (string.IsNullOrEmpty(searchTerm) || 
             product.Name.ToLower().Contains(searchTerm.ToLower()) ||
             product.Description.ToLower().Contains(searchTerm.ToLower()) ||
             product.Sku.ToLower().Contains(searchTerm.ToLower())) &&
            (!categoryId.HasValue || product.CategoryId == categoryId.Value) &&
            (!minPrice.HasValue || product.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || product.Price <= maxPrice.Value);
    }
} 