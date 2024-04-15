using Courses.Shared.Models;
using Courses.WebApp.Views.Shared.Components.CoursePreviewGrid;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Courses.WebApp.ViewModels.Course;

public class CourseViewModel
{
    public List<SelectListItem> Categories { get; set; }
    public CoursePreviewGridModel? CoursePreview { get; set; }
}