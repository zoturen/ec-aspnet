using Microsoft.AspNetCore.Identity;

namespace Courses.Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Bio { get; set; }
    public string AvatarUrl { get; set; }
    public UserAddressEntity? Address { get; set; }
}