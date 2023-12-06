using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.AdminService;
using Xunit;

namespace UniTrackBackend.Api.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public async Task CreateUser_ReturnsAppropriateResponse()
        {
            // Arrange
            var mockAdminService = new Mock<IAdminService>();
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
            mockAdminService.Setup(s => s.CreateUserAsync(It.IsAny<User>())).ReturnsAsync((identityResult));

            var controller = new AdminController(mockAdminService.Object);

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
            var mockAdminService = new Mock<IAdminService>();
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
            mockAdminService.Setup(s => s.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            var controller = new AdminController(mockAdminService.Object);

            // Act
            var result = await controller.GetUser(user.Id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okObjectResult.Value);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var mockAdminService = new Mock<IAdminService>();
            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                    UserName = "user1",
                    Email = "user1@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    RefreshToken = "Token123",
                    RefreshTokenValidity = DateTime.UtcNow.AddDays(10)
                },
                new User
                {
                    Id = "2",
                    UserName = "user2",
                    Email = "user2@example.com",
                    FirstName = "Alice",
                    LastName = "Smith",
                    RefreshToken = "Token456",
                    RefreshTokenValidity = DateTime.UtcNow.AddDays(5)
                },
                new User
                {
                    Id = "3",
                    UserName = "user3",
                    Email = "user3@example.com",
                    FirstName = "Bob",
                    LastName = "Brown"
                }
            };
            mockAdminService.Setup(s => s.GetAllUsers()).Returns(users);

            var controller = new AdminController(mockAdminService.Object);

            // Act
            var result = await controller.GetAllUsers();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okObjectResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsAppropriateResponse()
        {
            // Arrange
            var mockAdminService = new Mock<IAdminService>();
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
            mockAdminService.Setup(s => s.UpdateUserAsync(It.IsAny<User>())).ReturnsAsync(identityResult);

            var controller = new AdminController(mockAdminService.Object);

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
            var mockAdminService = new Mock<IAdminService>();
            var identityResult = IdentityResult.Success;
            mockAdminService.Setup(s => s.DeleteUserAsync(It.IsAny<string>())).ReturnsAsync(identityResult);

            var controller = new AdminController(mockAdminService.Object);

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
