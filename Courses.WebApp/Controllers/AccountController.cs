using Courses.Infrastructure.Entities;
using Courses.WebApp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class AccountController(UserManager<UserEntity> userManager) : Controller
{
    public async Task<IActionResult> Details()
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return View();
        }
        var model = new DetailsViewModel
        {
            BasicInfo = new BasicInfoViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                Bio = user.Bio 
            }
        };

        return View(model);
    }
    
    [HttpPost]
    public IActionResult UpdateBasicInfo(BasicInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Update the basic info here...
            Console.WriteLine("valid");
            //return ViewComponent("BasicInfo", model);
            return PartialView("_BasicInfo", model);
        }
        else 
        {
            Console.WriteLine("not valid");
            // Set TempData values to indicate an error with the basic info form
           // TempData["BasicInfoError"] = true;
           // TempData["ModelError"] = ModelState;
       
            // Redirect back to the form

            //return View("Details",model);
            return PartialView("_BasicInfo", model);
        }
    }
}