using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Tests;

public class AdminServiceTests
{
    private readonly AdminService _adminService;
    private readonly UserManager<User> _userManager;

    public AdminServiceTests()
    {
        _userManager = A.Fake<UserManager<User>>();
        _adminService = new AdminService(_userManager);
    }

    [Fact]
    public async Task CreateUserAsync_CallsUserManagerCreateAsync()
    {
        var user = new User
        {
            FirstName = null,
            LastName = null
        };
        var identityResult = IdentityResult.Success;

        A.CallTo(() => _userManager.CreateAsync(user)).Returns(identityResult);

        var result = await _adminService.CreateUserAsync(user);

        A.CallTo(() => _userManager.CreateAsync(user)).MustHaveHappenedOnceExactly();
        Assert.Equal(identityResult, result);
    }
    [Fact]
    public async Task GetUserByIdAsync_CallsUserManagerFindByIdAsync()
    {
        var user = new User
        {
            Id = "testId",
            FirstName = null,
            LastName = null
        };

        A.CallTo(() => _userManager.FindByIdAsync(user.Id)).Returns(user);

        var result = await _adminService.GetUserByIdAsync(user.Id);

        A.CallTo(() => _userManager.FindByIdAsync(user.Id)).MustHaveHappenedOnceExactly();
        Assert.Equal(user, result);
    }
    [Fact]
    public void GetAllUsers_ReturnsAllUsers()
    {
        var users = new List<User> { new User
        {
            FirstName = null,
            LastName = null
        }, new User
            {
                FirstName = null,
                LastName = null
            }
        };
        var userManagerUsers = A.Fake<IQueryable<User>>();
        A.CallTo(() => userManagerUsers.Provider).Returns(users.AsQueryable().Provider);
        A.CallTo(() => userManagerUsers.Expression).Returns(users.AsQueryable().Expression);
        A.CallTo(() => userManagerUsers.ElementType).Returns(users.AsQueryable().ElementType);
        A.CallTo(() => userManagerUsers.GetEnumerator()).Returns(users.GetEnumerator());
        A.CallTo(() => _userManager.Users).Returns(userManagerUsers);

        var result = _adminService.GetAllUsers();

        Assert.Equal(users, result);
    }
    [Fact]
    public async Task UpdateUserAsync_CallsUserManagerUpdateAsync()
    {
        var user = new User
        {
            FirstName = null,
            LastName = null
        };
        var identityResult = IdentityResult.Success;

        A.CallTo(() => _userManager.UpdateAsync(user)).Returns(identityResult);

        var result = await _adminService.UpdateUserAsync(user);

        A.CallTo(() => _userManager.UpdateAsync(user)).MustHaveHappenedOnceExactly();
        Assert.Equal(identityResult, result);
    }
    [Fact]
    public async Task DeleteUserAsync_WhenUserExists_CallsUserManagerDeleteAsync()
    {
        var user = new User
        {
            FirstName = null,
            LastName = null
        };
        user.Id = "testId";
        var identityResult = IdentityResult.Success;

        A.CallTo(() => _userManager.FindByIdAsync(user.Id)).Returns(user);
        A.CallTo(() => _userManager.DeleteAsync(user)).Returns(identityResult);

        var result = await _adminService.DeleteUserAsync(user.Id);

        A.CallTo(() => _userManager.DeleteAsync(user)).MustHaveHappenedOnceExactly();
        Assert.Equal(identityResult, result);
    }

    [Fact]
    public async Task DeleteUserAsync_WhenUserDoesNotExist_ReturnsFailedResult()
    {
        var userId = "testId";

        A.CallTo(() => _userManager.FindByIdAsync(userId)).Returns((User)null);

        var result = await _adminService.DeleteUserAsync(userId);

        Assert.False(result.Succeeded);
    }
    [Fact]
    public async Task CreateUserAsync_WhenUserCreationFails_ReturnsFailureResult()
    {
        var user = new User
        {
            FirstName = null,
            LastName = null
        };
        var identityResult = IdentityResult.Failed();

        A.CallTo(() => _userManager.CreateAsync(user)).Returns(identityResult);

        var result = await _adminService.CreateUserAsync(user);

        Assert.False(result.Succeeded);
    }
    [Fact]
    public async Task GetUserByIdAsync_WhenExceptionThrown_ThrowsException()
    {
        var userId = "testId";

        A.CallTo(() => _userManager.FindByIdAsync(userId)).Throws<Exception>();

        await Assert.ThrowsAsync<Exception>(() => _adminService.GetUserByIdAsync(userId));
    }
    [Fact]
    public async Task UpdateUserAsync_WhenUpdateFails_ReturnsFailureResult()
    {
        var user = new User
        {
            FirstName = null,
            LastName = null
        };
        var identityResult = IdentityResult.Failed();

        A.CallTo(() => _userManager.UpdateAsync(user)).Returns(identityResult);

        var result = await _adminService.UpdateUserAsync(user);

        Assert.False(result.Succeeded);
    }
    [Fact]
    public async Task DeleteUserAsync_WhenExceptionThrown_ThrowsException()
    {
        var user = new User
        {
            Id = "testId",
            FirstName = null,
            LastName = null
        };

        A.CallTo(() => _userManager.FindByIdAsync(user.Id)).Returns(user);
        A.CallTo(() => _userManager.DeleteAsync(user)).Throws<Exception>();

        await Assert.ThrowsAsync<Exception>(() => _adminService.DeleteUserAsync(user.Id));
    }

}
