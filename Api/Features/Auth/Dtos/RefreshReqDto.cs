using System.ComponentModel.DataAnnotations;

namespace Api.Features.Auth.Dtos;

public class RefreshReqDto
{
    [Required]
    public required string RefreshToken { get; set; }
}