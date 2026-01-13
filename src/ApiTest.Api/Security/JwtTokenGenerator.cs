using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiTest.Api.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;
    
    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Generate(Guid id)
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
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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