using ApiTest.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize] // requiere JWT
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // ========================
    // GET ALL (Admin)
    // ========================
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    // ========================
    // GET BY ID (Admin)
    // ========================
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // ========================
    // DELETE (Admin)
    // ========================
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}