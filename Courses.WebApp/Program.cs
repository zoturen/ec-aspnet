using System.Net.Http.Headers;
using Courses.Infrastructure.DAL;
using Courses.Infrastructure.Entities;
using Courses.Infrastructure.Extensions;
using Courses.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddInfrastructure();


//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<SignInManager<UserEntity>>();
//builder.Services.AddScoped<UserManager<UserEntity>>();

var app = builder.Build();
app.UseInfrastructure();
app.Run();