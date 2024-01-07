import { Attendance } from "./attendance";
import { ClassAverageComparison } from "./class-average-comparison";
import { DetailedSubjectPerformance } from "./detailed-subject-performance";
import { SubjectAverage } from "./subject-average";

export interface Statistic {
  studentName: string;
  studentId: string;
  subjectAvg: SubjectAverage[];
  overallAverage: number;
  performanceSummary: string;
  detailedSubjectPerformance: DetailedSubjectPerformance[];
  classAverageComparison: ClassAverageComparison[];
  attendance: Attendance;
}
