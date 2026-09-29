using System.ComponentModel.DataAnnotations;

namespace Api.Features.Auth.Dtos;

public class LoginReqDto
{
    [Required]
    public required string UserName { get; set; }
    
    [Required]
    public required string Password { get; set; }
}