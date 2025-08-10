using FluentValidation;
using WebApiTemplate.Models.DTOs;

namespace WebApiTemplate.Validators;

/// <summary>
/// Validator for CreateProductDto
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters")
            .MinimumLength(2).WithMessage("Product name must be at least 2 characters long");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0")
            .LessThanOrEqualTo(999999.99m).WithMessage("Product price cannot exceed 999,999.99");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative")
            .LessThanOrEqualTo(999999).WithMessage("Stock quantity cannot exceed 999,999");

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Product SKU is required")
            .MaximumLength(50).WithMessage("Product SKU cannot exceed 50 characters")
            .Matches(@"^[A-Z0-9\-_]+$").WithMessage("Product SKU can only contain uppercase letters, numbers, hyphens, and underscores");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Valid category is required");
    }
}

/// <summary>
/// Validator for UpdateProductDto
/// </summary>
public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters")
            .MinimumLength(2).WithMessage("Product name must be at least 2 characters long");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0")
            .LessThanOrEqualTo(999999.99m).WithMessage("Product price cannot exceed 999,999.99");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative")
            .LessThanOrEqualTo(999999).WithMessage("Stock quantity cannot exceed 999,999");

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Product SKU is required")
            .MaximumLength(50).WithMessage("Product SKU cannot exceed 50 characters")
            .Matches(@"^[A-Z0-9\-_]+$").WithMessage("Product SKU can only contain uppercase letters, numbers, hyphens, and underscores");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Valid category is required");
    }
}

/// <summary>
/// Validator for CreateCategoryDto
/// </summary>
public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters")
            .MinimumLength(2).WithMessage("Category name must be at least 2 characters long");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Category description cannot exceed 500 characters");
    }
}

/// <summary>
/// Validator for UpdateCategoryDto
/// </summary>
public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters")
            .MinimumLength(2).WithMessage("Category name must be at least 2 characters long");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Category description cannot exceed 500 characters");
    }
}

/// <summary>
/// Validator for CreateUserDto
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long")
            .Matches(@"^[a-zA-Z0-9._-]+$").WithMessage("Username can only contain letters, numbers, dots, underscores, and hyphens");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("First name can only contain letters and spaces");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Last name can only contain letters and spaces");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of birth cannot be in the future")
            .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Date of birth seems invalid");

        RuleFor(x => x.Roles)
            .Must(roles => roles == null || roles.All(role => !string.IsNullOrWhiteSpace(role)))
            .WithMessage("All roles must be valid");
    }
}

/// <summary>
/// Validator for LoginDto
/// </summary>
public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email or username is required")
            .MaximumLength(100).WithMessage("Email or username cannot exceed 100 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}

/// <summary>
/// Validator for ChangePasswordDto
/// </summary>
public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]").WithMessage("New password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")
            .NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from current password");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithMessage("Password confirmation is required")
            .Equal(x => x.NewPassword).WithMessage("Password confirmation must match new password");
    }
} 