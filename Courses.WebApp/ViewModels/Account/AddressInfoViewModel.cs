using System.ComponentModel.DataAnnotations;

namespace Courses.WebApp.ViewModels.Account;

public class AddressInfoViewModel
{
    [Display(Name = "Address Line 1", Prompt = "Enter your address line...")]
    [Required(ErrorMessage = "You must enter a address.")]
    public string? Line1 { get; set; }
    [Display(Name = "Address Line 2", Prompt = "Enter your second address line...")]
    public string? Line2 { get; set; }
    [Display(Name = "Postal Code", Prompt = "Enter your postal code...")]
    [Required(ErrorMessage = "You must enter a postal code.")]
    public string? PostalCode { get; set; }
    [Display(Name = "City", Prompt = "Enter your city...")]
    [Required(ErrorMessage = "You must enter a city.")]
    public string? City { get; set; }
    public bool UpdatedSuccessfully { get; set; }
}
