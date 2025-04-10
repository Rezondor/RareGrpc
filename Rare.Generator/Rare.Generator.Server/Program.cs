using Rare.Generator.Server.Services;

namespace Rare.Generator.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc();

        var app = builder.Build();

        app.MapGrpcService<RareGeneratorService>();
        
        app.Run();
    }
}