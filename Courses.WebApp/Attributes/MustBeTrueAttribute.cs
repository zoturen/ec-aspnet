using System.ComponentModel.DataAnnotations;

namespace Courses.WebApp.Attributes;

public class MustBeTrueAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is true;
    }
}