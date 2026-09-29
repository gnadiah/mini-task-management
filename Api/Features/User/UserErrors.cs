using ErrorOr;

namespace Api.Features.User;

public static class UserErrors
{
    public static Error NotFound => Error.Custom(
        type: StatusCodes.Status404NotFound,
        code: "user-not-found",
        description: "The requested user does not exist.",
        metadata: new Dictionary<string, object> { ["title"] = "User not found" });
}
