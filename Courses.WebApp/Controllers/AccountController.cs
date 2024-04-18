using System.Security.Claims;
using Courses.Infrastructure.Dtos;
using Courses.Infrastructure.Entities;
using Courses.Infrastructure.Services;
using Courses.WebApp.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, AddressService addressService, AccountService accountService) : Controller
{
    public async Task<IActionResult> Details()
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return View();
        }

        var address = await addressService.GetAddress(user.Id);
        var model = new DetailsViewModel
        {
            BasicInfo = new BasicInfoViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                Bio = user.Bio
            },
            AddressInfo = new AddressInfoViewModel
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                PostalCode = address.PostalCode,
                City = address.City
            }
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(BasicInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);
            if (user is null)
            {
                return PartialView("Account/Details/_BasicInfo", model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Bio = model.Bio;
            user.PhoneNumber = model.Phone;
            user.Email = model.Email;
            user.UserName = model.Email;
            var result = await userManager.UpdateAsync(user);
            model.UpdatedSuccessfully = result.Succeeded;
        }

        return PartialView("Account/Details/_BasicInfo", model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddressInfo(AddressInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var address = new CreateUserAddressDto
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                Line1 = model.Line1!,
                Line2 = model.Line2,
                PostalCode = model.PostalCode!,
                City = model.City!
            };

            model.UpdatedSuccessfully = await addressService.UpsertAddress(address);
        }

        return PartialView("Account/Details/_AddressInfo", model);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeAvatar(IFormFile file)
    {
        var redirectUrl = Url.Action("Details", "Account") ?? "account/details";
        if (file is null || file.Length == 0) // TODO: we should validate if the file actually is a image.
                                              //       One way would be to look at the header and match the bytes for a real image file.
        {
            return Redirect(redirectUrl);
        }
        await accountService.ChangeAvatar(User.FindFirstValue(ClaimTypes.NameIdentifier)!, file);
        return Redirect(redirectUrl); // TODO: should somehow handle error. Maybe a Toast?
    }
}