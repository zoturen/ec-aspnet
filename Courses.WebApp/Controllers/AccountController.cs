using Courses.WebApp.Views.Account.Components.BasicInfo;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class AccountController : Controller
{
    public IActionResult Details()
    {
        return View();
    }
    
    public IActionResult UpdateBasicInfo(BasicInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Update the basic info here...

            return RedirectToAction("Details", "Account");
        }
        else 
        {
            // Set TempData values to indicate an error with the basic info form
            TempData["BasicInfoError"] = true;
            TempData["ModelError"] = ModelState;
       
            // Redirect back to the form
            return RedirectToAction("Details", "Account");
        }
    }
}