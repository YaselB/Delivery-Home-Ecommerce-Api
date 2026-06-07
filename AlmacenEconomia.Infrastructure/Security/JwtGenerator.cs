using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlmacenEconomia.Application.Interfaces.Jwt;
using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Domain.Entity.Customer;
using AlmacenEconomia.Domain.Entity.Worker;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AlmacenEconomia.Infrastructure.Security;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings jwtSettings;
    public JwtGenerator(IOptions<JwtSettings> options)
    {
        jwtSettings = options.Value;
    }
    public string GenerateAdminToken(AdminEntity admin)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub , admin.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName , admin.Email),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
        };
        foreach( var i in admin.Permission)
        {
            claims.Add(new Claim("permission" ,i));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateCustomerToken(CustomerEntity customer)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub , customer.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName , customer.Email),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
        };
        foreach(var i in customer.Permission)
        {
            claims.Add(new Claim("permission" , i));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateWorkerToken(WorkerEntity worker)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub , worker.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName , worker.Email),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
        };
        foreach( var i in worker.Permission)
        {
            claims.Add(new Claim("permission" , i));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}