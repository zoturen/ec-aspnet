using Courses.Infrastructure.Extensions;
using Courses.WebApp.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
builder.Services.AddScoped<BreadcrumbActionFilter>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(BreadcrumbActionFilter));
});
builder.Services.AddRouting(x => x.LowercaseUrls = true);

var app = builder.Build();
app.UseInfrastructure();


app.Run();