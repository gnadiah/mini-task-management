using Api.Features.User.Dtos;
using Api.Models;
using Riok.Mapperly.Abstractions;

namespace Api.Features.User;

[Mapper]
public static partial class UserMapper
{
    public static partial UserResDto ToUserResDto(ApplicationUser user);

    private static string MapDateOfBirth(DateOnly dateOfBirth) => dateOfBirth.ToString("yyyy-MM-dd");
}