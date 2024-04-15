namespace Courses.Shared.Models.Requests;

public class CreateCourseProgramDetailRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Step { get; set; }
}