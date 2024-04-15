namespace Courses.Shared.Models.Responses;

public class CoursesResponse
{
    public int Id { get; set; }
    public string[] Tags { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal ReviewCount { get; set; }
    public int StarsCount { get; set; }
    public decimal LikesCount { get; set; }
    public decimal HoursOfContent { get; set; }
    public string Author { get; set; } = null!;
    public bool IsBookmarked { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
}