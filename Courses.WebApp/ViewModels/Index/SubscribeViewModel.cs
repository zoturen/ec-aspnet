using System.ComponentModel.DataAnnotations;

namespace Courses.WebApp.ViewModels.Index;

public class SubscribeViewModel
{
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string EmailAddress { get; set; } = null!;
    public bool DailyNewsletter { get; set; }
    public bool EventUpdates { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool WeekInReview { get; set; }
    public bool Podcasts { get; set; }
    public bool Successful { get; set; }
}