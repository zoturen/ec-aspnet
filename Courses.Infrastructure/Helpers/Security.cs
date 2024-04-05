using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Courses.Infrastructure.Helpers;

public class Security(IConfiguration configuration)
{
    private readonly string _privateKeyPath = configuration["identity:certs:privatekey"] ??
                                              throw new Exception(
                                                  "No private key path was found in identity:certs:privatekey");
    
    private readonly string _publicKeyPath = configuration["identity:certs:publickey"] ??
                                              throw new Exception(
                                                  "No private key path was found in identity:certs:privatekey");


    public string GetPublicKey()
    {
        var publicKeyText = File.ReadAllText(_publicKeyPath);
        return publicKeyText;
    }

    private RSAParameters? GetRsaParametersFromPrivateKey()
    {
        try
        {
            var rsa = RSA.Create();
            var privateKeyText = File.ReadAllText(_privateKeyPath);
            var privateKeyBlock = privateKeyText.Split("-", StringSplitOptions.RemoveEmptyEntries)[1];
            var privateKeyBytes = Convert.FromBase64String(privateKeyBlock);
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            return rsa.ExportParameters(true);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: could not load the private key for identity.... {e.Message}");
            // make a failsafe if were unable to load the certificate...
        return null;
        }
    }

    public string? GenerateAccessToken(ClaimsPrincipal claimsPrincipal)
    {
        var rsaParams = GetRsaParametersFromPrivateKey();

        if (rsaParams is null)
        {
            // something went superior bad and our things broke. happy not using the api.
            return null;
        }

        try
        {
            var privateKey = new RsaSecurityKey(rsaParams.Value);
            var creds = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = configuration["identity:audience"],
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = configuration["identity:issuer"],
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claimsPrincipal.Claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ERROR: could not create a access token: {e.Message}");
            return null;
        }
    }
}