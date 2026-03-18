using ProductCQRS.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<AppSettingProfile>(
    builder.Configuration.GetSection("AppSettings"));

builder.Services.Configure<AdminProfile>(
    builder.Configuration.GetSection("AdminProfile"));

builder.Services.Configure<PaginationProfile>(
    builder.Configuration.GetSection("PaginationProfile"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
