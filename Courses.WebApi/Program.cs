using Courses.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApp();

var app = builder.Build();

app.UseApp();
app.Run();
