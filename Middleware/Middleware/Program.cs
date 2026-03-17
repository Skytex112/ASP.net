using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Data;
using Middleware.Mapping;
using Middleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<RateLimitMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
