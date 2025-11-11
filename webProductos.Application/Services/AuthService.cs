
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webProductos.Application.DTOs;
using webProductos.Domain.Entities;
using webProductos.Infrastructure.Persistence;

namespace webProductos.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _ctx;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext ctx, IConfiguration config)
    {
        _ctx = ctx;
        _config = config;
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
    {
        var exists = await _ctx.Users.AnyAsync(u => u.UserName == dto.Username || u.Email == dto.Email);
        if (exists) return new AuthResultDto { Success = false, Errors = new[] { "Usuario ya existe" } };

        var user = new User
        {
            UserName = dto.Username,
            Email = dto.Email,
            Role = dto.Role ?? "User",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync();

        return new AuthResultDto { Success = true };
    }

    public async Task<AuthResultDto> LoginAsync(LoginDto dto)
    {
        var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return new AuthResultDto { Success = false, Errors = new[] { "Credenciales inválidas" } };

        var token = GenerateToken(user);
        return new AuthResultDto { Success = true, Token = token };
    }

    private string GenerateToken(User user)
    {
        var secret = _config["JwtSettings:Secret"];
        var issuer = _config["JwtSettings:Issuer"];
        var audience = _config["JwtSettings:Audience"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddMinutes(60), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
