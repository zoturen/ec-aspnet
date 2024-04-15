using System.Text;
using Courses.Shared.Models.Responses;
using Courses.WebApp.Client;
using Courses.WebApp.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Views.Shared.Components.CoursePreviewGrid;

public class CoursePreviewGridViewComponent(UserApi userApi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CoursePreviewGridModel gridModel)
    {

        var client = userApi.GetClient(HttpContext);

        var sb = new StringBuilder();
        sb.Append("courses?");
        if (gridModel.Category.Length > 0)
        {
            sb.Append($"category={gridModel.Category}&");
        }

        if (gridModel.Filter.Length > 0)
        {
            sb.Append($"searchString={gridModel.Filter}&");
        }

        sb.Append($"pageCount=9&");
        sb.Append($"pageNumber={gridModel.CurrentPage}");

        var courses = await client.GetFromJsonAsync<PaginatedResponse<CoursesResponse>>(sb.ToString());

        var coursePreviewGridViewModel = new CourseGridViewModel
        {
            PreviewCourseItems = courses.Results.Select(x => new PreviewCourseViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Price = x.Price,
                    DiscountPrice = x.DiscountedPrice ?? 0,
                    BestSeller = x.Tags.Contains("Best Seller"),
                    HoursOfContent = (int)x.HoursOfContent,
                    ApprovalRate = (int)x.LikesCount,
                    RateCount = x.ReviewCount,
                    IsBookmarked = x.IsBookmarked,
                    ImageUrl = x.ImageUrl
                })
                .ToList(),
            TotalPages = courses.TotalPages,
            CurrentPage = courses.CurrentPage,

        };
        return View(coursePreviewGridViewModel);
    }

}