using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ApiTest.Infrastructure.Persistence;
using ApiTest.Infrastructure.Persistence.Health;

var builder = WebApplication.CreateBuilder(args);

// =============================================================
// CONFIG: DATABASE
// =============================================================

if (builder.Environment.IsDevelopment())
{
    Env.Load("../../.env"); // LOAD .env ONLY IN DEVELOPMENT
}

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString =
    $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)));
});

// =============================================================
// DEPENDENCY INJECTION: SERVICES
// =============================================================
builder.Services.AddScoped<DatabaseHealthChecker>();

// =============================================================
// DEPENDENCY INJECTION: REPOSITORIES
// =============================================================

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();