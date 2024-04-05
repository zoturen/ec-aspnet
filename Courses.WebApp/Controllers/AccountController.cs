using Courses.Infrastructure.Entities;
using Courses.WebApp.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

[Authorize]
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
            // update!!!!!!!
            
        }
        model.UpdatedSuccessfully = ModelState.IsValid;

        return PartialView("Account/Details/_BasicInfo", model);
      
    }
    
    [HttpPost]
    public IActionResult UpdateAddressInfo(AddressInfoViewModel model)
    {

        if (ModelState.IsValid)
        {
            // update!!!!!!!
            
        }
        model.UpdatedSuccessfully = ModelState.IsValid;

        return PartialView("Account/Details/_AddressInfo", model);
      
    }
}