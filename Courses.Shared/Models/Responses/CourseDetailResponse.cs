namespace Courses.Shared.Models.Responses;

public class CourseDetailResponse
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Step { get; set; }
}