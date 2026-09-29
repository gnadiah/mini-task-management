namespace Api.Features.Auth.Dtos;

public class RefreshResDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}