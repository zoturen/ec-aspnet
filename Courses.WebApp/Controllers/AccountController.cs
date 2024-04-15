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
public class AccountController(UserManager<UserEntity> userManager, AddressService addressService) : Controller
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
}