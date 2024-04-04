using System.ComponentModel.DataAnnotations;
using Courses.WebApp.Attributes;

namespace Courses.WebApp.ViewModels.Auth;

public class SignUpViewModel
{
    [Required(ErrorMessage = "You must enter a first name.")]
    [Display(Name = "First name", Prompt = "Enter your firstname")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a last name.")]
    [Display(Name = "Last name", Prompt = "Enter your lastname")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a email address.")]
    [Display(Name = "Email", Prompt = "Enter your email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a password.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "******")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Confirm your password.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "******")]
    [Compare(nameof(Password), ErrorMessage = "You passwords must match.")]
    public string ConfirmPassword { get; set; } = null!;

    [MustBeTrue(ErrorMessage = "You must accept the terms & conditions.")] 
    public bool AcceptedTerms { get; set; }

}