using Api.Models;

namespace Api.Features.Auth;

public interface ITokenService
{
    public string GenerateAccessToken(ApplicationUser user);
    public (string RawToken, string TokenHash) GenerateRefreshToken();
    public string HashToken(string rawToken);
}