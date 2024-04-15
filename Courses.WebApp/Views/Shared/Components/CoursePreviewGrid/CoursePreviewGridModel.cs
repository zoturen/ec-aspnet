namespace Courses.WebApp.Views.Shared.Components.CoursePreviewGrid;

public class CoursePreviewGridModel
{
    public string Category { get; set; } = null!;
    public string? Filter { get; set; } 
    public int ItemCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}