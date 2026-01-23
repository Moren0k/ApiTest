using System.Text;
using ApiTest.Application.Features.AuthFeatures;
using ApiTest.Application.Features.ImageFeatures;
using ApiTest.Application.Features.UserFeatures;
using ApiTest.Application.IProviders.IExternalServices.ICloudinary;
using ApiTest.Application.IProviders.ISecurity;
using ApiTest.Domain.Entities.Common;
using ApiTest.Domain.Entities.EImage;
using ApiTest.Domain.Entities.EUser;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ApiTest.Infrastructure.Persistence;
using ApiTest.Infrastructure.Providers.ExternalServices.Cloudinary;
using ApiTest.Infrastructure.Providers.HealthChecks;
using ApiTest.Infrastructure.Providers.Security.Jwt;
using ApiTest.Infrastructure.Providers.Security.Password;
using ApiTest.Infrastructure.Repositories.Common;
using ApiTest.Infrastructure.Repositories.Images;
using ApiTest.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load("../../.env"); // LOAD .env ONLY IN DEVELOPMENT
}

// =============================================================
// CONFIG: DATABASE
// =============================================================

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var dbHost = Environment.GetEnvironmentVariable("Db__Host");
    var dbPort = Environment.GetEnvironmentVariable("Dd__Port");
    var dbName = Environment.GetEnvironmentVariable("Dd__Name");
    var dbUser = Environment.GetEnvironmentVariable("Dd__User");
    var dbPassword = Environment.GetEnvironmentVariable("Dd__Password");
    var connectionString =
        $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};";
    
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)));
});

// =============================================================
// DEPENDENCY INJECTION: REPOSITORIES
// =============================================================
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

// =============================================================
// DEPENDENCY INJECTION: PROVIDERS
// =============================================================
builder.Services.AddScoped<ICheckDatabaseProvider, CheckDatabaseProvider>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<ICloudinaryProvider, CloudinaryProvider>();
builder.Services.AddScoped<IPasswordHasherProvider, PasswordHasherProvider>();

// =============================================================
// DEPENDENCY INJECTION: SERVICES
// =============================================================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();

// =============================================================
// CONTROLLERS
// =============================================================
builder.Services.AddControllers();

// =============================================================
// SWAGGER/OPENAPI
// =============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================================
// JWT/SECURITY
// =============================================================
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretKey = Environment.GetEnvironmentVariable("Jwt__Key") ??
                        throw new InvalidOperationException("JWT_KEY not configured");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// =============================================================
// CLOUDINARY
// =============================================================
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection(CloudinarySettings.SectionName));

builder.Services.PostConfigure<CloudinarySettings>(settings =>
{
    if (string.IsNullOrWhiteSpace(settings.CloudName))
        throw new InvalidOperationException("Cloudinary CloudName not configured");

    if (string.IsNullOrWhiteSpace(settings.ApiKey))
        throw new InvalidOperationException("Cloudinary ApiKey not configured");

    if (string.IsNullOrWhiteSpace(settings.ApiSecret))
        throw new InvalidOperationException("Cloudinary ApiSecret not configured");
});

// =============================================================
// CORS
// =============================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// =============================================================
// BUILD APP
// =============================================================
var app = builder.Build();

// =============================================================
// DB CONNECTIVITY CHECK AT STARTUP
// =============================================================
/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        if (db.Database.CanConnect())
        {
            Console.WriteLine("Database connected successfully");
        }
        else
        {
            Console.WriteLine("Database connection failed");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occured: {ex.Message}");
    }
}
*/

// =============================================================
// MIDDLEWARE PIPELINE
// =============================================================
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();