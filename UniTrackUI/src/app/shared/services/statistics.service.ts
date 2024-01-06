import { Injectable } from '@angular/core';
import { Statistic } from '../models/statistic';
import { RecommendedMaterial } from '../models/recommended-material';

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

  getRecommendedMaterials(): RecommendedMaterial[] {
    return [
      {
        title:
          'Math learning for beginners: How to solve system of equations tutorial - Algebra tutorial videos',
        link: 'https://www.youtube.com/watch?v=Fnc85GE6vds',
        thumbnail: 'https://i.ytimg.com/vi/Fnc85GE6vds/mqdefault.jpg',
        publishedDate: '2023-12-05T10:21:34+00:00',
      },
      {
        title: 'Solve Systems of Equations by Substitution',
        link: 'https://www.youtube.com/watch?v=Iw7xKmhYHg8',
        thumbnail: 'https://i.ytimg.com/vi/Iw7xKmhYHg8/mqdefault.jpg',
        publishedDate: '2023-11-21T04:09:59+00:00',
      },
      {
        title:
          'Linear Algebra: Introduction to Systems of Linear Equations (Section 1.1)',
        link: 'https://www.youtube.com/watch?v=-Ls8rLJ_Kzg',
        thumbnail: 'https://i.ytimg.com/vi/-Ls8rLJ_Kzg/mqdefault.jpg',
        publishedDate: '2022-07-19T13:12:20+00:00',
      },
      {
        title: 'Solve a System of Linear Equations by Graphing (One Solution)',
        link: 'https://www.youtube.com/watch?v=PIzct8L5lHo',
        thumbnail: 'https://i.ytimg.com/vi/PIzct8L5lHo/mqdefault.jpg',
        publishedDate: '2021-09-14T22:07:25+00:00',
      },
      {
        title: 'Precalculus: Systems of Linear Equations (Section 11.1)',
        link: 'https://www.youtube.com/watch?v=wrT-W6N6d5A',
        thumbnail: 'https://i.ytimg.com/vi/wrT-W6N6d5A/mqdefault.jpg',
        publishedDate: '2020-05-06T00:01:49+00:00',
      },
    ];
  }
}