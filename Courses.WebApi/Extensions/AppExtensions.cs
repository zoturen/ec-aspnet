using System.Net;
using System.Security.Cryptography;
using Courses.WebApi.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Courses.WebApi.Extensions;

public static class AppExtensions
{
    public static void AddApp(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddDbContext<DataContext>(x => { x.UseNpgsql(builder.Configuration["postgres:connectionString"]); });


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpClient();

        var serviceProvider = services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost:5298",
                    ValidAudience = "localhost:5122",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                    {
                        var url = builder.Configuration["identity:publickey"] ?? throw new Exception("ERROR: could not get the public key url");

                        var client = httpClientFactory.CreateClient();
                        var pem = Task.Run(() => client.GetStringAsync(url)).Result; // We are fetching a .pem file, with the ---- begin and ends. we could probably remove that and not depend on BouncyCastle. But not today.
                        // Convert PEM to RSA key
                        var pemReader = new PemReader(new StringReader(pem));
                        var rsaKeyParameters = (RsaKeyParameters)pemReader.ReadObject();
                        var rsaParameters = DotNetUtilities
                            .ToRSAParameters(rsaKeyParameters);
                        var rsa = new RSACryptoServiceProvider();
                        rsa.ImportParameters(rsaParameters);
                
                        return new List<SecurityKey> { new RsaSecurityKey(rsa) };
                    }
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Bearer", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
            });
        }); 
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

        app.UseAuthentication();

        app.MapGet("/", (HttpContext context) =>
        {

            Console.WriteLine("hello");
            Console.WriteLine();
            return "hello";
        }).RequireAuthorization(JwtBearerDefaults.AuthenticationScheme);
    }
}