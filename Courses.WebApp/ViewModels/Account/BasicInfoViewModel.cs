using System.ComponentModel.DataAnnotations;

namespace Courses.WebApp.ViewModels.Account;

public class BasicInfoViewModel
{
    [Required(ErrorMessage = "You must enter a firstname.")]
    [Display(Name = "FirstName", Prompt = "Enter your first name.")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "You must enter a lastname.")]
    [Display(Name = "LastName", Prompt = "Enter your last name....")]
    public string LastName { get; set; } = null!;
    [Display(Name = "Email", Prompt = "Enter your email address...")]
    [Required(ErrorMessage = "You must enter a email address.")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Enter you phone number.")]
    [Display(Name = "Phone number", Prompt = "Enter your phone number...")]
    public string? Phone { get; set; }
    [DataType(DataType.MultilineText)]
    [Display(Name = "Bio", Prompt = "....")]
    public string? Bio { get; set; }
}