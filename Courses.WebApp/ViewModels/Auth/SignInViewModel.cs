using System.ComponentModel.DataAnnotations;

namespace Courses.WebApp.ViewModels.Auth;

public class SignInViewModel
{
    [Display(Prompt = "Enter your email...")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Prompt = "******")]
    public string Password { get; set; } = null!;
    
    public bool RememberMe { get; set; }
}