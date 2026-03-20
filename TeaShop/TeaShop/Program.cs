using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeaShop.Data;
using TeaShop.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore.Sqlite;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Ensure SQLite native provider is initialized before any EF Core/SQLite usage
SQLitePCL.Batteries.Init();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tea.db"));

builder.Services.AddSingleton<ISimpleMapper, SimpleMapper>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply any pending EF Core migrations at startup. This will create the
// missing tables when the database was created previously without the
// current model (avoids 'no such table' errors). Requires migrations to
// be created during development (dotnet ef migrations add ...).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // In Development, ensure a clean database that matches the current model so
    // the API works immediately (useful for testing with Postman). This will
    // delete any existing database and recreate it from the model.
    if (app.Environment.IsDevelopment())
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
    else
    {
        // In non-development environments apply migrations (recommended for prod).
        db.Database.Migrate();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
