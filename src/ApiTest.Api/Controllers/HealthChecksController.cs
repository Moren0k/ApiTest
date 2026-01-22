using ApiTest.Infrastructure.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Api.Controllers;

[ApiController]
[Route("checks")]
public sealed class HealthChecksController : ControllerBase
{
    private readonly ICheckDatabase _checkDatabase;

    public HealthChecksController(ICheckDatabase checkDatabase)
    {
        _checkDatabase = checkDatabase;
    }

    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabase()
    {
        var result = await _checkDatabase.Check();

        if (!result.IsHealthy)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, result);
        }

        return Ok(result);
    }

    [Authorize]
    [HttpGet("jwt")]
    public IActionResult GetProtectedData()
    {
     return Ok("The token is validated correctly");
    } 
}