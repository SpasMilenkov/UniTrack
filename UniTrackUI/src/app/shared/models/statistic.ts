import { Attendance } from "./attendance";
import { ClassAverageComparison } from "./class-average-comparison";
import { DetailedSubjectPerformance } from "./detailed-subject-performance";
import { SubjectAverage } from "./subject-average";

export interface Statistic {
  StudentName: string;
  StudentId: string;
  SubjectAvg: SubjectAverage[];
  OverallAverage: number;
  PerformanceSummary: string;
  DetailedSubjectPerformance: DetailedSubjectPerformance[];
  ClassAverageComparison: ClassAverageComparison[];
  Attendance: Attendance;
}
