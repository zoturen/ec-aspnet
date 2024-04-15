using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Courses.Infrastructure.Helpers;

namespace Courses.WebApp.Client;

public class UserApi
{
    private readonly Security _security;
    private readonly IConfiguration _configuration;

    private readonly HttpClient _client;

    public UserApi(IHttpClientFactory httpClientFactory, Security security, IConfiguration configuration)
    {
        _security = security;
        _configuration = configuration;
        _client = httpClientFactory.CreateClient("UserApi");
    }


    public HttpClient GetClient(HttpContext context)
    {
        var apikey = _configuration["apikey"] ?? throw new Exception("specify a apikey in appsettings: apikey");
        _client.DefaultRequestHeaders.Add("x-api-key", apikey);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken(context));
        return _client;
    }


    private string? GetAccessToken(HttpContext context)
    {
        try
        {
            var token = context?.Request.Cookies["AccessToken"];
            
            if (token != null && context?.User.Identity is {IsAuthenticated: false} && token.Length > 0)
            {
                context.Response.Cookies.Delete("AccessToken");
                return null;
            }
            if (string.IsNullOrEmpty(token))
            {
                token = SetAccessToken(context);
            }

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            if (ValidateTokenExpired(token))
            {
                token = SetAccessToken(context);
            } 

            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private bool ValidateTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return false;
            }
        }

        return true;
    }

    private string? SetAccessToken(HttpContext context)
    {
        
        var userClaims = context?.User;
        if (userClaims is null)
        {
            return null;
        }

        if (userClaims.Identity is {IsAuthenticated: false})
        {
            return null;
        }
        var token = _security.GenerateAccessToken(userClaims);

        if (token is null)
        {
            return null;
        }

        try
        {
            var options = new CookieOptions
            {
                HttpOnly = true, // Make the cookie access token inaccessible via Javascript
                Secure = true, // Transmit the cookie only over HTTPS
                SameSite = SameSiteMode.Strict, // Protect against CSRF attacks
                Expires = DateTimeOffset.UtcNow.AddMinutes(30) // Token expiry
            };

            context?.Response.Cookies.Append("AccessToken", token, options);

            return token;
        }
        catch (Exception e)
        {
            Debug.WriteLine("ERROR: could not set access token in a cookie");
            return null;
        }
    }
}