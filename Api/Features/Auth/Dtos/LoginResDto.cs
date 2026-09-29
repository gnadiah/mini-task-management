namespace Api.Features.Auth.Dtos;

public class LoginResDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}