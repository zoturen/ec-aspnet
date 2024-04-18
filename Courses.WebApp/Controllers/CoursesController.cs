using Courses.Infrastructure.Client;
using Courses.Shared.Models.Requests;
using Courses.WebApp.ViewModels.Course;
using Courses.WebApp.Views.Shared.Components.CoursePreviewGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Courses.WebApp.Controllers;

[Authorize]
public class CoursesController(UserApi userApi) : Controller
{
    public async Task<IActionResult> Index(string? category, string? searchString, int pageNumber, CourseViewModel? model)
    {
        var client = userApi.GetClient(HttpContext);

        var categories = await client.GetFromJsonAsync<Dictionary<string, List<string>>>("categories");

        model.Categories =
            [..categories["categories"].Select(x => new SelectListItem(x, x, x == category))];

        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        if (model.CoursePreview is null)
        {
            model.CoursePreview = new CoursePreviewGridModel
            {
                Category = category ?? "",
                Filter = searchString ?? "",
                ItemCount = 9,
                TotalPages = 0,
                CurrentPage = pageNumber
            }; 
        }
        
        
        return View(model);
    }

    [HttpPost("[controller]/bookmark")]
    public async Task<IActionResult> BookmarkCourse( BookmarkCourseViewModel model)
    {
        
        var client = userApi.GetClient(HttpContext);
        var result = await client.PostAsJsonAsync($"courses/{model.CourseId}/bookmark", new BookmarkCourseRequest
        {
            IsBookmarked = model.IsBookmarked
        });

        if (result.IsSuccessStatusCode)
        {
            return PartialView("Courses/_BookmarkCourse", model);
        }

        model.IsBookmarked = !model.IsBookmarked;
        return PartialView("Courses/_BookmarkCourse", model);

    }
}