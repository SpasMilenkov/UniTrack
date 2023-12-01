using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Mappings;

public class Mapper : IMapper
{
    private readonly ILogger<Mapper> _logger;
    public Mapper(ILogger<Mapper> logger)
    {
        _logger = logger;
    }
    public Mark? MapMark(MarkViewModel model)
    {
        try
        {
            var mark = new Mark
            {
                StudentId = model.StudentId,
                TeacherId = model.TeacherId,
                SubjectId = model.SubjectId,
                Points = model.Value,
                GradedOn =  model.GradedOn
            };
            return mark;
        }
        catch (Exception e)
        {
            
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
                Value = model.AbsenceCount,
                Time = model.Date,
                                
            };
            return absence;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to map AbsenceViewModel to Absence");
            return null;
        }
    }
}
//absences: {
//    subject: string;
//    absence: number;
//    excused: boolean;
//    date: Date;
//    teacherId; string;
//     teacherFirstName: string;
//     teacherLastName: string;
//  }[]