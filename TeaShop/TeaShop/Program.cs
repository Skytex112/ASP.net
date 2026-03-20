using Microsoft.EntityFrameworkCore;
using TeaShop.Data;
using TeaShop.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore.Sqlite;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tea.db"));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();