using Courses.Infrastructure.DAL;
using Courses.Infrastructure.Helpers;
using Courses.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Courses.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddDbContext<DataContext>(x =>
        {
            x.UseNpgsql(builder.Configuration["postgres:connectionString"]);
        });

        services.AddScoped<Security>();
        services.AddScoped<AccountService>();

    }

    public static void UseInfrastructure(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<DataContext>();

        dbContext.Database.Migrate();
    }
}