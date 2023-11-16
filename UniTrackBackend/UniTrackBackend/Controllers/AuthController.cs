using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Interfaces;
using UniTrackBackend.Models;
using UniTrackBackend.ViewModels;

namespace UniTrackBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthService _authService;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    // Login Endpoint
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password)) return Unauthorized();
        
        var token = _authService.GenerateJwtToken(user);
        var refreshToken = await _authService.GenerateRefreshToken(user);
            
        Response.Cookies.Append("RefreshToken", refreshToken, _authService.GetRefreshCookieOptions());
        Response.Cookies.Append("AccessToken", token, _authService.GetAccessCookieOptions());
        
        return Ok(new { token });

    }

    // Register Endpoint
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);
        
        await _signInManager.SignInAsync(user, isPersistent: false);
        var token = _authService.GenerateJwtToken(user);
        var refreshToken = await _authService.GenerateRefreshToken(user);
        return Ok(new { token, refreshToken });

    }

    // Refresh Token Endpoint
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["RefreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is required");
        }

        var user = await _authService.GetUserFromRefreshToken(refreshToken);
        
        if (user is null)
        {
            return BadRequest("Invalid refresh token");
        }

        var newAccessToken = _authService.GenerateJwtToken(user);
        var newRefreshToken = await _authService.GenerateRefreshToken(user);
        
        
        Response.Cookies.Append("RefreshToken", newRefreshToken, _authService.GetRefreshCookieOptions());
        Response.Cookies.Append("AccessToken", newAccessToken, _authService.GetAccessCookieOptions());
        return Ok("Token refreshed");
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["RefreshToken"];
        
        if (!string.IsNullOrEmpty(refreshToken))
        {
            var user = await _authService.GetUserFromRefreshToken(refreshToken);
            if (user != null)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
        }
        
        Response.Cookies.Delete("RefreshToken", new CookieOptions { Secure = true, HttpOnly = true });

        return Ok();
    }

    
}