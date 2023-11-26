using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Interfaces;
using UniTrackBackend.Services.Messaging;

namespace UniTrackBackend.Api.Tests;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly AuthController _controller;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly Mock<IUrlHelper> _mockUrlHelper;
    private readonly Mock<HttpRequest> _mockHttpRequest;

    public AuthControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockEmailService = new Mock<IEmailService>();
        _controller = new AuthController(_mockAuthService.Object, _mockEmailService.Object);

        // Mock HttpContext
        _mockHttpContext = new Mock<HttpContext>();
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = _mockHttpContext.Object
        };

        // Mock Response
        var response = new Mock<HttpResponse>();
        _mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);

        // Mock Response.Cookies
        var cookieCollection = new Mock<IResponseCookies>();
        response.SetupGet(r => r.Cookies).Returns(cookieCollection.Object);
        
        _mockHttpRequest = new Mock<HttpRequest>();
        _mockUrlHelper = new Mock<IUrlHelper>();

        _mockHttpContext.SetupGet(x => x.Request).Returns(_mockHttpRequest.Object);
        _mockHttpRequest.SetupGet(x => x.Scheme).Returns("http");

        _controller.ControllerContext = new ControllerContext() { HttpContext = _mockHttpContext.Object };
        _controller.Url = _mockUrlHelper.Object;
    }


    [Fact]
    public async Task Login_Successful_ReturnsOkAndSetsCookies()
    {
        // Arrange
        var userModel = new LoginViewModel { Email = "test@example.com", Password = "Password123" };
        var user = new User
        {
            Id = "2",
            UserName = "new@example.com",
            FirstName = "string",
            LastName = "string",
        };

        _mockAuthService.Setup(s => s.LoginUser(userModel)).ReturnsAsync(user);
        _mockAuthService.Setup(s => s.GenerateJwtToken(user)).Returns("fake-jwt-token");
        _mockAuthService.Setup(s => s.GenerateRefreshToken(user)).ReturnsAsync("fake-refresh-token");

        // Mock the cookies setup
        var cookies = new Mock<IResponseCookies>();
        _mockHttpContext.SetupGet(x => x.Response.Cookies).Returns(cookies.Object);

        // Act
        var result = await _controller.Login(userModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        // Verify that cookies are set
        cookies.Verify(c => c.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()),
            Times.AtLeastOnce());
    }


    [Fact]
    public async Task Login_Unsuccessful_ReturnsUnauthorizedResult()
    {
        // Arrange
        var userModel = new LoginViewModel { Email = "wrong@example.com", Password = "Password123" };

        _mockAuthService.Setup(s => s.LoginUser(userModel)).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Login(userModel);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }
    [Fact]
    public async Task Register_Successful_ReturnsOkResult()
    {
        // Arrange
        var registerModel = new RegisterViewModel
        {
            Email = "new@example.com",
            Password = "Password1231",
            UserName = "string",
            FirstName = "string",
            LastName = "string",
            ConfirmPassword = "Password123!"
        };
        var user = new User
        {
            Id = "2",
            UserName = "new@example.com",
            FirstName = "string",
            LastName = "string",
        };

        _mockAuthService.Setup(s => s.RegisterUser(registerModel)).ReturnsAsync(user);
        _mockAuthService.Setup(s => s.GetEmailConfirmationToken(user)).ReturnsAsync("confirmation-token");
        _mockAuthService.Setup(s => s.GenerateJwtToken(user)).Returns("fake-jwt-token");
        _mockAuthService.Setup(s => s.GenerateRefreshToken(user)).ReturnsAsync("fake-refresh-token");
        _mockAuthService.Setup(s => s.SignInUser(user)).Returns(Task.CompletedTask);

        // Mock Url.Action
        _mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
            .Returns("http://fakeurl.com/confirm-email");

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        // Verify that Url.Action is called to generate the callback URL
        _mockUrlHelper.Verify(x => x.Action(It.Is<UrlActionContext>(
            uac => uac.Values != null &&
                   uac.Action == "ConfirmEmail" && 
                   uac.Controller == "Auth" && 
                   uac.Values.ToString()!.Contains(user.Id) &&
                   uac.Values.ToString()!.Contains("confirmation-token") &&
                   uac.Protocol == "http")), Times.Once());
    }
    
    
    [Fact]
    public async Task Register_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Model is invalid"); // Simulate model validation failure

        var registerModel = new RegisterViewModel
        {
            UserName = null,
            FirstName = null,
            LastName = null,
            Email = null,
            Password = null,
            ConfirmPassword = null
        }; // Invalid model (empty or incorrect data)

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    // [Fact]
    // public async Task Register_EmailInUse_ReturnsBadRequest()
    // {
    //     // Arrange
    //     var registerModel = new RegisterViewModel
    //     {
    //         Email = "existing@example.com",
    //         UserName = "new@example.com",
    //         FirstName = "string",
    //         LastName = "string",
    //         Password = "Password123!",
    //         ConfirmPassword = "Password123!",
    //         // Other properties...
    //     };
    //
    //     _mockAuthService.Setup(s => s.RegisterUser(registerModel))!.ReturnsAsync((User)null!); // Simulate email already in use
    //
    //     // Act
    //     var result = await _controller.Register(registerModel);
    //
    //     // Assert
    //     Assert.IsType<BadRequestObjectResult>(result);
    // }

    [Fact]
    public async Task Register_TokenGenerationFailure_ReturnsBadRequest()
    {
        // Arrange
        var registerModel = new RegisterViewModel
        {
            Email = "new@example.com",
            UserName = "new@example.com",
            FirstName = "string",
            LastName = "string",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            // Other properties...
        };
        var user = new User
        {
            Id = "3",
            UserName = "new@example.com",
            FirstName = "string",
            LastName = "string",
            // Other properties...
        };

        _mockAuthService.Setup(s => s.RegisterUser(registerModel)).ReturnsAsync(user);
        _mockAuthService.Setup(s => s.GetEmailConfirmationToken(user)).ReturnsAsync(() => null); // Simulate token generation failure

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async Task Register_EmailInUse_ReturnsBadRequest()
    {
        var registerModel = new RegisterViewModel
        {
            Email = "existing@example.com",
            UserName = "new@example.com",
            FirstName = "string",
            LastName = "string",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            // Other properties...
        };

        _mockAuthService.Setup(s => s.RegisterUser(registerModel))!.ReturnsAsync((User)null); // Simulate email already in use

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }


}