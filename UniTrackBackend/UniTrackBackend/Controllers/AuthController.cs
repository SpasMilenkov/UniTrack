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
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Authenticates a user and provides a JWT and refresh token.
    /// </summary>
    /// <remarks>
    /// This endpoint authenticates the user based on the provided email and password.
    /// If authentication is successful, it returns a JWT for accessing protected resources and a refresh token.
    /// The tokens are set as cookies in the response.
    /// </remarks>
    /// <param name="model">The login credentials (email and password).</param>
    /// <response code="200">Successful login with JWT and refresh token.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Registers a new user and provides a JWT and refresh token.
    /// </summary>
    /// <remarks>
    /// This endpoint registers a new user with the provided details.
    /// On successful registration, it returns a JWT and a refresh token.
    /// </remarks>
    /// <param name="model">Registration details including email, password, and name.</param>
    /// <response code="200">Successful registration with JWT and refresh token.</response>
    /// <response code="400">Bad request if registration fails (e.g., email already in use).</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Refreshes the JWT using a refresh token.
    /// </summary>
    /// <remarks>
    /// This endpoint uses the refresh token provided in cookies to generate a new JWT and refresh token.
    /// If the refresh token is valid, it returns new tokens as cookies in the response.
    /// </remarks>
    /// <response code="200">Successful refresh with new JWT and refresh token.</response>
    /// <response code="400">Bad request if the refresh token is missing or invalid.</response>
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    
    /// <summary>
    /// Logs out the user by invalidating the refresh token.
    /// </summary>
    /// <remarks>
    /// This endpoint logs out the current user by invalidating the refresh token.
    /// It deletes the refresh token cookie from the response.
    /// </remarks>
    /// <response code="200">Successful logout.</response>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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