namespace Courses.Infrastructure.Dtos;

public class CreateUserAddressDto
{
    public string UserId { get; set; } = null!;
    public string Line1 { get; set; } = null!;
    public string? Line2 { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}