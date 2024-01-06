using System.Globalization;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Mappings;

public class Mapper : IMapper
{
    private readonly ILogger<Mapper> _logger;
    public Mapper(ILogger<Mapper> logger)
    {
        _logger = logger;
    }
    public Mark? MapMark(MarkDto? model)
    {
        if (model is null)
            return null;
        try
        {
            var mark = new Mark
            {
                StudentId = model.StudentId,
                TeacherId = model.TeacherId,
                SubjectId = model.SubjectId,
                Value = model.Value,
                GradedOn =  model.GradedOn,
                Topic = model.Topic
            };
            return mark;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to convert mark view model to mark entity");
            return null;
        }
    }
    public Absence? MapAbsence(AbsenceDto model)
    {
        try
        {
            var absence = new Absence
            {
                StudentId = model.StudentId,
                TeacherId = model.TeacherId,
                SubjectId = model.SubjectId,
                Value = model.Value,
                Time = model.Time.Date,
                Excused = model.Excused
            };
            return absence;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map AbsenceDto to Absence");
            return null;
        }
    }

    public StudentResultDto? MapStudentDto(Student student)
    {
        try
        {
            var absenceViewModels = new List<AbsenceResultDto>();
            foreach (var absence in student.Absences)
            {
                absenceViewModels.Add(MapAbsenceResultDto(absence));
            }

            var markViewModels = new List<MarkResultDto>();
            foreach (var mark in student.Marks)
            {
                markViewModels.Add(MapMarkDto(mark));
            }
            var model = new StudentResultDto()
            {
                Id = student.Id.ToString(),
                FirstName = student.User.FirstName,
                LastName = student.User.LastName,
                UniId = student.Id.ToString(),
                ClassId = student.GradeId.ToString(),
                ClassTeacherId = student.Grade.ClassTeacherId.ToString(),
                ClassTeacherFirstName = student.Grade.ClassTeacher.User.FirstName,
                ClassTeacherLastName = student.Grade.ClassTeacher.User.LastName,
                AvatarUrl = student.User.AvatarUrl,
                Type = "STUDENT",
                ClassName = student.Grade.Name,
                Email = student.User.Email,
                Marks = markViewModels,
                Absences = absenceViewModels
            };
            return model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public AbsenceResultDto MapAbsenceResultDto(Absence absence)
    {
        try
        {
            var model = new AbsenceResultDto()
            {
                AbsenceId = absence.Id,
                Subject = absence.Subject.Name,
                AbsenceCount = absence.Value,
                Excused = absence.Excused,
                Date = absence.Time.Date,
                StudentId = absence.StudentId,
                TeacherId = absence.TeacherId,
                TeacherFirstName = absence.Teacher.User.FirstName,
                TeacherLastName = absence.Teacher.User.LastName
                
            };
            return model;
        }
        catch (Exception e)
        {
            _logger.LogError("Error while trying to map absence entity to view model");
            throw;
        }
    }

    public MarkResultDto MapMarkDto(Mark mark)
    {
        try
        {
            var model = new MarkResultDto(
                Math.Round(mark.Value).ToString(CultureInfo.InvariantCulture),
                mark.StudentId.ToString(),
                mark.TeacherId.ToString(),
                mark.SubjectId.ToString(),
                mark.Topic,
                mark.GradedOn.ToString("O"),
                mark.Subject.Name,
                mark.Teacher.User.FirstName,
                mark.Teacher.User.LastName
                
                );
            return model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public TeacherResultDto? MapTeacherDto(Teacher teacher, string? classId = null, string? className = null)
    {
        try
        {
            var model = new TeacherResultDto
            {
                Id = teacher.Id.ToString(),
                UniId = teacher.SchoolId.ToString(),
                AvatarUrl = teacher.User.AvatarUrl,
                FirstName = teacher.User.FirstName,
                LastName = teacher.User.LastName,
                Type = "TEACHER",
                ClassId = classId,
                ClassName = className,
                Subjects = teacher.Subjects.Select(s => s.Name).ToList()
            };
            return model;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map teacher to TeacherViewModel");
            return null;
        }
        
    }

    public Subject MapSubject(SubjectDto model)
    {
        try
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var subject = new Subject
            {
                Name = model.Name,
                Teachers = new List<Teacher>() 
            };
            return subject;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map teacher to Subject");
            return null;
        }
    }

    public SubjectResultDto? MapSubjectResultDto(Subject subject)
    {
        try
        {
            var viewModel = new SubjectResultDto
            {
                Id = subject.Id,
                Name = subject.Name,
                TeacherNames = subject.Teachers.Select(t => t.Id.ToString())
            };

            return viewModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map teacher to TeacherViewModel");
            return null;
        }
    }
    

    public LoginResultDto? MapLoginResult( string userId, string role, string avatarUrl)
    {
        try
        {
            var model = new LoginResultDto
            {
                UserId = userId,
                UserRole = role,
                AvatarUrl = avatarUrl
            };
            return model;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map teacher to TeacherViewModel");
            return null;
        }
    }
}