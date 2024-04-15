namespace Courses.WebApp.ViewModels.Course;

public class PreviewCourseViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public bool BestSeller { get; set; }
    public int HoursOfContent { get; set; }
    public int ApprovalRate { get; set; }
    public decimal RateCount { get; set; }
    public bool IsBookmarked { get; set; }
    public string ImageUrl { get; set; } = null!;
}