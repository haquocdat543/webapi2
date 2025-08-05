using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.User.DTOs;
using Module.User.Services;

namespace Module.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public UserController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
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

    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        var result = await _userService.SeedAsync();
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool isAuthenticated = await _userService.LoginAsync(dto);

        if (!isAuthenticated)
            return Unauthorized();

        var token = _jwtService.GenerateToken(dto.Name);
        return Ok(new { token }); // or Ok() if you prefer no body
    }

    [HttpPost("password")]
    public async Task<IActionResult> Login([FromBody] UpdatePasswordDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool isAuthenticated = await _userService.UpdatePasswordAsync(dto);

        if (!isAuthenticated)
            return Unauthorized();

        return Ok(); // or Ok() if you prefer no body
    }

    [Authorize]
    [HttpPatch()]
    public async Task<IActionResult> PatchUser([FromBody] PatchUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var authHeader = Request.Headers["Authorization"].ToString();
        var username = _jwtService.ExtractUsernameFromBearer(authHeader);

        if (username == null)
            return Forbid();

        var result = await _userService.PatchUserAsync(dto, username);
        if (!result)
            return Unauthorized();

        return Ok(); // or Ok() if you prefer no body
    }

    [Authorize]
    [HttpPut()]
    public async Task<IActionResult> PutUser([FromBody] PutUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var authHeader = Request.Headers["Authorization"].ToString();
        var username = _jwtService.ExtractUsernameFromBearer(authHeader);

        if (username == null)
            return Forbid();

        var result = await _userService.PutUserAsync(dto, username);
        if (!result)
            return Unauthorized();

        return Ok(); // or Ok() if you prefer no body
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

