using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Rare.Generator.Server.Services;

public class RareGeneratorService : RareGenerator.RareGeneratorBase
{
    private readonly Random _random = new();

    public override Task<GenerateReply> Generate(GenerateRequest request, ServerCallContext context)
    {
        var rareDct = new List<(int count, string name)>(request.RareChances.Count);

        var sum = 0;
        foreach (var item in request.RareChances)
        {
            if (item.Count == 0) continue;

            sum += item.Count;
            rareDct.Add((item.Count, item.Name));
        }

        if (request.RareChances.Count == 0 || sum == 0)
            return Task.FromResult(new GenerateReply
            {
                Message = string.Empty
            });

        var numbResult = _random.Next(0, sum) + 1;

        foreach (var item in rareDct)
        {
            numbResult -= item.count;

            if (numbResult <= 0)
            {
                return Task.FromResult(new GenerateReply
                {
                    Message = item.name 
                });
            }

        }

        return Task.FromResult(new GenerateReply
        {
            Message = string.Empty
        });
    }
}
