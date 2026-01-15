using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiTest.Application.DTOs;
using ApiTest.Application.ISecurity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiTest.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;
    
    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Generate(UserDto user)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        
        var issuer = jwtSection["Issuer"]!;
        var audience = jwtSection["Audience"]!;
        var minutes = int.Parse(jwtSection["AccessTokenMinutes"]!);
        
        var secretKey = Environment.GetEnvironmentVariable("JWT_KEY");
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey!)
        );
        
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Name, user.Name)
        };
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: cred
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}