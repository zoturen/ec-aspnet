using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class AccountController : Controller
{
    public IActionResult Details()
    {
        return View();
    }
}