using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Mappings;

public class Mapper : IMapper
{
    private readonly ILogger<Mapper> _logger;
    public Mapper(ILogger<Mapper> logger)
    {
        _logger = logger;
    }
    public Mark? MapMark(MarkViewModel? model)
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
                GradedOn =  model.GradedOn
            };
            return mark;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to convert mark view model to mark entity");
            return null;
        }
    }
    public Absence? MapAbsence(AbsenceViewModel model)
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
            _logger.LogError(e, "Failed to map AbsenceViewModel to Absence");
            return null;
        }
    }

    public StudentResultViewModel? MapStudentViewModel(Student student)
    {
        try
        {
            var absenceViewModels = new List<AbsenceResultViewModel>();
            foreach (var absence in student.Absences)
            {
                absenceViewModels.Add(MapAbsenceResultViewModel(absence));
            }

            var markViewModels = new List<MarkViewModel>();
            foreach (var mark in student.Marks)
            {
                markViewModels.Add(MapMarkViewModel(mark));
            }
            var model = new StudentResultViewModel()
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

    public AbsenceResultViewModel MapAbsenceResultViewModel(Absence absence)
    {
        try
        {
            var model = new AbsenceResultViewModel()
            {
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

    public MarkViewModel? MapMarkViewModel(Mark mark)
    {
        try
        {
            var model  = new MarkViewModel
            {
                Id = mark.Id,
                Value = mark.Value,
                StudentId = mark.StudentId,
                TeacherId = mark.TeacherId,
                SubjectId = mark.SubjectId,
                GradedOn = mark.GradedOn
            };
            return model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public School? MapSchool(SchoolViewModel model)
    {
        try
        {
            var school = new School
            {
                // Id = model.Id,
                // Name = model.Name,
                // Teachers = model.Teachers,
                // Students = model.Students,
            };
            return school;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map SchoolViewModel to School");
            return null;
        }
    }

    public TeacherResultViewModel? MapTeacherViewModel(Teacher teacher, string? classId = null, string? className = null)
    {
        try
        {
            var model = new TeacherResultViewModel
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

    public Subject? MapSubject(SubjectViewModel model)
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

    public SubjectResultViewModel? MapSubjectViewModel(Subject subject)
    {
        try
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var viewModel = new SubjectResultViewModel
            {
                Name = subject.Name,
                TeacherIds = subject.Teachers.Select(t => t.Id.ToString())
            };

            return viewModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map teacher to TeacherViewModel");
            return null;
        }

    }

    public LoginResultViewModel? MapLoginResult( string userId, string role, string avatarUrl)
    {
        try
        {
            var model = new LoginResultViewModel
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