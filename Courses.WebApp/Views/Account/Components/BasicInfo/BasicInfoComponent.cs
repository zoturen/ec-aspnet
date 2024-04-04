using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Views.Account.Components.BasicInfo;

public class BasicInfoViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //perform any operations to prepare the model for view and pass it 
        var model = new BasicInfoViewModel();
        return View(model);
    }
}