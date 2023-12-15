using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Auth;
using UniTrackBackend.Services.Messaging;

namespace UniTrackBackend.Api.Tests;

public class AuthControllerTests
{
    private readonly IAuthService _fakeAuthService;
    private readonly IEmailService _fakeEmailService;
    private readonly AuthController _controller;
    private readonly HttpContext _fakeHttpContext;
    private readonly IUrlHelper _fakeUrlHelper;
    private readonly HttpRequest _fakeHttpRequest;


    public AuthControllerTests()
    {
        _fakeAuthService = A.Fake<IAuthService>();
        _fakeEmailService = A.Fake<IEmailService>();
        _controller = new AuthController(_fakeAuthService, _fakeEmailService);

        // Mock HttpContext
        _fakeHttpContext = A.Fake<HttpContext>();
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = _fakeHttpContext
        };

        // Mock Response
        var response = A.Fake<HttpResponse>();
        A.CallTo(() => _fakeHttpContext.Response).Returns(response);

        // Mock Response.Cookies
        var cookieCollection = A.Fake<IResponseCookies>();
        A.CallTo(() => response.Cookies).Returns(cookieCollection);

        _fakeHttpRequest = A.Fake<HttpRequest>();
        _fakeUrlHelper = A.Fake<IUrlHelper>();

        A.CallTo(() => _fakeHttpContext.Request).Returns(_fakeHttpRequest);
        A.CallTo(() => _fakeHttpRequest.Scheme).Returns("http");

        _controller.ControllerContext = new ControllerContext() { HttpContext = _fakeHttpContext };
        _controller.Url = _fakeUrlHelper;
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
        A.CallTo(() => _fakeAuthService.LoginUser(userModel)).Returns(user);
        A.CallTo(() => _fakeAuthService.GenerateJwtToken(user)).Returns("fake-jwt-token");
        A.CallTo(() => _fakeAuthService.GenerateRefreshToken(user)).Returns("fake-refresh-token");

        var cookies = A.Fake<IResponseCookies>();
        A.CallTo(() => _fakeHttpContext.Response.Cookies).Returns(cookies);


        // Act
        var result = await _controller.Login(userModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        // Verify that cookies are set
        A.CallTo(() => cookies.Append(A<string>.Ignored, A<string>.Ignored, A<CookieOptions>.Ignored))
            .MustHaveHappened();

    }


    [Fact]
    public async Task Login_Unsuccessful_ReturnsUnauthorizedResult()
    {
        // Arrange
        var userModel = new LoginViewModel { Email = "wrong@example.com", Password = "Password123" };

        A.CallTo(() => _fakeAuthService.LoginUser(userModel)).Returns((User)null);

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

        A.CallTo(() => _fakeAuthService.RegisterUser(registerModel)).Returns(user);
        A.CallTo(() => _fakeAuthService.GetEmailConfirmationToken(user)).Returns("confirmation-token");
        A.CallTo(() => _fakeAuthService.GenerateJwtToken(user)).Returns("fake-jwt-token");
        A.CallTo(() => _fakeAuthService.GenerateRefreshToken(user)).Returns("fake-refresh-token");
        A.CallTo(() => _fakeAuthService.SignInUser(user)).Returns(Task.CompletedTask);

        A.CallTo(() => _fakeUrlHelper.Action(A<UrlActionContext>.Ignored))
            .Returns("http://fakeurl.com/confirm-email");

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        // Verify that Url.Action is called to generate the callback URL
        A.CallTo(() => _fakeUrlHelper.Action(A<UrlActionContext>.That.Matches(uac =>
            uac.Values != null &&
            uac.Action == "ConfirmEmail" && 
            uac.Controller == "Auth" && 
            uac.Values.ToString()!.Contains(user.Id) &&
            uac.Values.ToString()!.Contains("confirmation-token") &&
            uac.Protocol == "http"))).MustHaveHappenedOnceExactly();

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

        A.CallTo(() => _fakeAuthService.RegisterUser(registerModel)).Returns(user);
        A.CallTo(() => _fakeAuthService.GetEmailConfirmationToken(user)).Returns((string)null);

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

        A.CallTo(() => _fakeAuthService.RegisterUser(registerModel))!.Returns((User)null);


        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }


}