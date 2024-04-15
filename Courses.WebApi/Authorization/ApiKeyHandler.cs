using Microsoft.AspNetCore.Authorization;

namespace Courses.WebApi.Authorization;

public class ApiKeyHandler(IConfiguration configuration) : AuthorizationHandler<ApiKeyRequirement>
{
   
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (!httpContext.Request.Headers.TryGetValue("x-api-key", out var apiKeyHeaderValues))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var providedApiKey = apiKeyHeaderValues.First();

        var apikey = configuration["apikey"] ?? throw new Exception("specify a apikey in appsettings: apikey");
        if (providedApiKey != apikey)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}