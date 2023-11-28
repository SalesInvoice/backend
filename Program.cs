using SalesInvoiceProcess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Database Context
builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlServer(connectionString));

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Enable controllers
builder.Services.AddControllers();

var app = builder.Build();

// Use CORS
app.UseCors();

// Controllers
app.MapControllers();

app.Run();