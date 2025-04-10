using Microsoft.AspNetCore.Mvc;

namespace Rare.Client.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RareGeneratorController(RareGenerator.RareGeneratorClient grpcClient) : ControllerBase
    {
        private static readonly Dictionary<string, int> _standardChance = new()
        {
            { "Common", 100},
            { "Rare", 30},
            { "Epic", 10},
            { "Legendary", 3},
            { "Artefact", 1},
        };

        private static readonly List<RareWithChance> _standardRare = _standardChance.Select(x => new RareWithChance
        {
            Name = x.Key,
            Count = x.Value
        }).ToList();

        [HttpGet]
        public async Task<IActionResult> GetRandomRare()
        {
            var req = new GenerateRequest();
            req.RareChances.AddRange(_standardRare);
            var result = await grpcClient.GenerateAsync(req);

            return Ok(result.Message);
        }
    }
}
