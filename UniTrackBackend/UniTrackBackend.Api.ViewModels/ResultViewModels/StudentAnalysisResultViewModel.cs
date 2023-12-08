
namespace UniTrackBackend.Api.ViewModels.ResultViewModels
{
    public class StudentAnalysisResultViewModel
    {
        public required string StudentName { get; set; }
        public int StudentId { get; set; }
        public required List<SubjectAverage> SubjectAvg { get; set; }
        public required decimal OverallAverage { get; set; }
        public required string PerformanceSummary { get; set; }
        public required List<DetailedSubjectPerformance> DetailedSubjectPerformance { get; set; }
        public required List<ClassAverage> ClassAverageComparison { get; set; }
        public required AttendanceRecord Attendance { get; set; }
    }

    public class SubjectAverage
    {
        public string SubjectName { get; set; }
        public string? Average { get; set; }
    }

    public class DetailedSubjectPerformance
    {
        public string SubjectName { get; set; }
        public SubjectDetail Details { get; set; }
    }

    public class ClassAverage
    {
        public string ClassName { get; set; }
        public decimal Average { get; set; }
    }

    public class SubjectDetail
    {
        public int MarksCount { get; set; }
        public decimal HighestMark { get; set; }
        public decimal LowestMark { get; set; }
        // Additional details as needed
    }

    public class AttendanceRecord
    {
        public decimal TotalAbsence { get; set; }
        public decimal ExcusedAbsence { get; set; }
        public decimal UnExcusedAbsence { get; set; }
        // Additional details as needed
    }
}
