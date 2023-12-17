using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    private readonly UniTrackDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AuthService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<User> userManager,
        IConfiguration config,
        UniTrackDbContext context,
        SignInManager<User> signInManager,
        ILogger<AuthService> logger, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _config = config;
        _context = context;
        _signInManager = signInManager;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public string GenerateJwtToken(User user)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while generating JWT token");
            throw;
        }

    }
    
    public async Task<string> GenerateRefreshToken(User user)
    {
        try
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            user.RefreshToken = refreshToken;
            user.RefreshTokenValidity = DateTime.Now.AddHours(2).ToUniversalTime(); // Refresh token valid for 2 hours

            await _userManager.UpdateAsync(user);

            return refreshToken;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while generating user refresh token");
            throw;
        }
    }

    public bool ValidateRefreshToken(User user, string refreshToken)
    {
        try
        {
            return user.RefreshToken == refreshToken && user.RefreshTokenValidity > DateTime.Now;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while authenticating the user");
            throw;
        }
    }
    public async Task<User?> GetUserFromRefreshToken(string refreshToken)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenValidity > DateTime.Now.ToUniversalTime());

            if (user != null)
            {
                return await _userManager.FindByIdAsync(user.Id);
            }

            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while attempting to get user from refresh token");
            throw;
        }

    }


    public async Task<string?> GetEmailConfirmationToken(User user)
    {
        try
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while generating email confirmation token");
            throw;
        }
    }

    public async Task<User> RegisterUser(RegisterViewModel model)
    {
        try
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                AvatarUrl = "https://cdn-icons-png.flaticon.com/512/1154/1154955.png"
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new DataException();
        
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while registering the user");
            throw;
        }
    }

    public async Task<User?> LoginUser(LoginViewModel model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return null;
            
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while logging in the user");
            throw;
        }

    }

    public async Task SignInUser(User user)
    {
        try
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while signing in the user");
            throw;
        }
    }

    public async Task LogoutUser(User user)
    {
        try
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while logging out the user");
            throw;
        }
    }

    public async Task<IdentityResult> ConfirmEmail(User user, string token)
    {
        try
        {
            var result = await _userManager.ConfirmEmailAsync(user, WebUtility.UrlDecode(token));
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while verifying the user email");
            throw;
        }
    }

    public async Task<IdentityResult?> ResetPassword(ResetPasswordModel model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                _logger.LogWarning("User with that email does not exist", model.Email);
                return null;
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while resetting password");
            throw;
        }
    }

    public async Task<string> GenerateForgottenPasswordLink(User user)
    {
        try
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token is null)
                throw new NullReferenceException();
            return token;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while generating password reset link");
            throw;
        }
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null )
            {
                _logger.LogWarning("User with that email doesnt exist or has not confirmed it");
                return null;
            }

            if (await _userManager.IsEmailConfirmedAsync(user)) return user;
            
            _logger.LogWarning("User with that email has not confirmed it");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> GetUserById(string id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                _logger.LogWarning("User with that id does not exist", id);
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while trying to fetch a user");
            throw;
        }
    }

    public async Task<string> GetUserRole(User user)
    {
        var roleList = await _userManager.GetRolesAsync(user);
        
        // the system does not allow more than one role per user
        // no time to allow it to handle more than one :^) so
        // we assume that there is no other thing in the list than the role we need
        return roleList.First();
    }
}

 