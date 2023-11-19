using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Interfaces;


namespace UniTrackBackend.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    private readonly UniTrackDbContext _context;
    public AuthService(UserManager<User> userManager, IConfiguration config, UniTrackDbContext context)
    {
        _userManager = userManager;
        _config = config;
        _context = context;
    }
    
    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("sub", user.Id)
            }),
            Expires = DateTime.UtcNow.AddMinutes(2), // Short-lived token
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public async Task<string> GenerateRefreshToken(User user)
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        user.RefreshToken = refreshToken;
        user.RefreshTokenValidity = DateTime.Now.AddHours(2).ToUniversalTime(); // Refresh token valid for 2 hours

        await _userManager.UpdateAsync(user);

        return refreshToken;
    }

    public bool ValidateRefreshToken(User user, string refreshToken)
    {
        return user.RefreshToken == refreshToken && user.RefreshTokenValidity > DateTime.Now;
    }
    public async Task<User?> GetUserFromRefreshToken(string refreshToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenValidity > DateTime.Now.ToUniversalTime());

        if (user != null)
        {
            return await _userManager.FindByIdAsync(user.Id);
        }

        return null;
    }
    public CookieOptions GetRefreshCookieOptions() => new ()
    {
        HttpOnly = true,
        Secure = true,
        Expires = DateTime.UtcNow.AddHours(2),
        Domain = "localhost",
        IsEssential = true
    };
    public CookieOptions GetAccessCookieOptions() => new ()
    {
        HttpOnly = true,
        Secure = true,
        Expires = DateTime.UtcNow.AddMinutes(2),
        Domain = "localhost",
        IsEssential = true
    };
    
    //Needs User repository and entities to develop further
    
    public async Task<bool> ApproveStudents()
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ApproveParents()
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ApproveTeachers()
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ApproveAdmins()
    {
        throw new NotImplementedException();
    }
}

 