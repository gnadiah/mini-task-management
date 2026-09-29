using ErrorOr;

namespace Api.Features.Auth;

public static class AuthErrors
{
    public static Error InvalidCredentials => Error.Custom(
        type: StatusCodes.Status401Unauthorized,
        code: "invalid-credentials",
        description: "The username or password is incorrect.",
        metadata: new Dictionary<string, object> { ["title"] = "Invalid credentials." });

    public static Error EmailAlreadyInUse => Error.Custom(
        type: StatusCodes.Status409Conflict,
        code: "email-already-in-use",
        description: "An account with this email already exists.",
        metadata: new Dictionary<string, object> { ["title"] = "Email already in use." });

    public static Error UserNameAlreadyInUse => Error.Custom(
        type: StatusCodes.Status409Conflict,
        code: "username-already-in-use",
        description: "An account with this username already exists.",
        metadata: new Dictionary<string, object> { ["title"] = "Username already in use." });

    public static Error AccountLocked => Error.Custom(
        type: StatusCodes.Status423Locked,
        code: "account-locked",
        description: "Too many failed attempts. Try again later.",
        metadata: new Dictionary<string, object> { ["title"] = "Account locked." });

    public static Error IdentityCreationFailed(string description) => Error.Custom(
        type: StatusCodes.Status400BadRequest,
        code: "identity-creation-failed",
        description: description,
        metadata: new Dictionary<string, object> { ["title"] = "Registration failed." });

    public static Error RefreshTokenInvalid => Error.Custom(
        type: StatusCodes.Status401Unauthorized,
        code: "refresh-token-invalid",
        description: "The refresh token is invalid.",
        metadata: new Dictionary<string, object> { ["title"] = "Refresh token invalid." });
    
    public static Error RefreshTokenExpired => Error.Custom(
        type: StatusCodes.Status401Unauthorized,
        code: "refresh-token-expired",
        description: "The refresh token is expired.",
        metadata: new Dictionary<string, object> { ["title"] = "Refresh token expired." });
}
