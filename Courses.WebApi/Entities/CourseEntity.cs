using System.ComponentModel.DataAnnotations;
using Courses.Shared.Models;

namespace Courses.WebApi.Entities;

public class CourseEntity
{
    [Key]
    public int Id { get; set; }

    // Arrays
    public string[] Tags { get; set; } = null!;
    public string[] Topics { get; set; } = null!;

    public Categories Category { get; set; } 
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal ReviewCount { get; set; }
    public int StarsCount { get; set; }
    public decimal LikesCount { get; set; }
    public decimal HoursOfContent { get; set; }
    public string Author { get; set; } = null!;
    public int ArticleCount { get; set; } // Should probably be loaded elsewhere in a real app
    public int DownloadableResourcesCount { get; set; } // All counters --||--
    public bool LifetimeAccess { get; set; }
    public bool CertificateOfCompletion { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }

    public List<CourseProgramDetailEntity> ProgramDetails { get; set; } = [];
}