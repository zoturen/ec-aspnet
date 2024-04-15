using Courses.Shared.Models.Requests;
using Courses.Shared.Models.Responses;
using Courses.WebApi.Entities;

namespace Courses.WebApi.Mappers;

public static class CourseMapper
{
    public static CourseEntity Map(this CreateCourseRequest request)
    {
        return new CourseEntity
        {
            Tags = request.Tags, 
            Topics = request.Topics, 
            Title = request.Title,
            ImageUrl = request.ImageUrl,
            SubTitle = request.SubTitle,
            Description = request.Description,
            ReviewCount = request.ReviewCount,
            StarsCount = request.StarsCount,
            LikesCount = request.LikesCount,
            HoursOfContent = request.HoursOfContent,
            Author = request.Author,
            ArticleCount = request.ArticleCount,
            DownloadableResourcesCount = request.DownloadableResourcesCount,
            LifetimeAccess = request.LifetimeAccess,
            CertificateOfCompletion = request.CertificateOfCompletion,
            Price = request.Price,
            DiscountedPrice = request.DiscountedPrice,
            ProgramDetails = request.ProgramDetails.Select(x => x.Map()).ToList()
        };
    }

    public static CourseProgramDetailEntity Map(this CreateCourseProgramDetailRequest request)
    {
        return new CourseProgramDetailEntity
        {
            Title = request.Title,
            Description = request.Description,
            Step = request.Step,
        };
    }

    public static CourseResponse MapAll(this CourseEntity course)
    {
        return new CourseResponse
        {
            Id = course.Id,
            Tags = course.Tags,
            Topics = course.Topics,
            Title = course.Title,
            ImageUrl = course.ImageUrl,
            SubTitle = course.SubTitle,
            Description = course.Description,
            ReviewCount = course.ReviewCount,
            StarsCount = course.StarsCount,
            LikesCount = course.LikesCount,
            HoursOfContent = course.HoursOfContent,
            Author = course.Author,
            ArticleCount = course.ArticleCount,
            DownloadableResourcesCount = course.DownloadableResourcesCount,
            LifetimeAccess = course.LifetimeAccess,
            CertificateOfCompletion = course.CertificateOfCompletion,
            Price = course.Price,
            DiscountedPrice = course.DiscountedPrice,
            CourseDetails = course.ProgramDetails.Select(x => x.Map()).ToList()
        };
    }

    public static CourseDetailResponse Map(this CourseProgramDetailEntity detail)
    {
        return new CourseDetailResponse
        {
            Title = detail.Title,
            Description = detail.Description,
            Step = detail.Step
        };
    }

    public static CoursesResponse Map(this CourseEntity course, bool isBookmarked)
    {
        return new CoursesResponse
        {
            Id = course.Id,
            Tags = course.Tags,
            Title = course.Title,
            ImageUrl = course.ImageUrl,
            ReviewCount = course.ReviewCount,
            StarsCount = course.StarsCount,
            LikesCount = course.LikesCount,
            HoursOfContent = course.HoursOfContent,
            Author = course.Author,
            Price = course.Price,
            DiscountedPrice = course.DiscountedPrice,
            IsBookmarked = isBookmarked
        };
    }
}