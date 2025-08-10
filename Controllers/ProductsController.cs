using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiTemplate.Models.Common;
using WebApiTemplate.Models.DTOs;
using WebApiTemplate.Services.Interfaces;

namespace WebApiTemplate.Controllers;

/// <summary>
/// Products API controller
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
[SwaggerTag("Product management operations")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
        IProductService productService,
        ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get all products with pagination and filtering
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10, max: 50)</param>
    /// <param name="searchTerm">Search term for product name, description, or SKU</param>
    /// <param name="categoryId">Filter by category ID</param>
    /// <param name="minPrice">Minimum price filter</param>
    /// <param name="maxPrice">Maximum price filter</param>
    /// <returns>Paginated list of products</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all products",
        Description = "Retrieves a paginated list of products with optional filtering and search capabilities")]
    [SwaggerResponse(200, "Products retrieved successfully", typeof(PaginatedResponse<ProductDto>))]
    [SwaggerResponse(400, "Invalid request parameters")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<ProductDto>>>> GetProducts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null)
    {
        try
        {
            _logger.LogInformation("Getting products. Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            // Validate parameters
            if (pageNumber < 1)
            {
                return BadRequest(ApiResponse<PaginatedResponse<ProductDto>>.Error("Page number must be greater than 0"));
            }

            if (pageSize < 1 || pageSize > 50)
            {
                return BadRequest(ApiResponse<PaginatedResponse<ProductDto>>.Error("Page size must be between 1 and 50"));
            }

            if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
            {
                return BadRequest(ApiResponse<PaginatedResponse<ProductDto>>.Error("Minimum price cannot be greater than maximum price"));
            }

            var products = await _productService.GetProductsAsync(
                pageNumber,
                pageSize,
                searchTerm,
                categoryId,
                minPrice,
                maxPrice);

            var response = ApiResponse<PaginatedResponse<ProductDto>>.Success(products, "Products retrieved successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products");
            return StatusCode(500, ApiResponse<PaginatedResponse<ProductDto>>.Error("An error occurred while retrieving products"));
        }
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product details</returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get product by ID",
        Description = "Retrieves a specific product by its unique identifier")]
    [SwaggerResponse(200, "Product retrieved successfully", typeof(ProductDto))]
    [SwaggerResponse(404, "Product not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetProduct(int id)
    {
        try
        {
            _logger.LogInformation("Getting product by ID: {Id}", id);

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(ApiResponse<ProductDto>.Error("Product not found", statusCode: 404));
            }

            var response = ApiResponse<ProductDto>.Success(product, "Product retrieved successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product with ID: {Id}", id);
            return StatusCode(500, ApiResponse<ProductDto>.Error("An error occurred while retrieving the product"));
        }
    }

    /// <summary>
    /// Get product by SKU
    /// </summary>
    /// <param name="sku">Product SKU</param>
    /// <returns>Product details</returns>
    [HttpGet("sku/{sku}")]
    [SwaggerOperation(
        Summary = "Get product by SKU",
        Description = "Retrieves a specific product by its SKU (Stock Keeping Unit)")]
    [SwaggerResponse(200, "Product retrieved successfully", typeof(ProductDto))]
    [SwaggerResponse(404, "Product not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetProductBySku(string sku)
    {
        try
        {
            _logger.LogInformation("Getting product by SKU: {Sku}", sku);

            if (string.IsNullOrWhiteSpace(sku))
            {
                return BadRequest(ApiResponse<ProductDto>.Error("SKU cannot be empty"));
            }

            var product = await _productService.GetProductBySkuAsync(sku);
            if (product == null)
            {
                return NotFound(ApiResponse<ProductDto>.Error("Product not found", statusCode: 404));
            }

            var response = ApiResponse<ProductDto>.Success(product, "Product retrieved successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product with SKU: {Sku}", sku);
            return StatusCode(500, ApiResponse<ProductDto>.Error("An error occurred while retrieving the product"));
        }
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="createProductDto">Product creation data</param>
    /// <returns>Created product</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Create a new product",
        Description = "Creates a new product with the provided information")]
    [SwaggerResponse(201, "Product created successfully", typeof(ProductDto))]
    [SwaggerResponse(400, "Invalid product data")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(409, "Product with SKU already exists")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        try
        {
            _logger.LogInformation("Creating new product: {Name}", createProductDto.Name);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<ProductDto>.Error("Validation failed", errors));
            }

            var product = await _productService.CreateProductAsync(createProductDto);

            var response = ApiResponse<ProductDto>.Success(product, "Product created successfully", 201);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, response);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("SKU"))
        {
            return Conflict(ApiResponse<ProductDto>.Error(ex.Message, statusCode: 409));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating product: {Name}", createProductDto.Name);
            return StatusCode(500, ApiResponse<ProductDto>.Error("An error occurred while creating the product"));
        }
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="updateProductDto">Product update data</param>
    /// <returns>Updated product</returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update an existing product",
        Description = "Updates an existing product with the provided information")]
    [SwaggerResponse(200, "Product updated successfully", typeof(ProductDto))]
    [SwaggerResponse(400, "Invalid product data")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(404, "Product not found")]
    [SwaggerResponse(409, "Product with SKU already exists")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
    {
        try
        {
            _logger.LogInformation("Updating product with ID: {Id}", id);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<ProductDto>.Error("Validation failed", errors));
            }

            var product = await _productService.UpdateProductAsync(id, updateProductDto);

            var response = ApiResponse<ProductDto>.Success(product, "Product updated successfully");
            return Ok(response);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ApiResponse<ProductDto>.Error(ex.Message, statusCode: 404));
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("SKU"))
        {
            return Conflict(ApiResponse<ProductDto>.Error(ex.Message, statusCode: 409));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating product with ID: {Id}", id);
            return StatusCode(500, ApiResponse<ProductDto>.Error("An error occurred while updating the product"));
        }
    }

    /// <summary>
    /// Delete a product (soft delete)
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Success status</returns>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Delete a product",
        Description = "Soft deletes a product (marks as deleted but keeps in database)")]
    [SwaggerResponse(200, "Product deleted successfully")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(404, "Product not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(int id)
    {
        try
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);

            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.Error("Product not found", statusCode: 404));
            }

            var response = ApiResponse<bool>.Success(true, "Product deleted successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting product with ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.Error("An error occurred while deleting the product"));
        }
    }

    /// <summary>
    /// Get products by category
    /// </summary>
    /// <param name="categoryId">Category ID</param>
    /// <returns>List of products in the category</returns>
    [HttpGet("category/{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Get products by category",
        Description = "Retrieves all products belonging to a specific category")]
    [SwaggerResponse(200, "Products retrieved successfully", typeof(List<ProductDto>))]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProductsByCategory(int categoryId)
    {
        try
        {
            _logger.LogInformation("Getting products by category ID: {CategoryId}", categoryId);

            var products = await _productService.GetProductsByCategoryAsync(categoryId);

            var response = ApiResponse<IEnumerable<ProductDto>>.Success(products, "Products retrieved successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products by category ID: {CategoryId}", categoryId);
            return StatusCode(500, ApiResponse<IEnumerable<ProductDto>>.Error("An error occurred while retrieving products"));
        }
    }

    /// <summary>
    /// Get active products
    /// </summary>
    /// <returns>List of active products</returns>
    [HttpGet("active")]
    [SwaggerOperation(
        Summary = "Get active products",
        Description = "Retrieves all active products")]
    [SwaggerResponse(200, "Active products retrieved successfully", typeof(List<ProductDto>))]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetActiveProducts()
    {
        try
        {
            _logger.LogInformation("Getting active products");

            var products = await _productService.GetActiveProductsAsync();

            var response = ApiResponse<IEnumerable<ProductDto>>.Success(products, "Active products retrieved successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting active products");
            return StatusCode(500, ApiResponse<IEnumerable<ProductDto>>.Error("An error occurred while retrieving active products"));
        }
    }

    /// <summary>
    /// Get products with low stock
    /// </summary>
    /// <param name="threshold">Stock threshold (default: 10)</param>
    /// <returns>List of products with low stock</returns>
    [HttpGet("low-stock")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Get products with low stock",
        Description = "Retrieves products that have stock quantity below the specified threshold")]
    [SwaggerResponse(200, "Low stock products retrieved successfully", typeof(List<ProductDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProductsWithLowStock([FromQuery] int threshold = 10)
    {
        try
        {
            _logger.LogInformation("Getting products with low stock (threshold: {Threshold})", threshold);

            if (threshold < 0)
            {
                return BadRequest(ApiResponse<IEnumerable<ProductDto>>.Error("Threshold must be non-negative"));
            }

            var products = await _productService.GetProductsWithLowStockAsync(threshold);

            var response = ApiResponse<IEnumerable<ProductDto>>.Success(products, "Low stock products retrieved successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products with low stock");
            return StatusCode(500, ApiResponse<IEnumerable<ProductDto>>.Error("An error occurred while retrieving low stock products"));
        }
    }

    /// <summary>
    /// Search products
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <returns>List of matching products</returns>
    [HttpGet("search")]
    [SwaggerOperation(
        Summary = "Search products",
        Description = "Searches products by name, description, or SKU")]
    [SwaggerResponse(200, "Search results retrieved successfully", typeof(List<ProductDto>))]
    [SwaggerResponse(400, "Search term is required")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> SearchProducts([FromQuery] string searchTerm)
    {
        try
        {
            _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(ApiResponse<IEnumerable<ProductDto>>.Error("Search term is required"));
            }

            var products = await _productService.SearchProductsAsync(searchTerm);

            var response = ApiResponse<IEnumerable<ProductDto>>.Success(products, "Search results retrieved successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching products with term: {SearchTerm}", searchTerm);
            return StatusCode(500, ApiResponse<IEnumerable<ProductDto>>.Error("An error occurred while searching products"));
        }
    }

    /// <summary>
    /// Update product stock
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to update (positive for increase, negative for decrease)</param>
    /// <returns>Success status</returns>
    [HttpPatch("{id:int}/stock")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update product stock",
        Description = "Updates the stock quantity of a product")]
    [SwaggerResponse(200, "Stock updated successfully")]
    [SwaggerResponse(400, "Invalid quantity")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(404, "Product not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateStock(int id, [FromBody] int quantity)
    {
        try
        {
            _logger.LogInformation("Updating stock for product ID: {Id}, quantity: {Quantity}", id, quantity);

            var result = await _productService.UpdateStockAsync(id, quantity);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.Error("Product not found", statusCode: 404));
            }

            var response = ApiResponse<bool>.Success(true, "Stock updated successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating stock for product ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.Error("An error occurred while updating stock"));
        }
    }
} 