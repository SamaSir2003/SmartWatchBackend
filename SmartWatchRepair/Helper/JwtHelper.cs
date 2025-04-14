using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SmartWatchRepair.Helper;

public static class JwtHelper
{
    private static string SecretKey = "YourSuperSecureLongSecretKeyHere123456!";  // Replace with your actual secret key

    // Renamed method to GenerateJwtToken
    public static string GenerateJwtToken(string userId, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userId),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "YourIssuer",
            audience: "YourAudience",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Validate Token (no change)
    public static ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(SecretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "YourIssuer",
                ValidAudience = "YourAudience",
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out var validatedToken);

            return principal;
        }
        catch
        {
            return null; // Invalid token
        }
    }
}