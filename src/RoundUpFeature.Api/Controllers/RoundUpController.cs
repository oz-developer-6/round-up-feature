using Microsoft.AspNetCore.Mvc;
using RoundUpFeature.Api.StarlingService;

namespace RoundUpFeature.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoundUpController : ControllerBase
{
    private readonly ILogger<RoundUpController> _logger;
    private readonly IApiClient _starlingApiClient;

    public RoundUpController(
        ILogger<RoundUpController> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _starlingApiClient = apiClient;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        _logger.LogInformation("Rounding up transactions");
        return Ok();
    }
}
