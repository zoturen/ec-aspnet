namespace Courses.Shared.Models.Responses;

public class CategoriesResponse
{

    public CategoriesResponse()
    {
        foreach (var category in Enum.GetValues<Categories>())
        {
            if (category == Models.Categories.None)
            {
                continue;
            }
            Categories.Add(category);
        }
    }
    
    public List<Categories> Categories { get; set; } = [];
    
}