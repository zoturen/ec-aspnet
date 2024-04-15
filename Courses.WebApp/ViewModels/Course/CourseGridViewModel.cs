namespace Courses.WebApp.ViewModels.Course;

public class CourseGridViewModel
{
    public List<PreviewCourseViewModel> PreviewCourseItems { get; set; } = [];
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}