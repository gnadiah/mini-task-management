using Api.Features.Auth.Dtos;
using Api.Models;
using Riok.Mapperly.Abstractions;

namespace Api.Features.Auth;

[Mapper]
public static partial class AuthMapper
{
    public static partial ApplicationUser ToUserApplication(RegisterReqDto dto);

    public static LoginResDto ToLoginResDto((string AccessToken, string RefreshToken) tokens) => new()
    {
        AccessToken = tokens.AccessToken,
        RefreshToken = tokens.RefreshToken 
    };

    public static RefreshResDto ToRefreshResDto((string AccessToken, string RefreshToken) tokens) => new()
    {
        AccessToken = tokens.AccessToken,
        RefreshToken = tokens.RefreshToken
    };
}