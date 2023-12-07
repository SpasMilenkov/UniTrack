import { Injectable } from '@angular/core';
import { Statistic } from '../models/statistic';

@Injectable({
  providedIn: 'root',
})
export class EventsService {
  getCurrentStudentStatistics(id: string): Statistic {
    return {
      StudentName: 'John Doe',
      StudentId: '12345',
      OverallAverage: 4.5,
      PerformanceSummary:
        'John has shown great improvement in Science but needs to focus more on History.',
      SubjectAvg: [
        {
          subject: 'Mathematics',
          grade: 'Good',
        },
        {
          subject: 'Science',
          grade: 'Average',
        },
        {
          subject: 'History',
          grade: 'Excellent',
        },
      ],
      DetailedSubjectPerformance: [
        {
          subject: 'Mathematics',
          MarksCount: 10,
          HighestMark: 5.75,
          LowestMark: 2.52,
        },
        {
          subject: 'Science',
          MarksCount: 8,
          HighestMark: 4.8,
          LowestMark: 3.2,
        },
        {
          subject: 'History',
          MarksCount: 12,
          HighestMark: 6.0,
          LowestMark: 5.25,
        },
      ],
      ClassAverageComparison: [
        {
          subject: 'Mathematics',
          comparison: 4.2,
        },
        {
          subject: 'Science',
          comparison: 3.8,
        },
        {
          subject: 'History',
          comparison: 4.5,
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
