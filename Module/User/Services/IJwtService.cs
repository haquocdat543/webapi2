namespace Module.User.Services;

public interface IJwtService
{
    string GenerateToken(string username);
}
