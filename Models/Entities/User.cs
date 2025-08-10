using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Models.Entities;

/// <summary>
/// User entity extending ASP.NET Core Identity
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// First name of the user
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the user
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Whether the user is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date when the user was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the user was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Full name of the user (computed property)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}".Trim();

    /// <summary>
    /// Collection of orders for this user
    /// </summary>
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
} 