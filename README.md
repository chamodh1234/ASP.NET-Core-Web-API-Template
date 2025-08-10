# ASP.NET Core Web API Template 🚀

[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

A comprehensive, production-ready ASP.NET Core Web API template designed for **educational purposes** and rapid development. This template demonstrates **real-world best practices**, **clean architecture**, and **modern development patterns** that students and developers can reference when building their own applications.

## 🎯 **What Students Will Learn**

This template is specifically designed to teach you:

### **Core Concepts**
- ✅ **Clean Architecture** - Separation of concerns with proper layering
- ✅ **Repository Pattern** - Data access abstraction and unit of work
- ✅ **Dependency Injection** - Loose coupling and testability
- ✅ **Entity Framework Core** - Modern ORM usage with Code-First approach
- ✅ **Authentication & Authorization** - JWT-based security with ASP.NET Core Identity
- ✅ **API Versioning** - Maintaining backward compatibility
- ✅ **Error Handling** - Global exception management and logging
- ✅ **Validation** - Input validation with FluentValidation
- ✅ **AutoMapper** - Object mapping between entities and DTOs
- ✅ **SignalR** - Real-time communication
- ✅ **Redis Caching** - Performance optimization
- ✅ **Rate Limiting** - API protection and throttling
- ✅ **Health Checks** - Application monitoring
- ✅ **Swagger/OpenAPI** - API documentation

### **Advanced Patterns**
- ✅ **CQRS Pattern** - Command Query Responsibility Segregation
- ✅ **Specification Pattern** - Flexible querying
- ✅ **Result Pattern** - Consistent API responses
- ✅ **Pagination** - Efficient data retrieval
- ✅ **Soft Delete** - Data preservation
- ✅ **Audit Trail** - Change tracking
- ✅ **Background Services** - Long-running tasks
- ✅ **Response Compression** - Performance optimization

## 📋 **Table of Contents**

- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Architecture Overview](#architecture-overview)
- [Implementation Guide](#implementation-guide)
- [API Documentation](#api-documentation)
- [Testing Strategy](#testing-strategy)
- [Deployment](#deployment)
- [Best Practices](#best-practices)
- [Common Patterns](#common-patterns)
- [Troubleshooting](#troubleshooting)
- [Additional Resources](#additional-resources)
- [Contributing](#contributing)

## 🎯 **Overview**

This template provides a **solid foundation** for building RESTful Web APIs using ASP.NET Core. It's specifically crafted for **educational environments**, offering clear examples of **industry-standard practices** that students can learn from and adapt to their projects.

### **What Makes This Template Special**

1. **Real-World Patterns** - Not just basic CRUD, but enterprise-level patterns
2. **Comprehensive Documentation** - Every concept is explained with examples
3. **Production Ready** - Includes logging, monitoring, security, and performance
4. **Student Friendly** - Clear comments and explanations throughout the code
5. **Extensible** - Easy to add new features and modify existing ones

## ✨ **Features**

### **Core Functionality**
- ✅ **RESTful API endpoints** with full CRUD operations
- ✅ **Entity Framework Core** with Code-First approach
- ✅ **Repository and Unit of Work** patterns
- ✅ **AutoMapper** for object mapping
- ✅ **FluentValidation** for input validation
- ✅ **JWT Authentication** and Role-based Authorization
- ✅ **API Versioning** support
- ✅ **Swagger/OpenAPI** documentation

### **Advanced Features**
- ✅ **SignalR** for real-time notifications
- ✅ **Redis Cache** for performance optimization
- ✅ **Rate Limiting** for API protection
- ✅ **Health Checks** for monitoring
- ✅ **Background Services** for long-running tasks
- ✅ **Response Compression** for bandwidth optimization
- ✅ **CORS** configuration for cross-origin requests
- ✅ **Structured Logging** with Serilog

### **Development & Production Ready**
- ✅ **Global exception handling** with proper error responses
- ✅ **Structured logging** with Serilog
- ✅ **Health checks** for monitoring
- ✅ **CORS configuration** for frontend integration
- ✅ **Rate limiting** for API protection
- ✅ **Response compression** for performance
- ✅ **Docker support** for containerization
- ✅ **Environment-specific** configurations

### **Testing & Quality**
- ✅ **Unit tests** with xUnit
- ✅ **Integration tests** for API endpoints
- ✅ **Test data builders** for consistent test data
- ✅ **Code coverage** reporting
- ✅ **Static code analysis** ready

## 🔧 **Prerequisites**

Before you begin, ensure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB is sufficient for development)
- [Redis](https://redis.io/download) (for caching - optional)
- [Git](https://git-scm.com/)
- [Postman](https://www.postman.com/) or similar API testing tool (optional)

### **Recommended Extensions (VS Code)**
- C# Dev Kit
- REST Client
- GitLens
- Thunder Client
- Docker

## 🚀 **Getting Started**

### **1. Clone the Repository**

```bash
git clone https://github.com/yourusername/aspnet-webapi-template.git
cd aspnet-webapi-template
```

### **2. Restore Dependencies**

```bash
dotnet restore
```

### **3. Update Database Connection**

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WebApiTemplateDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### **4. Run Database Migrations**

```bash
dotnet ef database update
```

### **5. Run the Application**

```bash
dotnet run
```

The API will be available at:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`
- **Swagger UI:** `https://localhost:5001/swagger`

### **6. Test the API**

1. **Open Swagger UI** at `https://localhost:5001/swagger`
2. **Try the endpoints** - Start with GET `/api/v1/products`
3. **Authenticate** - Use the login endpoint to get a JWT token
4. **Test protected endpoints** - Use the token in the Authorization header

## 📁 **Project Structure**

```
WebApiTemplate/
├── Controllers/              # API Controllers
│   ├── ProductsController.cs
│   ├── CategoriesController.cs
│   └── AuthController.cs
├── Models/                   # Data Models
│   ├── Entities/            # Database entities
│   │   ├── BaseEntity.cs
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── User.cs
│   │   ├── Order.cs
│   │   └── OrderItem.cs
│   ├── DTOs/               # Data Transfer Objects
│   │   ├── ProductDto.cs
│   │   ├── CategoryDto.cs
│   │   └── UserDto.cs
│   └── Common/             # Shared models
│       └── ApiResponse.cs
├── Services/                # Business Logic Layer
│   ├── Interfaces/         # Service interfaces
│   │   └── IProductService.cs
│   └── ProductService.cs   # Service implementations
├── Repositories/            # Data Access Layer
│   ├── Interfaces/         # Repository interfaces
│   │   ├── IGenericRepository.cs
│   │   └── IProductRepository.cs
│   ├── GenericRepository.cs
│   └── ProductRepository.cs
├── Data/                    # Database Context
│   └── ApplicationDbContext.cs
├── Mappings/               # AutoMapper profiles
│   └── MappingProfile.cs
├── Validators/             # FluentValidation validators
│   ├── CreateProductValidator.cs
│   └── UpdateProductValidator.cs
├── Hubs/                   # SignalR hubs
│   └── NotificationHub.cs
├── Middleware/             # Custom middleware
├── Extensions/             # Extension methods
├── Configuration/          # Configuration classes
├── HealthChecks/           # Health check implementations
├── BackgroundServices/     # Background services
├── Common/                 # Shared utilities
├── Exceptions/             # Custom exceptions
├── Filters/                # Action filters
├── Program.cs              # Application entry point
├── appsettings.json        # Configuration
└── README.md              # This file
```

## 🏗️ **Architecture Overview**

This template follows **Clean Architecture** principles:

### **Layers Explanation**

1. **API Layer** (`Controllers/`)
   - HTTP endpoint definitions
   - Request/response handling
   - Cross-cutting concerns (middleware, filters)

2. **Application Layer** (`Services/`)
   - Business logic and use cases
   - Service interfaces and implementations
   - Data transformation (DTOs)

3. **Domain Layer** (`Models/Entities/`)
   - Business entities and core business logic
   - Independent of external concerns
   - Defines interfaces for repositories

4. **Infrastructure Layer** (`Repositories/`, `Data/`)
   - Implements data access using Entity Framework Core
   - External service integrations
   - Repository pattern implementation

### **Dependency Flow**

```
API → Application → Domain
API → Infrastructure → Domain
```

## 📚 **Implementation Guide**

### **Creating Your First Entity**

1. **Define the Entity** in `Models/Entities/`:

```csharp
public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
```

2. **Create Repository Interface** in `Repositories/Interfaces/`:

```csharp
public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
}
```

3. **Implement Repository** in `Repositories/`:

```csharp
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
    }
}
```

4. **Create DTOs** in `Models/DTOs/`:

```csharp
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
```

5. **Create Service** in `Services/`:

```csharp
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
```

6. **Create Controller** in `Controllers/`:

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
}
```

### **Adding Validation**

1. **Create Validator** in `Validators/`:

```csharp
public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
```

2. **Register Validator** in `Program.cs`:

```csharp
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
```

### **Database Migrations**

To add a new migration after changing entities:

```bash
# Add migration
dotnet ef migrations add AddProductEntity

# Update database
dotnet ef database update
```

## 📖 **API Documentation**

### **Authentication**

This API uses JWT Bearer tokens for authentication.

1. **Register a new user:**
```http
POST /api/v1/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

2. **Login to get token:**
```http
POST /api/v1/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password123!"
}
```

3. **Use token in requests:**
```http
GET /api/v1/products
Authorization: Bearer YOUR_JWT_TOKEN_HERE
```

### **Common Endpoints**

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/products` | Get all products (paginated) |
| GET | `/api/v1/products/{id}` | Get product by ID |
| POST | `/api/v1/products` | Create new product |
| PUT | `/api/v1/products/{id}` | Update product |
| DELETE | `/api/v1/products/{id}` | Delete product |
| GET | `/api/v1/products/search?term=phone` | Search products |
| GET | `/api/v1/products/category/{id}` | Get products by category |
| GET | `/api/v1/products/low-stock` | Get products with low stock |
| PATCH | `/api/v1/products/{id}/stock` | Update product stock |

### **Response Format**

All API responses follow a consistent format:

```json
{
  "success": true,
  "data": { ... },
  "message": "Operation completed successfully",
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-01T12:00:00Z"
}
```

Error responses:
```json
{
  "success": false,
  "data": null,
  "message": "Validation failed",
  "errors": [
    "Product name is required",
    "Price must be greater than 0"
  ],
  "statusCode": 400,
  "timestamp": "2024-01-01T12:00:00Z"
}
```

## 🧪 **Testing Strategy**

### **Running Tests**

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test tests/WebApiTemplate.UnitTests
```

### **Test Structure**

```csharp
[Fact]
public async Task GetProductById_WithValidId_ReturnsProduct()
{
    // Arrange
    var product = new Product { Id = 1, Name = "Test Product" };
    _mockRepository.Setup(x => x.GetByIdAsync(1))
                  .ReturnsAsync(product);

    // Act
    var result = await _productService.GetProductByIdAsync(1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Test Product", result.Name);
}
```

### **Integration Tests**

Integration tests are located in `tests/WebApiTemplate.IntegrationTests/` and use a test database:

```csharp
public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProductsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/products");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
```

## 🚢 **Deployment**

### **Docker Deployment**

1. **Build Docker image:**
```bash
docker build -t webapi-template .
```

2. **Run container:**
```bash
docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Production webapi-template
```

### **Environment Variables**

Key environment variables for deployment:

```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=YourProductionConnectionString
JwtSettings__SecretKey=YourSecretKey
JwtSettings__Issuer=YourIssuer
JwtSettings__Audience=YourAudience
Redis__ConnectionString=YourRedisConnectionString
```

### **Azure App Service Deployment**

1. Publish the application:
```bash
dotnet publish -c Release -o ./publish
```

2. Deploy using Azure CLI or Visual Studio publish profile.

## ✅ **Best Practices**

### **Code Organization**

1. **Single Responsibility** - Each class should have one reason to change
2. **Dependency Inversion** - Depend on abstractions, not concretions
3. **Interface Segregation** - Keep interfaces focused and minimal
4. **Open/Closed Principle** - Open for extension, closed for modification

### **API Design**

1. **RESTful URLs** - Use nouns for resources, HTTP verbs for actions
2. **Consistent Naming** - Use camelCase for JSON, PascalCase for C#
3. **Status Codes** - Use appropriate HTTP status codes
4. **Versioning** - Always version your APIs

### **Security**

1. **Authentication** - Always authenticate users
2. **Authorization** - Check permissions for each endpoint
3. **Input Validation** - Validate all input data
4. **HTTPS** - Use HTTPS in production
5. **Secrets** - Never commit secrets to source control

### **Performance**

1. **Async/Await** - Use async programming for I/O operations
2. **Caching** - Implement caching for frequently accessed data
3. **Pagination** - Paginate large result sets
4. **Connection Pooling** - Use connection pooling for database access

## 🔄 **Common Patterns**

### **Repository Pattern**

```csharp
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
```

### **Unit of Work Pattern**

```csharp
public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task<int> SaveChangesAsync();
}
```

### **Result Pattern**

```csharp
public class Result<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new();
}
```

## 🔧 **Troubleshooting**

### **Common Issues**

1. **Database Connection Errors**
   - Check connection string format
   - Ensure SQL Server is running
   - Verify database exists

2. **Migration Issues**
   - Delete migrations folder and recreate
   - Check for model conflicts
   - Ensure DbContext is properly configured

3. **Authentication Issues**
   - Verify JWT configuration
   - Check token expiration
   - Ensure correct authentication scheme

4. **Dependency Injection Errors**
   - Verify service registration in Program.cs
   - Check interface implementations
   - Ensure correct lifetime scopes

### **Debugging Tips**

1. **Enable Detailed Errors** - Set `ASPNETCORE_ENVIRONMENT=Development`
2. **Check Logs** - Review application logs in the console
3. **Use Debugger** - Set breakpoints in your IDE
4. **Test Endpoints** - Use Swagger UI or Postman to test

## 📚 **Additional Resources**

### **Microsoft Documentation**
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Web API Best Practices](https://docs.microsoft.com/en-us/aspnet/core/web-api/)

### **Learning Resources**
- [Clean Architecture by Robert Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Repository Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- [RESTful API Design](https://restfulapi.net/)

### **Tools & Libraries**
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)
- [Serilog](https://serilog.net/)
- [xUnit](https://xunit.net/)

## 🤝 **Contributing**

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

### **How to Contribute**

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

### **Code Style**

- Follow C# naming conventions
- Use meaningful variable and method names
- Add XML documentation for public members
- Keep methods small and focused
- Write tests for new features

## 📄 **License**

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 💬 **Support**

If you have questions or need help:

1. Check the [Issues](https://github.com/yourusername/aspnet-webapi-template/issues) page
2. Create a new issue if your question isn't answered
3. Join our discussions in the [Discussions](https://github.com/yourusername/aspnet-webapi-template/discussions) tab

---

## 🎓 **For Students**

This template is designed to help you learn modern web API development. Here's a suggested learning path:

### **Phase 1: Understanding the Basics**
1. **Start with the project structure** - Understand how files are organized
2. **Run the application** - See it in action
3. **Explore the database** - Look at the entities and relationships
4. **Test the API** - Use Swagger UI to test endpoints

### **Phase 2: Understanding the Architecture**
1. **Follow a request** - Trace a request from controller to database and back
2. **Study the layers** - Understand the separation of concerns
3. **Learn the patterns** - Repository, Service, DTO patterns
4. **Understand dependency injection** - How services are wired together

### **Phase 3: Adding Features**
1. **Add a new entity** - Create a new entity with full CRUD operations
2. **Implement business logic** - Add custom business rules
3. **Add validation** - Implement input validation
4. **Add tests** - Write unit and integration tests

### **Phase 4: Advanced Concepts**
1. **Implement caching** - Add Redis caching
2. **Add real-time features** - Implement SignalR
3. **Add authentication** - Implement custom authentication
4. **Deploy the application** - Deploy to a cloud provider

### **Key Learning Points**

- **Clean Architecture** - How to organize code for maintainability
- **SOLID Principles** - How to write good, testable code
- **API Design** - How to design RESTful APIs
- **Security** - How to implement authentication and authorization
- **Performance** - How to optimize for performance
- **Testing** - How to write effective tests
- **Deployment** - How to deploy to production

### **Remember**

- **Don't just read the code** - Modify it, break it, fix it
- **Ask questions** - If something isn't clear, ask!
- **Practice** - Build your own projects using these patterns
- **Stay curious** - Keep learning and exploring new technologies

**Happy coding! 🚀**
