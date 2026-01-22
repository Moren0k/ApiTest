using ApiTest.Infrastructure.Health;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Api.Controllers;

[ApiController]
[Route("health")]
public sealed class HealthController : ControllerBase
{
    private readonly IDatabaseHealth _dbHealth;

    public HealthController(IDatabaseHealth dbHealth)
    {
        _dbHealth = dbHealth;
    }

    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabase()
    {
        var result = await _dbHealth.HealthAsync();

        if (!result.IsHealthy)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, result);
        }

        return Ok(result);
    }
}