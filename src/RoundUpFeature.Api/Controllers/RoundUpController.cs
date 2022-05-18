using Microsoft.AspNetCore.Mvc;

namespace RoundUpFeature.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoundUpController : ControllerBase
{
    private readonly ILogger<RoundUpController> _logger;

    public RoundUpController(ILogger<RoundUpController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Get()
    {
        _logger.LogInformation("Rounding up transactions");
        return Ok();
    }
}
