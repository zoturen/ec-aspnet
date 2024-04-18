using Courses.Infrastructure.Client;
using Courses.WebApp.Attributes;
using Courses.WebApp.ViewModels.Index;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class HomeController(UserApi userApi) : Controller
{
    [Breadcrumb(Name = "Home")]
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Subscribe(SubscribeViewModel model)
    {
        var client = userApi.GetClient(HttpContext);
        var res = await client.PostAsJsonAsync("/newsletter", model);
        if (res.IsSuccessStatusCode)
        {
            model.Successful = true;
        }

        return PartialView("Index/_Subscribe", model);
    }
}