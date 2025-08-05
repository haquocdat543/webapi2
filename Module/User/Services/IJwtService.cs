namespace Module.User.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username);
        string? ExtractUsername(string token);
        string? ExtractUsernameFromBearer(string authorizationHeader); // ✅ Correct name
    }
}

