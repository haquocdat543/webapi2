using Microsoft.AspNetCore.Mvc;
using Module.User.DTOs;
using Module.User.Services;

namespace Module.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdUser = await _userService.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool isAuthenticated = await _userService.LoginAsync(dto);

        if (!isAuthenticated)
            return Unauthorized();

        return Ok(true); // or Ok() if you prefer no body
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDTO dto)
    {
        var deleted = await _userService.DeleteUserAsync(dto);
        if (!deleted)
            return BadRequest(deleted);

        return Ok(true);
    }
}

