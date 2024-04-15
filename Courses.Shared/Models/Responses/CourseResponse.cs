namespace Courses.Shared.Models.Responses;

public class CourseResponse
{
    public int Id { get; set; }
    public string[] Tags { get; set; } = null!;
    public string[] Topics { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal ReviewCount { get; set; }
    public int StarsCount { get; set; }
    public decimal LikesCount { get; set; }
    public decimal HoursOfContent { get; set; }
    public string Author { get; set; } = null!;
    public int ArticleCount { get; set; } 
    public int DownloadableResourcesCount { get; set; } 
    public bool LifetimeAccess { get; set; }
    public bool CertificateOfCompletion { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public List<CourseDetailResponse> CourseDetails { get; set; }
}