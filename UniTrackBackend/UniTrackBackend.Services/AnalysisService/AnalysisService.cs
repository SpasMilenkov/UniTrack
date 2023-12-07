using System.Text;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.AnalysisService;

public class AnalysisService : IAnalysisService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<AnalysisService> _logger;
    public AnalysisService(UnitOfWork unitOfWork, ILogger<AnalysisService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private Dictionary<string, string>? CalculateStudentPerformanceBySubject(Student student)
    {
        try
        {
            var performance = new Dictionary<string, string>();
            var marksGroupedBySubject = student.Marks.GroupBy(m => m.Subject.Name);

            foreach (var subjectGroup in marksGroupedBySubject)
            {
                var subjectName = subjectGroup.Key;
                var averageMark = Math.Round(subjectGroup.Average(m => m.Value), 2);
                var performanceDescription = GetPerformanceDescription(averageMark);
                performance[subjectName] = performanceDescription;
            }

            return performance;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(CalculateStudentPerformanceBySubject)}");
            return null;
        }
    }

    private string GetPerformanceDescription(decimal averageMark)
    {
        try
        {
            return averageMark switch
            {
                >= 6m => "Excellent - Outstanding performance and mastery of the subject.",
                >= 5m => "Good - Demonstrates strong understanding and ability.",
                >= 4m => "Average - Meets basic requirements but has room for growth.",
                >= 3m => "Below Average - Improvement needed to meet the standards.",
                _ => "Struggling - Needs significant improvement and support."
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(GetPerformanceDescription)}");
            return null;
        }

    }

    private string GeneratePerformanceSummary(StudentAnalysisResultViewModel viewModel)
    {
        try
        {
            var summary = new StringBuilder();

            // Overall Performance
            summary.AppendLine($"Overall Performance: {AssessOverallPerformance(viewModel.OverallAverage)}");

            // Strengths and Weaknesses in Each Subject
            foreach (var subject in viewModel.DetailedSubjectPerformance)
            {
                var subjectName = subject.Key;
                var detail = subject.Value;
                var classAverageForSubject = viewModel.ClassAverageComparison.TryGetValue(subjectName, out var value) 
                    ? value 
                    : 0; // Default to 0 if not found
                var subjectPerformance = AssessSubjectPerformance(detail, classAverageForSubject);
                summary.AppendLine($"{subjectName}: {subjectPerformance}");
            }

            // Attendance
            var attendanceImpact = AssessAttendanceImpact(viewModel.Attendance);
            summary.AppendLine($"Attendance Impact: {attendanceImpact}");

            return summary.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(GeneratePerformanceSummary)}");
            return null;
        }

    }
    private string AssessOverallPerformance(decimal overallAverage)
    {
        try
        {
            return overallAverage switch
            {
                >= 5.5m => "Excellent - Consistently high achiever in all subjects.",
                >= 4.5m => "Good - Above average performance with room for further excellence.",
                >= 3.5m => "Average - Meets basic standards but requires more effort to excel.",
                _ => "Needs Improvement - Below average performance, considerable effort required."
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(AssessOverallPerformance)}");
            return null;
        }

    }

    private string AssessSubjectPerformance(SubjectDetail detail, decimal classAvg)
    {
        try
        {
            var performance = new StringBuilder();
            var avgMark =Math.Round((detail.HighestMark + detail.LowestMark) / 2.0m, 2) ;

            if (avgMark >= classAvg + 1m)
                performance.Append("Outperforming most peers, showing excellent understanding.");
            else if (avgMark >= classAvg)
                performance.Append("Performing on par with peers, solid understanding demonstrated.");
            else
                performance.Append("Falling behind peers, needs to focus more on this subject.");

            performance.Append($" Score range observed: {detail.LowestMark}-{detail.HighestMark}.");

            return performance.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(AssessSubjectPerformance)}");
            return null;
        }

    }

    private string AssessAttendanceImpact(AttendanceRecord attendance)
    {
        try
        {
            var totalAbsences = Math.Round(attendance.TotalAbsence, 1);
            var unexcusedAbsences =Math.Round(attendance.UnExcusedAbsence, 1);

            if (unexcusedAbsences / totalAbsences > 0.5m)
                return "High number of unexcused absences, likely impacting academic performance negatively.";
        
            return totalAbsences > 10 ? "Frequent absences observed, potentially hindering learning progress." : "Good attendance record, positively contributing to learning engagement.";
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(AssessAttendanceImpact)}");
            return null;
        }

    }

    private Dictionary<string, SubjectDetail> CalculateSubjectPerformanceDetails(Student student)
    {
        try
        {
            var subjectDetails = new Dictionary<string, SubjectDetail>();

            var marksGroupedBySubject = student.Marks.GroupBy(m => m.Subject.Name);

            foreach (var subjectGroup in marksGroupedBySubject)
            {
                var marks = subjectGroup.ToList();
                var subjectDetail = new SubjectDetail
                {
                    MarksCount = marks.Count,
                    HighestMark = Math.Round(marks.Max(m => m.Value), 2),
                    LowestMark = Math.Round(marks.Min(m => m.Value), 2) 
                };

                subjectDetails[subjectGroup.Key] = subjectDetail;
            }

            return subjectDetails;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(CalculateSubjectPerformanceDetails)}");
            return null;
        }

    }

    private AttendanceRecord CalculateAttendance(Student student)
    {
        try
        {
            var totalAbsences = student.Absences.Count;
            var unexcusedAbsences = student.Absences.Count(a => !a.Excused);
            var excusedAbsences = totalAbsences - unexcusedAbsences;

            return new AttendanceRecord
            {
                TotalAbsence = totalAbsences,
                UnExcusedAbsence = unexcusedAbsences,
                ExcusedAbsence = excusedAbsences
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while trying to {nameof(CalculateAttendance)}");
            return null;
        }

    }

    public async Task<StudentAnalysisResultViewModel?> GenerateAnalysisModel(int studentId)
    {
        try
        {
            var student = await _unitOfWork.StudentRepository.GetStudentWithDetailsAsync(studentId);
            if (student is null)
                return null;

            var overallAvg = student.Marks.Select(m => m.Value).Average();
            var successPerSubject = CalculateStudentPerformanceBySubject(student)!;
            var detailedSubjectPerformance = CalculateSubjectPerformanceDetails(student);
            var attendanceRecord = CalculateAttendance(student);
            if (successPerSubject is null)
                return null;

            // Calculate class averages
            var classAverages = await _unitOfWork.MarkRepository.CalculateClassAverages(student.GradeId);

            var model = new StudentAnalysisResultViewModel
            {
                StudentName = student.User.FirstName + ' ' + student.User.LastName,
                SubjectAvg = successPerSubject,
                DetailedSubjectPerformance = detailedSubjectPerformance,
                ClassAverageComparison = classAverages,
                OverallAverage = Math.Round(overallAvg, 2),
                Attendance = attendanceRecord,
                PerformanceSummary = null!
            };

            var perfSummary = GeneratePerformanceSummary(model);
            model.PerformanceSummary = perfSummary;

            return model;
        }
        catch (Exception  e)
        {
            _logger.LogError($"Error while trying to {nameof(GenerateAnalysisModel)}");
            return null;
        }

    }
}
