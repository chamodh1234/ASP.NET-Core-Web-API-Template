# ASP.NET Core Web API Development Commands
## Complete Reference Guide for Students

### 1. Create .NET Web API Application
```bash
# Basic Web API with minimal APIs (default)
dotnet new webapi -n <ProjectName>

# Web API with controllers (traditional MVC style)
dotnet new webapi -n <ProjectName> --use-controllers

# Web API with authentication
dotnet new webapi -n <ProjectName> -au Individual

# Web API with organizational authentication
dotnet new webapi -n <ProjectName> -au SingleOrg

# Web API without HTTPS (for development)
dotnet new webapi -n <ProjectName> --no-https

# Web API without OpenAPI/Swagger
dotnet new webapi -n <ProjectName> --no-openapi

# Web API with custom output directory
dotnet new webapi -n <ProjectName> -o ./MyCustomFolder
```

### 2. Create Template Controllers
```bash
# Basic controller
dotnet new controller -n <ControllerName>

# Controller with custom output directory
dotnet new controller -n <ControllerName> -o Controllers

# Controller with custom namespace
dotnet new controller -n <ControllerName> --namespace MyApp.Controllers

# Controller with custom route
dotnet new controller -n <ControllerName> --route "api/v1/products"
```

### 3. Create Template Models
```bash
# Basic class (for models, DTOs, entities)
dotnet new class -n <ClassName> -o Models

# Entity model
dotnet new class -n Product -o Models/Entities

# DTO model
dotnet new class -n ProductDto -o Models/DTOs

# View model
dotnet new class -n ProductViewModel -o Models/ViewModels

# Record (immutable data)
dotnet new record -n ProductRecord

# Interface
dotnet new class -n IProductRepository -o Models/Interfaces
```

### 4. Create Template Services
```bash
# Service class
dotnet new class -n ProductService -o Services

# Service interface
dotnet new class -n IProductService -o Services/Interfaces

# Repository class
dotnet new class -n ProductRepository -o Repositories

# Repository interface
dotnet new class -n IProductRepository -o Repositories/Interfaces
```

### 5. Create Template Middleware
```bash
# Middleware class
dotnet new class -n ExceptionHandlingMiddleware -o Middleware

# Custom middleware
dotnet new class -n RequestLoggingMiddleware -o Middleware
```

### 6. Create Template Extensions
```bash
# Extension methods
dotnet new class -n ServiceCollectionExtensions -o Extensions

# Configuration extensions
dotnet new class -n ConfigurationExtensions -o Extensions
```

### 7. Create Template Validators
```bash
# FluentValidation validator
dotnet new class -n CreateProductValidator -o Validators

# Update validator
dotnet new class -n UpdateProductValidator -o Validators
```

### 8. Create Template Tests
```bash
# Unit test project
dotnet new xunit -n <ProjectName>.Tests

# Integration test project
dotnet new xunit -n <ProjectName>.IntegrationTests

# Test class
dotnet new class -n ProductControllerTests -o Tests/Controllers

# Service test class
dotnet new class -n ProductServiceTests -o Tests/Services
```

### 9. Entity Framework Commands
```bash
# Add EF Core packages
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

# Create DbContext
dotnet new class -n ApplicationDbContext -o Data

# Add migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Remove migration
dotnet ef migrations remove

# Generate SQL script
dotnet ef migrations script
```

### 10. SignalR Commands
```bash
# Add SignalR package
dotnet add package Microsoft.AspNetCore.SignalR

# Create SignalR hub
dotnet new class -n NotificationHub -o Hubs
```

### 11. Redis Cache Commands
```bash
# Add Redis package
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis

# Create cache service
dotnet new class -n RedisCacheService -o Services/Cache
```

### 12. Authentication & Authorization
```bash
# Add JWT packages
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package System.IdentityModel.Tokens.Jwt

# Add Identity packages
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.UI
```

### 13. AutoMapper Commands
```bash
# Add AutoMapper package
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

# Create mapping profile
dotnet new class -n MappingProfile -o Mappings
```

### 14. FluentValidation Commands
```bash
# Add FluentValidation package
dotnet add package FluentValidation.AspNetCore

# Create validator
dotnet new class -n CreateProductValidator -o Validators
```

### 15. Serilog Commands
```bash
# Add Serilog packages
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File

# Create logging configuration
dotnet new class -n LoggingConfiguration -o Configuration
```

### 16. Health Checks
```bash
# Add health check packages
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore

# Create health check
dotnet new class -n DatabaseHealthCheck -o HealthChecks
```

### 17. API Versioning
```bash
# Add API versioning package
dotnet add package Microsoft.AspNetCore.Mvc.Versioning

# Create versioned controller
dotnet new controller -n ProductsV1Controller -o Controllers/V1
dotnet new controller -n ProductsV2Controller -o Controllers/V2
```

### 18. Rate Limiting
```bash
# Add rate limiting package
dotnet add package Microsoft.AspNetCore.RateLimiting

# Create rate limiting configuration
dotnet new class -n RateLimitingConfiguration -o Configuration
```

### 19. Background Services
```bash
# Create background service
dotnet new class -n EmailBackgroundService -o BackgroundServices

# Create hosted service
dotnet new class -n DataProcessingHostedService -o BackgroundServices
```

### 20. Common Development Commands
```bash
# Restore packages
dotnet restore

# Build project
dotnet build

# Run project
dotnet run

# Run in watch mode (auto-restart on changes)
dotnet watch run

# Publish for production
dotnet publish -c Release -o ./publish

# Run tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Clean build artifacts
dotnet clean

# Format code
dotnet format

# Analyze code
dotnet analyze
```

### 21. Docker Commands
```bash
# Create Dockerfile
dotnet new dockerfile

# Build Docker image
docker build -t <ImageName> .

# Run Docker container
docker run -p 8080:80 <ImageName>

# Create docker-compose
dotnet new docker-compose
```

### 22. Project Structure Commands
```bash
# Create solution file
dotnet new sln -n <SolutionName>

# Add project to solution
dotnet sln add <ProjectPath>

# Add project reference
dotnet add reference <ProjectPath>

# Add package reference
dotnet add package <PackageName>
```

### 23. Configuration Commands
```bash
# Add configuration package
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables

# Create configuration class
dotnet new class -n AppSettings -o Configuration
```

### 24. CORS Configuration
```bash
# Create CORS configuration
dotnet new class -n CorsConfiguration -o Configuration
```

### 25. Swagger/OpenAPI Configuration
```bash
# Add Swagger packages
dotnet add package Swashbuckle.AspNetCore
dotnet add package Swashbuckle.AspNetCore.Annotations

# Create Swagger configuration
dotnet new class -n SwaggerConfiguration -o Configuration
```

## Notes for Students:
- Always use meaningful names for your classes, methods, and variables
- Follow C# naming conventions (PascalCase for classes, camelCase for variables)
- Use async/await for I/O operations
- Implement proper error handling and logging
- Write unit tests for your business logic
- Use dependency injection for loose coupling
- Follow SOLID principles
- Document your APIs with XML comments
- Use appropriate HTTP status codes
- Implement proper validation
- Use secure authentication and authorization
- Optimize performance with caching
- Monitor your application health