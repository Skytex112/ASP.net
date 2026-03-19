using WebApplication3.Handlers;
using WebApplication3.Interfaces;
using WebApplication3.Repositories;
using WebApplication3.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddEndpointsApiExplorer();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {


        }

        app.UseHttpsRedirection();

        app.UseExceptionHandler();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}