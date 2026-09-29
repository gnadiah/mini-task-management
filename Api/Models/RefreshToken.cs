using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class RefreshToken : BaseEntity
{
    public required Guid UserId { get; set; }
    public virtual required ApplicationUser User { get; set; }
    
    public required string RefreshTokenValue { get; set; }

    public required DateTime ExpiresAt { get; set; }
}