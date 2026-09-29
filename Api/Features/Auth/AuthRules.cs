namespace Api.Constants;

public static class AuthRules
{
    public const string ALLOWED_USERNAME_CHARS =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";

    public const string USERNAME_PATTERN = @"^[a-zA-Z0-9\-_]+$";

    public const string USERNAME_ERROR_MESSAGE =
        "UserName must contain only letters, numbers, dashes and underscores";

    public const string PASSWORD_PATTERN = @"^(?=.*[A-Za-z])(?=.*\d).+$";

    public const string PASSWORD_ERROR_MESSAGE =
        "Password must contain at least one letter and one number";

    public const int MIN_USERNAME_LENGTH = 6;
    public const int MAX_USERNAME_LENGTH = 20;
    public const int MIN_PASSWORD_LENGTH = 8;

    public const int MAX_FAILED_ACCESS_ATTEMPTS = 5;
    public const int LOCKOUT_TIME_SPAN_IN_MINUTES = 5;
}