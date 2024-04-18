using Microsoft.AspNetCore.Mvc.Filters;

namespace Courses.WebApp.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class BreadcrumbAttribute : ActionFilterAttribute
{
    public string Name { get; set; } = null!;
    public string ParentAction { get; set; } = "";
    public string ParentController { get; set; } = "";
}