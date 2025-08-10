namespace WebApiTemplate.Models.DTOs;

/// <summary>
/// Category DTO for API responses
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Category ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Category description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Whether the category is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Date when the category was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date when the category was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Number of products in this category
    /// </summary>
    public int ProductCount { get; set; }
}

/// <summary>
/// DTO for creating a new category
/// </summary>
public class CreateCategoryDto
{
    /// <summary>
    /// Category name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Category description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// DTO for updating an existing category
/// </summary>
public class UpdateCategoryDto
{
    /// <summary>
    /// Category name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Category description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Whether the category is active
    /// </summary>
    public bool IsActive { get; set; }
} 