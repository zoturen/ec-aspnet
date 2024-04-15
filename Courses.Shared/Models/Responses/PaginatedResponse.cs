namespace Courses.Shared.Models.Responses;

public class PaginatedResponse <T> where T : class
{
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public List<T> Results { get; set; } = [];
}