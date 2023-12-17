import { Injectable } from '@angular/core';
import { Statistic } from '../models/statistic';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  getCurrentStudentStatistics(id: string): Statistic {
    return {
      studentName: 'John Doe',
      studentId: '12345',
      overallAverage: 4.5,
      performanceSummary:
        'John has shown great improvement in Science but needs to focus more on History.',
      subjectAvg: [
        {
          subjectName: 'Mathematics',
          average: 'Good',
        },
        {
          subjectName: 'Science',
          average: 'Average',
        },
        {
          subjectName: 'History',
          average: 'Excellent',
        },
      ],
      detailedSubjectPerformance: [
        {
          subjectName: 'Mathematics',
          details: {
            marksCount: 10,
            highestMark: 5.75,
            lowestMark: 2.52,
          },
        },
        {
          subjectName: 'Science',
          details: {
            marksCount: 8,
            highestMark: 4.8,
            lowestMark: 3.2,
          },
        },
        {
          subjectName: 'History',
          details: {
            marksCount: 12,
            highestMark: 6.0,
            lowestMark: 5.25,
          },
        },
      ],
      classAverageComparison: [
        {
          className: 'Mathematics',
          average: 4.2,
        },
        {
          className: 'Science',
          average: 3.8,
        },
        {
          className: 'History',
          average: 4.5,
        },
      ],
      attendance: {
        totalAbsence: 5.5,
        unExcusedAbsence: 2.5,
        excusedAbsence: 3,
      },
    };
  }
}
