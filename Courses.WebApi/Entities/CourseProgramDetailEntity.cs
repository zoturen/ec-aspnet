using System.ComponentModel.DataAnnotations;

namespace Courses.WebApi.Entities;

public class CourseProgramDetailEntity
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Step { get; set; } // In a better world, should step and courseId probably be unique with each other. But im not absolutely sure yet, maybe its ok to have step 4 two times.
                                  // Yeah, its my program. I make the decisions.

    public int CourseId { get; set; }
    public CourseEntity Course { get; set; }
}