using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Interfaces;

namespace UniTrackBackend.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    private readonly UniTrackDbContext _context;
    private readonly SignInManager<User> _signInManager;


    public AuthService(UserManager<User> userManager, IConfiguration config, UniTrackDbContext context, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _config = config;
        _context = context;
        _signInManager = signInManager;
    }
    
    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("sub", user.Id),
                new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
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


    public async Task<string?> GetEmailConfirmationToken(User user)
    {
        // Generate the confirmation link
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<User> RegisterUser(RegisterViewModel model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new DataException();
        
        return user;
    }

    public async Task<User> LoginUser(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password)) throw new DataException();;

        return user;
    }

    public async Task SignInUser(User user)
    {
        await _signInManager.SignInAsync(user, isPersistent: false);

    }

    public async Task LogoutUser(User user)
    {
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> ConfirmEmail(User user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user, WebUtility.UrlDecode(token));
        return result;
    }

    public async Task<IdentityResult> ResetPassword(ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        throw new NotImplementedException();
    }

    public async Task<string> GenerateForgottenPasswordLink(User user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (token is null)
            throw new Exception();
        return token;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            throw new DataException("If an account with this email exists, a password reset link has been sent.");
        }

        return user;
    }

    public async Task<User> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new DataException();
        return user;
    }
}

 