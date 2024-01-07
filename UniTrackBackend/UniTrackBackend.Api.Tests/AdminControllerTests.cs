using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;

namespace UniTrackBackend.Api.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public async Task CreateUser_ReturnsAppropriateResponse()
        {
            // Arrange
            var fakeAdminService = A.Fake<IAdminService>();
            var user = new User
            {
                Id = "1",
                UserName = "testuser1",
                Email = "testuser1@example.com",
                FirstName = "John",
                LastName = "Doe",
                RefreshToken = "SomeRefreshToken",
                RefreshTokenValidity = DateTime.UtcNow.AddDays(7)
            };

            var identityResult = IdentityResult.Success;
            A.CallTo(() => fakeAdminService.CreateUserAsync(A<User>.Ignored)).Returns(identityResult);

            var controller = new AdminController(fakeAdminService);

            // Act
            var result = await controller.CreateUser(user);

            // Assert
            if (identityResult.Succeeded)
            {
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("User created successfully", okResult.Value);
            }
            else
            {
                Assert.IsType<BadRequestResult>(result);
            }
        }


        [Fact]
        public async Task GetUser_ReturnsCorrectUser()
        {
            // Arrange
            var fakeAdminService = A.Fake<IAdminService>();
            var user = new User
            {
                Id = "2",
                UserName = "testuser2",
                Email = "testuser2@example.com",
                FirstName = "Ivan",
                LastName = "Ivanov",
                RefreshToken = "SomeRefreshToken",
                RefreshTokenValidity = DateTime.UtcNow.AddDays(7)
            };
            A.CallTo(() => fakeAdminService.GetUserByIdAsync(A<string>.Ignored)).Returns(user);

            var controller = new AdminController(fakeAdminService);

            // Act
            var result = await controller.GetUser(user.Id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okObjectResult.Value);
        }

        // [Fact]
        // public async Task GetAllUsers_ReturnsAllUsers()
        // {
        //     // Arrange
        //     var fakeAdminService = A.Fake<IAdminService>();
        //     var users = new List<User>
        //     {
        //         new User
        //         {
        //             Id = "1",
        //             UserName = "user1",
        //             Email = "user1@example.com",
        //             FirstName = "John",
        //             LastName = "Doe",
        //             RefreshToken = "Token123",
        //             RefreshTokenValidity = DateTime.UtcNow.AddDays(10)
        //         },
        //         new User
        //         {
        //             Id = "2",
        //             UserName = "user2",
        //             Email = "user2@example.com",
        //             FirstName = "Alice",
        //             LastName = "Smith",
        //             RefreshToken = "Token456",
        //             RefreshTokenValidity = DateTime.UtcNow.AddDays(5)
        //         },
        //         new User
        //         {
        //             Id = "3",
        //             UserName = "user3",
        //             Email = "user3@example.com",
        //             FirstName = "Bob",
        //             LastName = "Brown"
        //         }
        //     };
        //     A.CallTo(() => fakeAdminService.GetAllUsers()).Returns(users);
        //
        //     var controller = new AdminController(fakeAdminService);
        //
        //     // Act
        //     var result = await controller.GetAllUsers();
        //
        //     // Assert
        //     var okObjectResult = Assert.IsType<OkObjectResult>(result);
        //     Assert.Equal(users, okObjectResult.Value);
        // }

        [Fact]
        public async Task UpdateUser_ReturnsAppropriateResponse()
        {
            // Arrange
            var fakeAdminService = A.Fake<IAdminService>();
            var user = new User
            {
                Id = "3",
                UserName = "testuser3",
                Email = "testuser3@example.com",
                FirstName = "Alice",
                LastName = "Jas",
                RefreshToken = "ExpiredRefreshToken",
                RefreshTokenValidity = DateTime.UtcNow.AddDays(-1)
            };
            var identityResult = IdentityResult.Success;
            A.CallTo(() => fakeAdminService.UpdateUserAsync(A<User>.Ignored)).Returns(identityResult);

            var controller = new AdminController(fakeAdminService);

            // Act
            var result = await controller.UpdateUser(user.Id, user);

            // Assert
            if (identityResult.Succeeded)
            {
                Assert.IsType<NoContentResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }


        [Fact]
        public async Task DeleteUser_ReturnsAppropriateResponse()
        {
            // Arrange
            var fakeAdminService = A.Fake<IAdminService>();
            var identityResult = IdentityResult.Success;
            A.CallTo(() => fakeAdminService.DeleteUserAsync(A<string>.Ignored)).Returns(identityResult);

            var controller = new AdminController(fakeAdminService);

            // Act
            var result = await controller.DeleteUser("1"); // Use a test user ID

            // Assert
            if (identityResult.Succeeded)
            {
                Assert.IsType<NoContentResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}
