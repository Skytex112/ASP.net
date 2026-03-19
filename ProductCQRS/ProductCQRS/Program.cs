using ProductCQRS.Profiles;
using Serilog;
using System.Threading.RateLimiting;

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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
            "Logs/log.txt",
            rollingInterval: RollingInterval.Day)
    .CreateLogger();

//For appsetting.json(Logger)
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

//rate limited
var rateLimited = builder.Configuration.GetSection("RateLimiting");

var permitLimited = rateLimited.GetValue<int>("PermitLimit");
var windowLimit = rateLimited.GetValue<int>("WindowMinutes");
var queueLimited = rateLimited.GetValue<int>("QueneLimit");

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: "global",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = permitLimited,
                Window = TimeSpan.FromMinutes(windowLimit),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = queueLimited,
                AutoReplenishment = true,
            }));
});



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
