using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class AuthController : Controller
{
    public IActionResult SignIn()
    {
        return View();
    }
    
    public IActionResult SignUp()
    {
        return View();
    }
}