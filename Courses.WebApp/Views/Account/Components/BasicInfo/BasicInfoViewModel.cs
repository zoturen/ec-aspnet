namespace Courses.WebApp.Views.Account.Components.BasicInfo;

public class BasicInfoViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Bio { get; set; }
}