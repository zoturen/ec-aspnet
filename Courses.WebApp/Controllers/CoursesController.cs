using Courses.WebApp.Client;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class CoursesController(UserApi userApi) : Controller
{
    public async Task<IActionResult> Index()
    {
        var client = userApi.GetClient(HttpContext);

        var res = await client.GetAsync("/");
        
        return View();
    }
}