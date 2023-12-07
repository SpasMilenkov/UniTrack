import { Injectable } from '@angular/core';
import { Statistic } from '../models/statistic';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  getCurrentStudentStatistics(id: string): Statistic {
    return {
      StudentName: 'John Doe',
      StudentId: '12345',
      OverallAverage: 4.5,
      PerformanceSummary:
        'John has shown great improvement in Science but needs to focus more on History.',
      SubjectAvg: [
        {
          SubjectName: 'Mathematics',
          Average: 'Good',
        },
        {
          SubjectName: 'Science',
          Average: 'Average',
        },
        {
          SubjectName: 'History',
          Average: 'Excellent',
        },
      ],
      DetailedSubjectPerformance: [
        {
          SubjectName: 'Mathematics',
          Details: {
            MarksCount: 10,
            HighestMark: 5.75,
            LowestMark: 2.52,
          },
        },
        {
          SubjectName: 'Science',
          Details: {
            MarksCount: 8,
            HighestMark: 4.8,
            LowestMark: 3.2,
          },
        },
        {
          SubjectName: 'History',
          Details: {
            MarksCount: 12,
            HighestMark: 6.0,
            LowestMark: 5.25,
          },
        },
      ],
      ClassAverageComparison: [
        {
          ClassName: 'Mathematics',
          Average: 4.2,
        },
        {
          ClassName: 'Science',
          Average: 3.8,
        },
        {
          ClassName: 'History',
          Average: 4.5,
        },
      ],
      Attendance: {
        TotalAbsence: 5.5,
        UnExcusedAbsence: 2.5,
        ExcusedAbsence: 3,
      },
    };
  }
}
