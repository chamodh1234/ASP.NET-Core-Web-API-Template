using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Models.Entities;

/// <summary>
/// Category entity representing product categories
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Name of the category
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the category
    /// </summary>
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Whether the category is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Collection of products in this category
    /// </summary>
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
} 