using System.Diagnostics;
using Courses.Shared.Models.Requests;
using Courses.WebApi.DAL;
using Courses.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Courses.WebApi.Services;

public class NewsletterService(DataContext dataContext)
{
    public async Task<bool> Subscribe(NewsletterSubscribeRequest request)
    {
        try
        {
            // no need to check if we are already subscribed in current implementation
            // if we caught an ex we will still return false.
            
            // TODO: send back a sweeter message if we're already subscribed.
            
            var entity = new SubscribeEntity
            {
                EmailAddress = request.EmailAddress,
                DailyNewsletter = request.DailyNewsletter,
                EventUpdates = request.EventUpdates,
                AdvertisingUpdates = request.AdvertisingUpdates,
                StartupsWeekly = request.StartupsWeekly,
                WeekInReview = request.WeekInReview,
                Podcasts = request.Podcasts
            };

            dataContext.Subscribers.Add(entity);
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: shit went south. Maybe we're already subscribed: {e.Message}");
        }

        return false;
    }

    public async Task<bool> UnSubscribe(string email)
    {
        try
        {
            var entity = new SubscribeEntity
            {
                EmailAddress = email
            };

            dataContext.Entry(entity).State = EntityState.Deleted;
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            // We just want to remove the user from our awesome mailing list at any price. The user does not want our spam apparently. 
            Debug.WriteLine("Everything is fine. We caught an ex but we dont care if the user is subscribed or not.");
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: now we are in trouble in the unsubscribe section of the service: {e.Message}");    
        }
        return false;
    }
}