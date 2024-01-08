using System.Linq.Expressions;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Commons.Exceptions;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Services.Tests;

public class MarkServiceTests
{
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly ILogger<MarkService> _fakeLogger;
    private readonly IMapper _fakeMapper;
    private readonly MarkService _service;

    public MarkServiceTests()
    {
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _fakeLogger = A.Fake<ILogger<MarkService>>();
        _fakeMapper = A.Fake<IMapper>();
        _service = new MarkService(_fakeUnitOfWork, _fakeLogger, _fakeMapper);
    }

    public class AddMarkAsyncTests : MarkServiceTests
    {
        [Fact]
        public async Task AddMarkAsync_ValidMark_ReturnsMarkResultDto()
        {
            // Arrange
            var mark = new Mark
            {
                /* set properties */
            };
            var markResultDto = new MarkResultDto(
                Value: "5",
                StudentId: "S1234",
                TeacherId: "T567",
                SubjectId: "Sub89",
                Topic: "Algebra",
                GradedOn: "2024-01-08",
                SubjectName: "Mathematics",
                TeacherFirstName: "John",
                TeacherLastName: "Doe"
            );

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.AddAsync(mark)).DoesNothing();
            A.CallTo(() => _fakeUnitOfWork.SaveAsync()).Returns(Task.CompletedTask);
            A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(markResultDto);

            // Act
            Task createUserTask = _service.AddMarkAsync(mark);
            createUserTask.Wait();
            // Assert
            Assert.True(createUserTask.IsCompletedSuccessfully);
        }

        public class GetMarkByIdAsyncTests : MarkServiceTests
        {
            [Fact]
            public async Task GetMarkByIdAsync_ExistingId_ReturnsMarkResultDto()
            {
                // Arrange
                var markId = 1;
                var mark = new Mark
                {
                    /* set properties */
                };
                var markResultDto = new MarkResultDto(
                    Value: "5",
                    StudentId: "S1234",
                    TeacherId: "T567",
                    SubjectId: "Sub89",
                    Topic: "Algebra",
                    GradedOn: "2024-01-08",
                    SubjectName: "Mathematics",
                    TeacherFirstName: "John",
                    TeacherLastName: "Doe"
                );

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId)).Returns(Task.FromResult(mark));
                A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(markResultDto);

                // Act
                var result = await _service.GetMarkByIdAsync(markId);

                // Assert
                Assert.Equal(markResultDto, result);
            }

            [Fact]
            public async Task GetMarkByIdAsync_NonExistingId_ThrowsDataNotFoundException()
            {
                // Arrange
                var markId = 1;

                A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId))
                    .Returns(Task.FromResult<Mark>(null));

                // Act & Assert
                await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.GetMarkByIdAsync(markId));
            }
        }

        
        
    }

    public class GetMarksBySubjectAsyncTests : MarkServiceTests
    {
        [Fact]
        public async Task GetMarksBySubjectAsync_ValidSubjectId_ReturnsMarks()
        {
            // Arrange
            var subjectId = 1;
            var marks = new List<Mark>
            {
                new Mark { Value = 88, SubjectId = subjectId, TeacherId = 2, StudentId = 3, Topic = "Algebra", GradedOn = new DateTime(2024, 1, 8) }
            };
            var marksResultDto = marks.Select(m => new MarkResultDto(
                Value: m.Value.ToString(),
                StudentId: $"S{m.StudentId}",
                TeacherId: $"T{m.TeacherId}",
                SubjectId: $"Sub{m.SubjectId}",
                Topic: m.Topic,
                GradedOn: m.GradedOn.ToString("yyyy-MM-dd"),
                SubjectName: "Mathematics",
                TeacherFirstName: "John",
                TeacherLastName: "Doe"
            )).ToList();

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                .Returns(Task.FromResult<IEnumerable<Mark>>(marks));
            foreach (var mark in marks)
            {
                A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(marksResultDto.First());
            }

            // Act
            var result = await _service.GetMarksBySubjectAsync(subjectId);

            // Assert
            Assert.Equal(marksResultDto, result.ToList(), new MarkResultDtoComparer());
        }

        [Fact]
        public async Task GetMarksBySubjectAsync_OnError_ThrowsDataNotFoundException()
        {
            // Arrange
            var subjectId = 1;
            A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                .Throws<Exception>();

            // Act & Assert
            await Assert.ThrowsAsync<DataNotFoundException>(
                async () => await _service.GetMarksBySubjectAsync(subjectId));
        }
    }

    public class GetMarksByDateAsyncTests : MarkServiceTests
    {
        [Fact]
        public async Task GetMarksByDateAsync_ValidDate_ReturnsMarks()
        {
            // Arrange
            var date = new DateTime(2021, 04, 05);
            var marks = new List<Mark>
            {
                new Mark { Value = 92, GradedOn = date, TeacherId = 2, StudentId = 3, Topic = "Algebra" }
            };
            var marksResultDto = marks.Select(m => new MarkResultDto(
                Value: m.Value.ToString(),
                StudentId: $"S{m.StudentId}",
                TeacherId: $"T{m.TeacherId}",
                SubjectId: $"Sub89", // Assuming a fixed SubjectId for the sake of example
                Topic: m.Topic,
                GradedOn: m.GradedOn.ToString("yyyy-MM-dd"),
                SubjectName: "Mathematics",
                TeacherFirstName: "John",
                TeacherLastName: "Doe"
            )).ToList();

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                .Returns(Task.FromResult<IEnumerable<Mark>>(marks));
            foreach (var mark in marks)
            {
                A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(marksResultDto.First());
            }

            // Act
            var result = await _service.GetMarksByDateAsync(date);

            // Assert
            Assert.Equal(marksResultDto, result.ToList(), new MarkResultDtoComparer());
        }


        [Fact]
        public async Task GetMarksByDateAsync_OnError_ThrowsDataNotFoundException()
        {
            // Arrange
            var date = new DateTime(2021, 04, 05);
            A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetAsync(A<Expression<Func<Mark, bool>>>.Ignored, null))
                .Throws<Exception>();

            // Act & Assert
            await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.GetMarksByDateAsync(date));
        }
    }

    public class UpdateMarkAsyncTests : MarkServiceTests
    {
        [Fact]
        public async Task UpdateMarkAsync_ValidMark_ReturnsUpdatedMarkDto()
        {
            // Arrange
            var markId = 1;
            var mark = new Mark { Id = markId, Value = 95, TeacherId = 2, StudentId = 3 };
            var updatedMarkResultDto = new MarkResultDto(
                Value: "5",
                StudentId: "S1234",
                TeacherId: "T567",
                SubjectId: "Sub89",
                Topic: "Algebra",
                GradedOn: "2024-01-08",
                SubjectName: "Mathematics",
                TeacherFirstName: "John",
                TeacherLastName: "Doe"
            );

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.UpdateAsync(mark)).DoesNothing();
            A.CallTo(() => _fakeUnitOfWork.SaveAsync()).Returns(Task.CompletedTask);
            A.CallTo(() => _fakeMapper.MapMarkDto(mark)).Returns(updatedMarkResultDto);

            // Act
            var result = await _service.UpdateMarkAsync(mark, markId);

            // Assert
            Assert.Equal(updatedMarkResultDto, result);
        }

        [Fact]
        public async Task UpdateMarkAsync_OnError_ThrowsDataNotFoundException()
        {
            // Arrange
            var markId = 1;
            var mark = new Mark
            {
                /* set properties */
            };

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.UpdateAsync(mark)).Throws<Exception>();

            // Act & Assert
            await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.UpdateMarkAsync(mark, markId));
        }
    }

    public class DeleteMarkAsyncTests : MarkServiceTests
    {
        [Fact]
        public async Task DeleteMarkAsync_ValidId_DeletesMark()
        {
            // Arrange
            var markId = 1;
            var mark = new Mark { Id = markId };

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId))
                .Returns(Task.FromResult(mark));
            A.CallTo(() => _fakeUnitOfWork.MarkRepository.DeleteAsync(markId)).DoesNothing();
            A.CallTo(() => _fakeUnitOfWork.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteMarkAsync(markId);

            // Assert - no exception is thrown
            A.CallTo(() => _fakeUnitOfWork.MarkRepository.DeleteAsync(markId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteMarkAsync_OnError_ThrowsDataNotFoundException()
        {
            // Arrange
            var markId = 1;

            A.CallTo(() => _fakeUnitOfWork.MarkRepository.GetByIdAsync(markId)).Throws<Exception>();

            // Act & Assert
            await Assert.ThrowsAsync<DataNotFoundException>(async () => await _service.DeleteMarkAsync(markId));
        }
    }
}
/// <summary>
/// Helper class for comparing DTOs
/// </summary>
public class MarkResultDtoComparer : IEqualityComparer<MarkResultDto>
{
    public bool Equals(MarkResultDto x, MarkResultDto y)
    {
        if (x == null || y == null)
            return false;
        return x.Value == y.Value &&
               x.StudentId == y.StudentId &&
               x.TeacherId == y.TeacherId &&
               x.SubjectId == y.SubjectId &&
               x.Topic == y.Topic &&
               x.GradedOn == y.GradedOn &&
               x.SubjectName == y.SubjectName &&
               x.TeacherFirstName == y.TeacherFirstName &&
               x.TeacherLastName == y.TeacherLastName;
    }

    public int GetHashCode(MarkResultDto obj)
    {
        return obj.GetHashCode();
    }
}
