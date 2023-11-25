using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Interfaces;

namespace UniTrackBackend.Api.Tests;
public class AccountControllerTests
{
    private readonly Mock<UserManager<User>> _mockUserManager;
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<SignInManager<User>> _mockSignInManager;
    private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
    private readonly AuthController _controller;

    public AccountControllerTests()
    {
        _mockSignInManager = new Mock<SignInManager<User>>();
        _mockRoleManager = new Mock<RoleManager<IdentityRole>>();
        var userStoreMock = new Mock<IUserStore<User>>();
        var userStore = userStoreMock.Object;
        
        _mockAuthService = new Mock<IAuthService>();
        
        _controller = new AuthController(_mockUserManager.Object, _mockSignInManager.Object, _mockAuthService.Object, _mockRoleManager.Object);
    }
    
    [Fact]
    public async Task Login_ReturnsOkWithToken_WhenCredentialsAreValid()
    {
        // Arrange
        var loginViewModel = new LoginViewModel { Email = "test@example.com", Password = "Password123" };
        var user = new User 
        {  
            UserName = loginViewModel.Email,
            Email = loginViewModel.Email,
            FirstName = "string",
            LastName = "string"
        };
        var token = "some.jwt.token";
        var refreshToken = "some.refresh.token";

        _mockUserManager.Setup(m => m.FindByEmailAsync(loginViewModel.Email)).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.CheckPasswordAsync(user, loginViewModel.Password)).ReturnsAsync(true);
        _mockAuthService.Setup(a => a.GenerateJwtToken(user)).Returns(token);
        _mockAuthService.Setup(a => a.GenerateRefreshToken(user)).ReturnsAsync(refreshToken);

        // Act
        var result = await _controller.Login(loginViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnToken = okResult.Value as dynamic;
        Assert.Equal(token, returnToken.token);
    }

}
