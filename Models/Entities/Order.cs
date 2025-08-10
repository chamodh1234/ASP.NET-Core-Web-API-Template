using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTemplate.Models.Entities;

/// <summary>
/// Order entity representing customer orders
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Order number (unique identifier)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string OrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// User ID who placed the order
    /// </summary>
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Navigation property for the user
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Total amount of the order
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Status of the order
    /// </summary>
    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    /// <summary>
    /// Shipping address
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string ShippingAddress { get; set; } = string.Empty;

    /// <summary>
    /// Billing address
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string BillingAddress { get; set; } = string.Empty;

    /// <summary>
    /// Notes for the order
    /// </summary>
    [MaxLength(1000)]
    public string? Notes { get; set; }

    /// <summary>
    /// Date when the order was placed
    /// </summary>
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the order was shipped
    /// </summary>
    public DateTime? ShippedDate { get; set; }

    /// <summary>
    /// Date when the order was delivered
    /// </summary>
    public DateTime? DeliveredDate { get; set; }

    /// <summary>
    /// Collection of order items
    /// </summary>
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

/// <summary>
/// Enum representing order status
/// </summary>
public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Refunded = 6
} 