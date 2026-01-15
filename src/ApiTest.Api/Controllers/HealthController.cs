using ApiTest.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly DatabaseHealthChecker _dbHealthChecker;

    public HealthController(DatabaseHealthChecker dbHealthChecker)
    {
        _dbHealthChecker = dbHealthChecker;
    }

    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabase()
    {
        var canConnect = await _dbHealthChecker.CanConnectAsync();

        if (!canConnect)
        {
            return StatusCode(503, new {status = "unhealthy", dependency = "MySQL"});
        }
        
        return Ok(new {status = "healthy", dependency = "MySQL"});
    }
}