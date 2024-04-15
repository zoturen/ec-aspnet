using System.Diagnostics;
using Courses.Shared.Models;
using Courses.Shared.Models.Requests;
using Courses.Shared.Models.Responses;
using Courses.WebApi.DAL;
using Courses.WebApi.Entities;
using Courses.WebApi.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Courses.WebApi.Services;

public class CourseService(DataContext dataContext)
{
    public async Task<bool> CreateCourseAsync(CreateCourseRequest request)
    {
        try
        {
            var entity = request.Map();

            dataContext.Courses.Add(entity);
            await dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: something went wrong: {e.Message}");
        }

        return false;
    }

    public async Task<PaginatedResponse<CoursesResponse>> GetCoursesAsync(string userId, Categories category, string? searchString,
        int pageCount = 9,
        int pageNumber = 1)
    {
        var skip = pageCount * (pageNumber - 1);


        var queryable = dataContext.Courses.AsQueryable();
        if (category != Categories.None)
        {
            queryable = queryable.Where(x => x.Category == category);
        }

        if (searchString is not null)
        {
            queryable = queryable.Where(x => x.Title.Contains(searchString));
        }
        var totalCount = await queryable.CountAsync();
        var courseAndBookmarkInfo = queryable
            .GroupJoin(dataContext.SavedCourses.Where(x => x.UserId == userId),
                course => course.Id,
                savedCourse => savedCourse.CourseId,
                (course, savedCourses) => new { course, IsBookmarked = savedCourses.Any() });

        var maybeTotalPages = Math.Ceiling((double)totalCount / pageCount);
        var hasOneMorePage = totalCount % pageNumber != 0;
        var totalPages = hasOneMorePage ? maybeTotalPages + 1 : maybeTotalPages;

        var paginatedResults = courseAndBookmarkInfo.Skip(skip).Take(pageCount);

        var result = await paginatedResults.ToListAsync();

        var mapped = result.Select(x => x.course.Map(x.IsBookmarked)).ToList();        

        return new PaginatedResponse<CoursesResponse>
        {
            TotalPages = (int)totalPages,
            CurrentPage = pageNumber,
            Results = mapped
        };
    }

    public async Task<CourseResponse?> GetAsync(int id)
    {
        var courseEntity = await dataContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
        return courseEntity?.MapAll();
    }

    public async Task<bool> UpdateAsync(int id, UpdateCourseRequest request)
    {
        try
        {
            var exists = await dataContext.Courses.AnyAsync(x => x.Id == id);
            if (exists)
            {
                var courseEntity = new CourseEntity
                {
                    Id = id,
                    Tags = request.Tags,
                    Topics = request.Topics,
                    Category = request.Category,
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
                };

                var details = request.ProgramDetails.Select(x => new CourseProgramDetailEntity
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Step = x.Step,
                    CourseId = id
                }).ToList();

                courseEntity.ProgramDetails = details;

                //dataContext.CourseProgramDetails.UpdateRange(details);
                dataContext.Courses.Update(courseEntity);
                await dataContext.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: could not update course: {e.Message}");
        }

        return false;
    }

    public async Task<bool> BookmarkAsync(int courseId, string userId, BookmarkCourseRequest request)
    {
        try
        {
            var exists =
                await dataContext.SavedCourses.AnyAsync(x => x.CourseId == courseId && x.UserId == userId);
            var savedCourseEntity = new SavedCourseEntity
            {
                CourseId = courseId,
                UserId = userId
            };
            if (request.IsBookmarked && !exists)
            {
                
                dataContext.SavedCourses.Add(savedCourseEntity);
                await dataContext.SaveChangesAsync();
                return true;
            }

            if (exists && !request.IsBookmarked)
            {
                dataContext.Entry(savedCourseEntity).State = EntityState.Deleted;
                await dataContext.SaveChangesAsync();
                return true;
            }

            if (exists && request.IsBookmarked)
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: something very bad happened in BookmarkAsync: {e.Message}");
        }

        return false;
    }
}