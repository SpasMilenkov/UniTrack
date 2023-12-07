export interface Statistic {
  StudentName: string;
  StudentId: string;
  SubjectAvg: {
    subject: string;
    grade: string;
  }[];
  OverallAverage: number;
  PerformanceSummary: string;
  DetailedSubjectPerformance: {
    subject: string;
    MarksCount: number;
    HighestMark: number;
    LowestMark: number;
  }[];
  ClassAverageComparison: {
    subject: string;
    comparison: number;
  }[];
  Attendance: {
    TotalAbsence: number;
    UnExcusedAbsence: number;
    ExcusedAbsence: number;
  };
}
