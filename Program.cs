using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WebApiTemplate.Data;
using WebApiTemplate.Models.Entities;
using WebApiTemplate.Repositories;
using WebApiTemplate.Repositories.Interfaces;
using WebApiTemplate.Services;
using WebApiTemplate.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Starting Web API Template application");

    // Add services to the container
    ConfigureServices(builder.Services, builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline
    ConfigureMiddleware(app);

    // Seed database
    await SeedDatabaseAsync(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

/// <summary>
/// Configure application services
/// </summary>
static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Add database context
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("WebApiTemplate")));

    // Add Identity
    services.AddIdentity<User, IdentityRole>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

    // Add JWT Authentication
    var jwtSettings = configuration.GetSection("JwtSettings");
    var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

    // Add Authorization
    services.AddAuthorization(options =>
    {
        options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        options.AddPolicy("User", policy => policy.RequireRole("User", "Admin"));
    });

    // Add AutoMapper
    services.AddAutoMapper(typeof(Program));

    // Add FluentValidation
    services.AddFluentValidationAutoValidation();

    // Add API Versioning
    services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    });

    services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // Add Rate Limiting
    services.AddRateLimiter(options =>
    {
        options.AddFixedWindowLimiter("FixedWindow", limiterOptions =>
        {
            limiterOptions.PermitLimit = 100;
            limiterOptions.Window = TimeSpan.FromMinutes(1);
            limiterOptions.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            limiterOptions.QueueLimit = 2;
        });

        options.AddSlidingWindowLimiter("SlidingWindow", limiterOptions =>
        {
            limiterOptions.PermitLimit = 50;
            limiterOptions.Window = TimeSpan.FromSeconds(30);
            limiterOptions.SegmentsPerWindow = 3;
            limiterOptions.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            limiterOptions.QueueLimit = 2;
        });
    });

    // Add Response Compression
    services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
        options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
    });

    // Add CORS
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
            var allowedMethods = configuration.GetSection("Cors:AllowedMethods").Get<string[]>() ?? new[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" };
            var allowedHeaders = configuration.GetSection("Cors:AllowedHeaders").Get<string[]>() ?? new[] { "*" };
            var allowCredentials = configuration.GetValue<bool>("Cors:AllowCredentials", true);

            policy.WithOrigins(allowedOrigins)
                  .WithMethods(allowedMethods)
                  .WithHeaders(allowedHeaders)
                  .AllowCredentials(allowCredentials);
        });
    });

    // Add Health Checks
    services.AddHealthChecks()
        .AddDbContextCheck<ApplicationDbContext>("Database")
        .AddCheck("Self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());

    // Add Redis Cache
    services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = configuration.GetConnectionString("RedisConnection");
        options.InstanceName = configuration.GetValue<string>("Redis:InstanceName", "WebApiTemplate:");
    });

    // Add SignalR
    services.AddSignalR();

    // Add Controllers
    services.AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

    // Add Swagger/OpenAPI
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Web API Template",
            Version = "v1",
            Description = "A comprehensive ASP.NET Core Web API template for students",
            Contact = new OpenApiContact
            {
                Name = "Your Name",
                Email = "your.email@example.com"
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        });

        // Add JWT Authentication to Swagger
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        // Include XML comments
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }

        // Add operation filters for API versioning
        options.OperationFilter<Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer.ApiVersionOperationFilter>();
    });

    // Register Repositories
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddScoped<IProductRepository, ProductRepository>();

    // Register Services
    services.AddScoped<IProductService, ProductService>();

    // Add HTTP Context Accessor
    services.AddHttpContextAccessor();

    // Add Problem Details
    services.AddProblemDetails();
}

/// <summary>
/// Configure middleware pipeline
/// </summary>
static void ConfigureMiddleware(WebApplication app)
{
    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Template v1");
            options.RoutePrefix = string.Empty; // Serve Swagger UI at root
            options.DocumentTitle = "Web API Template - API Documentation";
            options.InjectStylesheet("/swagger-ui/custom.css");
        });

        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    // Use HTTPS redirection
    app.UseHttpsRedirection();

    // Use CORS
    app.UseCors("CorsPolicy");

    // Use Response Compression
    app.UseResponseCompression();

    // Use Rate Limiting
    app.UseRateLimiter();

    // Use Authentication & Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    // Use Serilog Request Logging
    app.UseSerilogRequestLogging();

    // Map Health Checks
    app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(x => new
                {
                    name = x.Key,
                    status = x.Value.Status.ToString(),
                    description = x.Value.Description
                })
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    });

    // Map SignalR Hub
    app.MapHub<WebApiTemplate.Hubs.NotificationHub>("/notificationHub");

    // Map Controllers
    app.MapControllers();

    Log.Information("Application configured successfully");
}

/// <summary>
/// Seed database with initial data
/// </summary>
static async Task SeedDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    try
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        // Seed admin user
        var adminUser = await userManager.FindByEmailAsync("admin@webapitemplate.com");
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = "admin@webapitemplate.com",
                Email = "admin@webapitemplate.com",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Log.Information("Admin user created successfully");
            }
            else
            {
                Log.Warning("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // Seed regular user
        var regularUser = await userManager.FindByEmailAsync("user@webapitemplate.com");
        if (regularUser == null)
        {
            regularUser = new User
            {
                UserName = "user@webapitemplate.com",
                Email = "user@webapitemplate.com",
                FirstName = "Regular",
                LastName = "User",
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(regularUser, "User123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(regularUser, "User");
                Log.Information("Regular user created successfully");
            }
            else
            {
                Log.Warning("Failed to create regular user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        Log.Information("Database seeded successfully");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while seeding the database");
    }
}
