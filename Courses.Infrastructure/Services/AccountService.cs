using System.Diagnostics;
using Courses.Infrastructure.DAL;
using Courses.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Courses.Infrastructure.Services;

public class AccountService(IConfiguration configuration, DataContext dataContext)
{
    public async Task<bool> ChangeAvatar(string userId, IFormFile file)
    {
        await using var transaction = await dataContext.Database.BeginTransactionAsync();
        try
        {
            var userEntity = await dataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (userEntity is null)
            {
                return false;
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"avatar_{userId}{fileExtension}";
            var path = configuration["avatarPath"] ?? "wwwroot/images";
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            

            userEntity.AvatarUrl = fileName;
            dataContext.Users.Update(userEntity);
            
            await dataContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: could not change image for user. {e.Message}");
        }

        await transaction.RollbackAsync();
        return false;
    }
}