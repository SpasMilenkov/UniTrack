using Microsoft.AspNetCore.Mvc;
using Moq;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.AdminService;

namespace UniTrackBackend.Api.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task CreateUser_ReturnsCreatedResponse()
        {
            var mockAdminService = new Mock<IAdminService>();
            var user = new User {
                Id = "1",
                UserName = "testuser1",
                Email = "testuser1@example.com",
                FirstName = "John",
                LastName = "Doe",
                RefreshToken = "SomeRefreshToken",
                RefreshTokenValidity = DateTime.UtcNow.AddDays(7)
            };
            mockAdminService.Setup(s => s.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            var controller = new AdminController(mockAdminService.Object);

            
            var result = await controller.CreateUser(user);

            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(user, createdAtActionResult.Value);
        }

        [Fact]
        public async Task GetUser_ReturnsCorrectUser()
        {
            
            var mockAdminService = new Mock<IAdminService>();
            var user = new User
            {
                Id = "2",
                UserName = "testuser2",
                Email = "testuser1@example.com",
                FirstName = "Ivan",
                LastName = "Vanov",
                RefreshToken = "SomeRefreshToken",
                RefreshTokenValidity = DateTime.UtcNow.AddDays(7)
            };
            mockAdminService.Setup(s =>s.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var controller = new AdminController(mockAdminService.Object);

            var result = await controller.GetUser(int.Parse(user.Id));

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okObjectResult.Value);
        }

        [Fact]
        public async Task GetUser_ThrowsFormatExceptionForInvalidId()
        {
            // Arrange
            var mockAdminService = new Mock<IAdminService>();
            var controller = new AdminController(mockAdminService.Object);
            var invalidId = "invalid";

            // Act & Assert
            await Assert.ThrowsAsync<FormatException>(() => controller.GetUser(int.Parse(invalidId)));
        }

        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            
            var mockAdminService = new Mock<IAdminService>();
            var users = new List<User> {
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
                    LastName = "Brown",
                    // This user doesn't have a refresh token
                }
            };
            
            mockAdminService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

            var controller = new AdminController(mockAdminService.Object);

            var result = await controller.GetAllUsers();

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okObjectResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNoContentOnSuccess()
        {
            
            var mockAdminService = new Mock<IAdminService>();
            var user = new User {
                Id="3",
                UserName = "testuser3",
                Email = "testuser3@example.com",
                FirstName = "Alice",
                LastName = "Jas",
                RefreshToken = "ExpiredRefreshToken",
                RefreshTokenValidity = DateTime.UtcNow.AddDays(-1)
            };
            mockAdminService.Setup(s => s.UpdateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var controller = new AdminController(mockAdminService.Object);

            var result = await controller.UpdateUser(user.Id, user);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContentOnSuccess()
        {
            
            var mockAdminService = new Mock<IAdminService>();
            mockAdminService.Setup(s => s.DeleteUserAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new AdminController(mockAdminService.Object);
                       
            var result = await controller.DeleteUser(1); // Use a test user ID
                       
            Assert.IsType<NoContentResult>(result);
        }



    }
}
