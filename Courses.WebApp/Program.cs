using Courses.Infrastructure.DAL;
using Courses.Infrastructure.Entities;
using Courses.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddInfrastructure();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
//builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
  //  .AddIdentityCookies();

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


//builder.Services.AddAuthorizationBuilder();
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);


//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<SignInManager<UserEntity>>();
//builder.Services.AddScoped<UserManager<UserEntity>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseInfrastructure();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();