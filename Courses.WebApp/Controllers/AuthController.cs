using Courses.Infrastructure.Entities;
using Courses.Infrastructure.Helpers;
using Courses.WebApp.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

public class AuthController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, Security security) : Controller
{


    public IActionResult SignIn()
    {
        var model = new SignInViewModel();
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                ModelState.AddModelError("invalid", "Invalid email or password.");
                return View(model);
            }
        }

        return View(model);
    }


    public IActionResult SignUp()
    {
        var model = new SignUpViewModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    AvatarUrl = "/images/unreasonably-happy.svg"
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }

                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Code);
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("exc", "Something went wrong, please try again.");
                return View(model);
            }
        }

        return View(model);
    }

    [HttpGet("auth/keys")]
    public IActionResult GetPublicKeys()
    {
        return Ok(security.GetPublicKey());
    }
}