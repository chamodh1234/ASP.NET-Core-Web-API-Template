using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTemplate.Models.Entities;

/// <summary>
/// Product entity representing a product in the system
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Name of the product
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the product
    /// </summary>
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Price of the product
    /// </summary>
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    /// <summary>
    /// Stock quantity of the product
    /// </summary>
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be greater than or equal to 0")]
    public int StockQuantity { get; set; }

    /// <summary>
    /// SKU (Stock Keeping Unit) of the product
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Whether the product is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Category ID (foreign key)
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Navigation property for the category
    /// </summary>
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; } = null!;

    /// <summary>
    /// Collection of order items for this product
    /// </summary>
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
} 