import { Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Attendance } from 'src/app/shared/models/attendance';
import { ClassAverageComparison } from 'src/app/shared/models/class-average-comparison';
import { DetailedSubjectPerformance } from 'src/app/shared/models/detailed-subject-performance';
import { RecommendedMaterial } from 'src/app/shared/models/recommended-material';
import { Statistic } from 'src/app/shared/models/statistic';
import { SubjectAverage } from 'src/app/shared/models/subject-average';
import { StatisticsService } from 'src/app/shared/services/statistics.service';

@Component({
  selector: 'app-statistics-layout',
  templateUrl: './statistics-layout.component.html',
  styleUrls: ['./statistics-layout.component.scss'],
})
export class StatisticsLayoutComponent implements OnInit {
  statisticObj$!: Observable<Statistic>;
  pieOptions: any;
  basicOptions: any;
  barOptions: any;
  lineOptions: any;
  attendanceData: any;
  subjectAvgData: any;
  detailedSubjectData: any;
  classAvgComparisonData: any;

  recommendedMaterials$!: Observable<RecommendedMaterial[]>;

  constructor(private statisticsService: StatisticsService) {}

  ngOnInit(): void {
    this.statisticObj$ = this.statisticsService
      .getCurrentStudentStatistics()
      .pipe(
        tap(
          ({
            attendance,
            subjectAvg,
            detailedSubjectPerformance,
            classAverageComparison,
          }) => {
            this.initAttendanceChart(attendance);
            this.iniSubjectAverageChart(subjectAvg);
            this.initDetailedSubjectChart(detailedSubjectPerformance);
            this.initClassAvgComparison(classAverageComparison);
          }
        )
      );

      this.recommendedMaterials$ = this.statisticsService.getCurrentStudentRecommendationMaterial();
  }

  private initAttendanceChart(attendance: Attendance): void {
    this.attendanceData = {
      labels: ['Unexcused Absences', 'Excused Absences'],
      datasets: [
        {
          data: [attendance.unExcusedAbsence, attendance.excusedAbsence],
          backgroundColor: ['#87BC3F', '#7E529B'],
          hoverBackgroundColor: ['#6AA91A', '#5F2E81'],
        },
      ],
    };

    this.pieOptions = {
      plugins: {
        legend: {
          labels: {
            usePointStyle: true,
            color: '#000',
          },
        },
      },
    };
  }

  private iniSubjectAverageChart(subjectsAvg: SubjectAverage[]): void {
    const averageGrades = subjectsAvg?.map((subject) => {
      let grade = 2;
      switch (subject.average) {
        case 'Excellent':
          grade = 6;
          break;
        case 'Good':
          grade = 5;
          break;
        case 'Average':
          grade = 4;
          break;
        case 'Below Average':
          grade = 3;
          break;
      }
      return grade;
    });

    this.subjectAvgData = {
      labels: [...subjectsAvg?.map((subject) => subject.subjectName)],
      datasets: [
        {
          label: 'Subject Average',
          data: [...averageGrades, ...averageGrades],
          backgroundColor: ['#87BC3F', '#7E529B', '#5c6bc0'],
          borderColor: ['#87BC3F', '#7E529B', '#5c6bc0'],
          borderWidth: 1,
        },
      ],
    };

    this.basicOptions = {
      plugins: {
        legend: {
          labels: {
            color: '#000',
          },
        },
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            color: '#000000',
          },
          grid: {
            color: '#ffffff',
            drawBorder: false,
          },
        },
        x: {
          ticks: {
            color: '#000000',
          },
          grid: {
            color: '#ffffff',
            drawBorder: false,
          },
        },
      },
    };
  }

  private initDetailedSubjectChart(
    detailedSubjectData: DetailedSubjectPerformance[]
  ): void {
    this.detailedSubjectData = {
      labels: detailedSubjectData.map(({ subjectName }) => subjectName),
      datasets: [
        {
          label: 'Marks Count',
          backgroundColor: '#7E529B',
          borderColor: '#7E529B',
          data: detailedSubjectData.map(({ details }) => details.marksCount),
        },
        {
          label: 'Highest Mark',
          backgroundColor: '#87BC3F',
          borderColor: '#87BC3F',
          data: detailedSubjectData.map(({ details }) => details.highestMark),
        },
        {
          label: 'Lowest Mark',
          backgroundColor: '#ffca28',
          borderColor: '#ffca28',
          data: detailedSubjectData.map(({ details }) => details.lowestMark),
        },
      ],
    };

    this.barOptions = {
      maintainAspectRatio: false,
      aspectRatio: 0.8,
      plugins: {
        legend: {
          labels: {
            color: '#000000',
          },
        },
      },
      scales: {
        x: {
          ticks: {
            color: '#000',
            font: {
              weight: 500,
            },
          },
          grid: {
            color: '#fff',
            drawBorder: false,
          },
        },
        y: {
          ticks: {
            color: '#000',
          },
          grid: {
            color: '#fff',
            drawBorder: false,
          },
        },
      },
    };
  }

  private initClassAvgComparison(
    classAvgComparison: ClassAverageComparison[]
  ): void {
    this.classAvgComparisonData = {
      labels: classAvgComparison.map(({ className }) => className),
      datasets: [
        {
          label: 'Class Average Comparison',
          data: classAvgComparison.map(({ average }) => average),
          fill: true,
          borderColor: '#7E529B',
          tension: 0.5,
        },
      ],
    };

    this.lineOptions = {
      maintainAspectRatio: false,
      aspectRatio: 0.8,
      plugins: {
        legend: {
          labels: {
            color: '#000',
          },
        },
      },
      scales: {
        x: {
          ticks: {
            color: '#000',
          },
          grid: {
            color: '#fff',
            drawBorder: false,
          },
        },
        y: {
          ticks: {
            color: '#000',
          },
          grid: {
            color: '#fff',
            drawBorder: false,
          },
        },
      },
    };
  }
}
