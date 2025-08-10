namespace WebApiTemplate.Models.DTOs;

/// <summary>
/// Product DTO for API responses
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Product ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Stock quantity
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Product SKU
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Whether the product is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Category information
    /// </summary>
    public CategoryDto? Category { get; set; }

    /// <summary>
    /// Date when the product was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date when the product was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// DTO for creating a new product
/// </summary>
public class CreateProductDto
{
    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Stock quantity
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Product SKU
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Category ID
    /// </summary>
    public int CategoryId { get; set; }
}

/// <summary>
/// DTO for updating an existing product
/// </summary>
public class UpdateProductDto
{
    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Stock quantity
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Product SKU
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Whether the product is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Category ID
    /// </summary>
    public int CategoryId { get; set; }
}

/// <summary>
/// DTO for product list with pagination
/// </summary>
public class ProductListDto
{
    /// <summary>
    /// List of products
    /// </summary>
    public List<ProductDto> Products { get; set; } = new();

    /// <summary>
    /// Total count of products
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Current page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Whether there is a next page
    /// </summary>
    public bool HasNextPage { get; set; }

    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    public bool HasPreviousPage { get; set; }
} 