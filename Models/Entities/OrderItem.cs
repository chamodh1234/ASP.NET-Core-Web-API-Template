using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTemplate.Models.Entities;

/// <summary>
/// OrderItem entity representing items in an order
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Order ID (foreign key)
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Navigation property for the order
    /// </summary>
    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; } = null!;

    /// <summary>
    /// Product ID (foreign key)
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Navigation property for the product
    /// </summary>
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;

    /// <summary>
    /// Quantity of the product
    /// </summary>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price at the time of order
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Total price for this item (Quantity * UnitPrice)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Discount applied to this item
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; } = 0;

    /// <summary>
    /// Final price after discount
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal FinalPrice { get; set; }
} 