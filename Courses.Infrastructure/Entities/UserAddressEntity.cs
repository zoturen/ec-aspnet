using System.ComponentModel.DataAnnotations;

namespace Courses.Infrastructure.Entities;

public class UserAddressEntity
{
    [Key] 
    public string UserId { get; set; } = null!;
    public string Line1 { get; set; } = null!;
    public string? Line2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public UserEntity? User { get; set; }
}