using Rare.Generator.Server.Services;

namespace Rare.Generator.Server;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(
            opt => opt.AddPolicy("CorsPolicy", 
                bld => bld.WithOrigins("https://localhost:5003", "http://localhost:5002")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));
        builder.Services.AddGrpc();

        var app = builder.Build();

        app.MapGrpcService<RareGeneratorService>();
        app.UseCors("CorsPolicy");
        app.Run();
    }
}