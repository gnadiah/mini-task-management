using Api.Models;
using ErrorOr;

namespace Api.Features.Auth;

public interface IAuthService
{
    public Task<ErrorOr<ApplicationUser>> Register(ApplicationUser user, string password);
    public Task<ErrorOr<(string AccessToken, string RefreshToken)>> Login(string userName, string password);
    public Task Logout(string refreshToken);
    public Task<ErrorOr<(string AccessToken, string RefreshToken)>> Refresh(string refreshToken);
}
