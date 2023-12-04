using Microsoft.AspNetCore.Mvc;
using Moq;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Controllers;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.AbsenceService;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Api.Tests
{
    public class AbsenceControllerTests
    {
        [Fact]
        public async Task PostAbsence_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockAbsenceService = new Mock<IAbsenceService>();
            var mockMapper = new Mock<IMapper>();
            var absence = new Absence { /* ... properties ... */ };
            var newAbsence = new Absence { Id = 1, /* ... other properties ... */ };

            mockAbsenceService.Setup(service => service.AddAbsenceAsync(absence))
                              .ReturnsAsync(newAbsence);

            var controller = new AbsencesController(mockMapper.Object, mockAbsenceService.Object);

            // Act
            var result = await controller.PostAbsence(absence);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetAbsenceById", actionResult.ActionName);
            Assert.Equal(newAbsence.Id, ((Absence)actionResult.Value).Id);
        }

        [Fact]
        public async Task GetAbsenceById_ReturnsAbsenceViewModel()
        {
            // Arrange
            var mockAbsenceService = new Mock<IAbsenceService>();
            var mockMapper = new Mock<IMapper>();
            var absenceId = 1;
            var absences = new List<Absence> { new Absence { Id = absenceId, /* ... properties ... */ } };
            var viewModel = new AbsenceViewModel { /* ... properties ... */ };

            mockAbsenceService.Setup(service => service.GetAbsencesByStudentIdAsync(absenceId))
                              .ReturnsAsync(absences);

            mockMapper.Setup(mapper => mapper.MapAbsenceViewModel(It.IsAny<Absence>()))
                      .Returns(viewModel);

            var controller = new AbsencesController(mockMapper.Object, mockAbsenceService.Object);

            // Act
            var result = await controller.GetAbsenceById(absenceId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AbsenceViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<AbsenceViewModel>(okResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetAllAbsences_ReturnsAllAbsences()
        {
            // Arrange
            var mockAbsenceService = new Mock<IAbsenceService>();
            var mockMapper = new Mock<IMapper>();
            var absences = new List<Absence>
        {
            // Add test data here
            new Absence() { /* ... properties ... */ },
            new Absence() { /* ... properties ... */ }
        };
            var viewModel = new AbsenceViewModel()
            {
                //Subject = "Mathematics",
                //AbsenceCount = 3,
                //Excused = false,
                //Date = new DateTime(2023,12,3),
                //StudentId = 123, 
                //TeacherId = 456, 
                //TeacherFirstName = "Ivan",
                //TeacherLastName = "Vanov"
            };

            mockAbsenceService.Setup(service => service.GetAbsencesAsync())
                              .ReturnsAsync(absences);

            mockMapper.Setup(mapper => mapper.MapAbsenceViewModel(It.IsAny<Absence>()))
                      .Returns(viewModel);

            var controller = new AbsencesController(mockMapper.Object, mockAbsenceService.Object);

            // Act
            var result = await controller.GetAllAbsences();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<AbsenceViewModel>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<AbsenceViewModel>>(okResult.Value);

            Assert.NotNull(returnValue);
            Assert.NotEmpty(returnValue);
        }
        [Fact]
        public async Task UpdateAbsence_ReturnsNoContentResult()
        {
            // Arrange
            var mockAbsenceService = new Mock<IAbsenceService>();
            var mockMapper = new Mock<IMapper>();
            var absenceId = 1;
            var absence = new Absence { Id = absenceId, /* ... properties ... */ };

            mockAbsenceService.Setup(service => service.UpdateAbsenceAsync(absence))
                              .Returns(Task.CompletedTask);

            var controller = new AbsencesController(mockMapper.Object, mockAbsenceService.Object);

            // Act
            var result = await controller.UpdateAbsence(absenceId, absence);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteAbsence_ReturnsNoContentResult()
        {
            // Arrange
            var mockAbsenceService = new Mock<IAbsenceService>();
            var absenceId = 1;

            mockAbsenceService.Setup(service => service.DeleteAbsenceAsync(absenceId))
                              .Returns(Task.CompletedTask);

            var controller = new AbsencesController(null, mockAbsenceService.Object); // Mapper not needed for delete

            // Act
            var result = await controller.DeleteAbsence(absenceId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }



    }
}
