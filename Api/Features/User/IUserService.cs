using Api.Models;
using ErrorOr;

namespace Api.Features.User;

public interface IUserService
{
    public Task<ErrorOr<ApplicationUser>> GetMe(string userId);
}
