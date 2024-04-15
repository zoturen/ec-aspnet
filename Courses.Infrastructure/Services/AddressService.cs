using Courses.Infrastructure.DAL;
using Courses.Infrastructure.Dtos;
using Courses.Infrastructure.Entities;
using Courses.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Services;

public class AddressService(DataContext context)
{
    public async Task<bool> UpsertAddress(CreateUserAddressDto dto)
    { 
        try
        {
            var userAddress = await context.UserAddresses.FirstOrDefaultAsync(x => x.UserId == dto.UserId);
            if (userAddress is null)
            {
                userAddress = new UserAddressEntity
                {
                    UserId = dto.UserId,
                    Line1 = dto.Line1,
                    Line2 = dto.Line2,
                    PostalCode = dto.PostalCode,
                    City = dto.City,
                };

                context.UserAddresses.Add(userAddress);
                await context.SaveChangesAsync();
                return true;
            }

            userAddress.Line1 = dto.Line1;
            userAddress.Line2 = dto.Line2;
            userAddress.PostalCode = dto.PostalCode;
            userAddress.City = dto.City;

            context.UserAddresses.Update(userAddress);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<UserAddress> GetAddress(string userId)
    {
        var userAddress = await context.UserAddresses.FirstOrDefaultAsync(x => x.UserId == userId);
        return new UserAddress
        {
            Line1 = userAddress?.Line1,
            Line2 = userAddress?.Line2,
            PostalCode = userAddress?.PostalCode,
            City = userAddress?.City
        };
    }
}