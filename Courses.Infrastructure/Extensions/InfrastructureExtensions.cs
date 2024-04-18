using System.Net.Http.Headers;
using Courses.Infrastructure.Client;
using Courses.Infrastructure.DAL;
using Courses.Infrastructure.Entities;
using Courses.Infrastructure.Helpers;
using Courses.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
        
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        builder.Services.AddIdentity<UserEntity, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/401";
            options.Cookie.Name = "SiliconSuperCookie";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/auth/signin";
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        });

        builder.Services.AddAntiforgery(x =>
        {
            x.Cookie.Name = "NoAttackMate";
        });


        builder.Services.AddHttpClient("UserApi", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["apiUrl"] ?? throw new Exception("we did not find any api base url."));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        builder.Services.AddScoped<UserApi>();

        builder.Services.AddHttpClient("Api", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["apiUrl"] ?? throw new Exception("we did not find any api base url."));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        builder.Services.AddScoped<AddressService>();
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddRouting(x => x.LowercaseUrls = true);


        services.AddScoped<Security>();
        services.AddScoped<AccountService>();

    }

    public static void UseInfrastructure(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();



        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        
        
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<DataContext>();

        dbContext.Database.Migrate();
    }
}