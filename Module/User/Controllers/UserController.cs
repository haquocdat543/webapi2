using Microsoft.AspNetCore.Mvc;

namespace YourProject.Modules.User.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
  [HttpGet]
  public IActionResult GetUsers()
  {
	var users = new[]
	{
			new { Id = 1, Name = "Alice" },
			new { Id = 2, Name = "Bob" },
			new { Id = 3, Name = "Charlie" }
		};
	return Ok(users);
  }
}

