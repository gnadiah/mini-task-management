using System.ComponentModel.DataAnnotations;
using Api.Constants;

namespace Api.Features.Auth.Dtos;

public class RegisterReqDto
{
    [Required] [EmailAddress] public required string Email { get; set; }

    [Required]
    [MinLength(AuthRules.MIN_USERNAME_LENGTH)]
    [MaxLength(AuthRules.MAX_USERNAME_LENGTH)]
    [RegularExpression(AuthRules.USERNAME_PATTERN, ErrorMessage = AuthRules.USERNAME_ERROR_MESSAGE)]
    public required string UserName { get; set; }

    [Required]
    [MinLength(AuthRules.MIN_PASSWORD_LENGTH)]
    [RegularExpression(AuthRules.PASSWORD_PATTERN, ErrorMessage = AuthRules.PASSWORD_ERROR_MESSAGE)]
    public required string Password { get; set; }

    [Required] public required DateOnly DateOfBirth { get; set; }
}