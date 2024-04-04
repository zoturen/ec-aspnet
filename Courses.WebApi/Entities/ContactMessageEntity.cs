using System.ComponentModel.DataAnnotations;

namespace Courses.WebApi.Entities;

public class ContactMessageEntity
{
    [Key]
    public int Id { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string? Service { get; set; }
    public string Message { get; set; } = null!;

}