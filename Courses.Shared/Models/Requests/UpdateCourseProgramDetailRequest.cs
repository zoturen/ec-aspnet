namespace Courses.Shared.Models.Requests;

public class UpdateCourseProgramDetailRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Step { get; set; }
}