using Grpc.Net.Client;

namespace Rare.Client.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton(services =>
        {
            var grpcSettings = builder.Configuration.GetSection("GrpcSettings");
            Console.WriteLine(grpcSettings["ServerUrl"]);
            var channel = GrpcChannel.ForAddress(grpcSettings["ServerUrl"] ?? throw new ArgumentNullException("Grpc link is null"), new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 16 * 1024 * 1024, // 16 MB
                MaxSendMessageSize = 16 * 1024 * 1024,
            });

            return new RareGenerator.RareGeneratorClient(channel);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
