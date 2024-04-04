using Courses.WebApi.DAL;
using Microsoft.EntityFrameworkCore;

namespace Courses.WebApi.Extensions;

public static class AppExtensions
{
    public static void AddApp(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddDbContext<DataContext>(x => { x.UseNpgsql(builder.Configuration["postgres:connectionString"]); });


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseApp(this WebApplication app)
    {
// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<DataContext>();

        dbContext.Database.Migrate();
    }
}