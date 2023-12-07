namespace UniTrackBackend.Api.ViewModels.ResultViewModels;

public class StudentAnalysisResultViewModel
{
    public required string StudentName { get; set; }
    public int StudentId { get; set; }
    public required Dictionary<string, string> SubjectAvg { get; set; }
    public required decimal OverallAverage { get; set; }
    public required string PerformanceSummary { get; set; }
    public required Dictionary<string, SubjectDetail> DetailedSubjectPerformance { get; set; } = null!;
    public required Dictionary<string, decimal> ClassAverageComparison { get; set; } = null!;
    public required AttendanceRecord Attendance { get; set; } = null!;
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
    public decimal TotalAbsence{ get; set; }
    public decimal ExcusedAbsence { get; set; }
    public decimal UnExcusedAbsence { get; set; }
    // Additional details as needed
}       