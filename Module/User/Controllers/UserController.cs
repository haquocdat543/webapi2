using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Module.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly UserDbContext _context;

  public UserController(UserDbContext context)
  {
	_context = context;
  }

  [HttpGet]
  public async Task<IActionResult> GetUsers()
  {
	var users = await _context.Users.ToListAsync();
	return Ok(users);
  }

  [HttpPost]
  public async Task<IActionResult> CreateUser([FromBody] Entities.User user)
  {
	_context.Users.Add(user);
	await _context.SaveChangesAsync();
	return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser(int id, [FromBody] Entities.User updatedUser)
  {
	var user = await _context.Users.FindAsync(id);
	if (user == null)
	  return NotFound();

	user.Name = updatedUser.Name;
	await _context.SaveChangesAsync();
	return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id)
  {
	var user = await _context.Users.FindAsync(id);
	if (user == null)
	  return NotFound();

	_context.Users.Remove(user);
	await _context.SaveChangesAsync();
	return NoContent();
  }
}

