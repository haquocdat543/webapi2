using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Module.User.Services;

public class JwtService : IJwtService
{
  private readonly IConfiguration _config;

  public JwtService(IConfiguration config)
  {
	_config = config;
  }

  public string GenerateToken(string username)
  {
	var jwtSettings = _config.GetSection("JwtSettings");
	var keyBytes = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key missing"));
	var key = new SymmetricSecurityKey(keyBytes); // ✅ wrap the bytes
	var expireBytes = Encoding.UTF8.GetBytes(jwtSettings["ExpiresInMinutes"] ?? throw new InvalidOperationException("JWT ExpiresInMinutes missing"));
	var expire = new SymmetricSecurityKey(expireBytes); // ✅ wrap the bytes


	var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

	var claims = new[]
	{
			new Claim(JwtRegisteredClaimNames.Sub, username),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

	var token = new JwtSecurityToken(
		issuer: jwtSettings["Issuer"],
		audience: jwtSettings["Audience"],
		claims: claims,
		expires: DateTime.UtcNow.AddMinutes(double.Parse(expireBytes)),
		signingCredentials: creds
	);

	return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
