using Courses.Shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApi.Controllers;


[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok(new CategoriesResponse());
    }
}