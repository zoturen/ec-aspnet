using System.ComponentModel.DataAnnotations;

namespace Courses.Shared.Models.Requests;

public class NewsletterSubscribeRequest
{
    [EmailAddress]
    [Required]
    public string EmailAddress { get; set; } = null!;

    public bool DailyNewsletter { get; set; }
    public bool EventUpdates { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool WeekInReview { get; set; }
    public bool Podcasts { get; set; } 
}