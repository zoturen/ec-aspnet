using Courses.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.DAL;

public class DataContext(DbContextOptions<DataContext> opts) : IdentityDbContext<UserEntity> (opts)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserAddressEntity> UserAddresses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserEntity>()
            .HasOne(a => a.Address)
            .WithOne(b => b.User)
            .HasForeignKey<UserAddressEntity>(b => b.UserId);
    }
}