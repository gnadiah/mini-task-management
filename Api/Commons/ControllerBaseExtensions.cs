using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Api.Commons;

public static class ControllerBaseExtensions
{
    public static ErrorOr<string> GetUserId(this ControllerBase controller)
    {
        var userId = controller.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return userId is null ? CommonErrors.Unauthorized : userId;
    }
}
