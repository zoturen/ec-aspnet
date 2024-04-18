using System.Security.Claims;
using Courses.Shared.Models;
using Courses.Shared.Models.Requests;
using Courses.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApi.Controllers;

[ApiController]
[Route("courses")]
[Authorize(Policy = "APIKEY", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CoursesController(CourseService courseService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest course)
    {
        var ok = await courseService.CreateCourseAsync(course);
        if (ok)
        {
            return Created();
        }

        return BadRequest();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCourse(int id, UpdateCourseRequest course)
    {
        var ok = await courseService.UpdateAsync(id, course);
        if (ok)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("{id:int}/bookmark")]
    public async Task<IActionResult> BookmarkCourse(int id, BookmarkCourseRequest bookmark)
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Unauthorized();
        }
        var ok = await courseService.BookmarkAsync(id, userId, bookmark);
        if (ok)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses(Categories category, string? searchString, int pageCount, int pageNumber)
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Unauthorized();
        }
        if (!Enum.IsDefined(typeof(Categories), category))
        {
            return BadRequest("No category matches your query");
        }

        return Ok(await courseService.GetCoursesAsync(userId, category, searchString, pageCount, pageNumber));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var course = await courseService.GetAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }
}