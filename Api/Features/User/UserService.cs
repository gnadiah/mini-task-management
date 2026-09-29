using Api.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Api.Features.User;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<ApplicationUser>> GetMe(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return UserErrors.NotFound;

        return user;
    }
}
