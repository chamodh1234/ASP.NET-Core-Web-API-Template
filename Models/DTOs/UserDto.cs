namespace WebApiTemplate.Models.DTOs;

/// <summary>
/// User DTO for API responses
/// </summary>
public class UserDto
{
    /// <summary>
    /// User ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// User name
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Whether the user is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Whether email is confirmed
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Date when the user was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date when the user was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// User roles
    /// </summary>
    public List<string> Roles { get; set; } = new();
}

/// <summary>
/// DTO for creating a new user
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// User name
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// User roles
    /// </summary>
    public List<string> Roles { get; set; } = new();
}

/// <summary>
/// DTO for updating an existing user
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Whether the user is active
    /// </summary>
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for user login
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Email or username
    /// </summary>
    public string EmailOrUsername { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Whether to remember the user
    /// </summary>
    public bool RememberMe { get; set; }
}

/// <summary>
/// DTO for authentication response
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// Whether authentication was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// JWT access token
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Refresh token
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Token expiration time
    /// </summary>
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// User information
    /// </summary>
    public UserDto? User { get; set; }

    /// <summary>
    /// Error message if authentication failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// DTO for refresh token request
/// </summary>
public class RefreshTokenDto
{
    /// <summary>
    /// Refresh token
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// DTO for change password request
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Current password
    /// </summary>
    public string CurrentPassword { get; set; } = string.Empty;

    /// <summary>
    /// New password
    /// </summary>
    public string NewPassword { get; set; } = string.Empty;

    /// <summary>
    /// Confirm new password
    /// </summary>
    public string ConfirmNewPassword { get; set; } = string.Empty;
} 