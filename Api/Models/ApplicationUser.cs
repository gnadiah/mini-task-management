using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Api.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    [PersonalData]
    [Column(TypeName = "date")]
    public DateOnly DateOfBirth { get; set; }
}