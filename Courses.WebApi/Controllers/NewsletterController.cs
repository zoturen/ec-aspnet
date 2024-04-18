using Courses.Shared.Models.Requests;
using Courses.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApi.Controllers;

[ApiController]
[Route("newsletter")]
[Authorize(Policy = "APIKEY")]
public class NewsletterController(NewsletterService newsletterService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Subscribe(NewsletterSubscribeRequest request)
    {
        if (await newsletterService.Subscribe(request))
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> UnSubscribe(string email)
    {
        if (await newsletterService.UnSubscribe(email))
        {
            return Ok();
        }

        return BadRequest();
    }
}