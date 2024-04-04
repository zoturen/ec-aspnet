using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class CoursesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}