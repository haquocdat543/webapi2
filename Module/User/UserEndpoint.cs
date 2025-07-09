using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace webapi.Module.User;

public static class UserEndpoint
{
  public static void MapUserEndpoint(this IEndpointRouteBuilder app)
  {
	app.MapGet("/user", () =>
	{
	  var users = new[]
		  {
				new User { Id = 1, Name = "Alice" },
				new User { Id = 2, Name = "Bob" }
		};
	  return Results.Ok(users);
	})
	.WithName("GetUsers")
	.WithOpenApi();

	// You can add more endpoints here, like POST, PUT, DELETE etc.
  }
}

