using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTemplate.Models.Entities;

/// <summary>
/// Base entity class that provides common properties for all entities
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primary key for the entity
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Date when the entity was created
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the entity was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Soft delete flag
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// User who created the entity
    /// </summary>
    [MaxLength(100)]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// User who last updated the entity
    /// </summary>
    [MaxLength(100)]
    public string? UpdatedBy { get; set; }
} 