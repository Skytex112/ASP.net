using Auth.Repository;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=auth.db"));

// Реєструємо репозиторій з окремого проекту
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();