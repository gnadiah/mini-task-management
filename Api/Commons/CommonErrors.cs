using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace Api.Commons;

public static class CommonErrors
{
    public static Error Unauthorized => Error.Custom(
        type: StatusCodes.Status401Unauthorized,
        code: "unauthorized",
        description: "Access token is missing required claims or expired",
        metadata: new Dictionary<string, object> { ["title"] = "Unauthorized" });
}
