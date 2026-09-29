using System.ComponentModel.DataAnnotations;

namespace Api.Features.Auth.Dtos;

public class LogoutReqDto
{
    [Required]
    public required string RefreshToken { get; set; }
}