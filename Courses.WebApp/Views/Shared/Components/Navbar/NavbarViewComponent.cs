using Courses.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Views.Shared.Components.Navbar;


public class NavbarViewModel
{
    public UserEntity? User { get; set; }
    public bool IsAuthenticated { get; set; }
}
public class NavbarViewComponent(UserManager<UserEntity> userManager) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavbarViewModel();
        model.IsAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;
        
        model.User = await userManager.GetUserAsync(HttpContext.User);
        return View(model);
    }
}