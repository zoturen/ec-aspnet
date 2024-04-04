using Courses.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Courses.WebApi.DAL;

public class DataContext(DbContextOptions<DataContext> opts) : DbContext(opts)
{
    public DbSet<ContactMessageEntity> ContactMessages { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<CourseProgramDetailEntity> CourseProgramDetails { get; set; }
    public DbSet<SubscribeEntity> Subscribers { get; set; }
}