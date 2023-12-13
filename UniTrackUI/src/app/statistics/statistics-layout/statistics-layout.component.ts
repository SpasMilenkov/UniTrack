import { Component, OnInit } from '@angular/core';
import { GradeTypes } from 'src/app/shared/enums/grade-types.enum';
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
  statisticObj!: Statistic;
  pieOptions: any;
  basicOptions: any;
  barOptions: any;
  lineOptions: any;
  attendanceData: any;
  subjectAvgData: any;
  detailedSubjectData: any;
  classAvgComparisonData: any;

  recommendedMaterials: RecommendedMaterial[] = [];

  constructor(private statisticsService: StatisticsService) {}

  ngOnInit(): void {
    this.statisticObj = this.statisticsService.getCurrentStudentStatistics('');
    this.recommendedMaterials = this.statisticsService.getRecommendedMaterials();

    const {
      Attendance: attendance,
      SubjectAvg: subjectsAvg,
      DetailedSubjectPerformance: detailedSubjectPerformance,
      ClassAverageComparison: classAvgComparison,
    } = this.statisticObj;

    this.initAttendanceChart(attendance);
    this.iniSubjectAverageChart(subjectsAvg);
    this.initDetailedSubjectChart(detailedSubjectPerformance);
    this.initClassAvgComparison(classAvgComparison);
  }

  private initAttendanceChart(attendance: Attendance): void {
    this.attendanceData = {
      labels: ['Unexcused Absences', 'Excused Absences'],
      datasets: [
        {
          data: [attendance.UnExcusedAbsence, attendance.ExcusedAbsence],
          backgroundColor: ['#673389', '#72B01D'],
          hoverBackgroundColor: ['#391459', '#42870A'],
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
      switch (subject.Average) {
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
      labels: [...subjectsAvg?.map((subject) => subject.SubjectName)],
      datasets: [
        {
          label: 'Subject Average',
          data: [...averageGrades, ...averageGrades],
          backgroundColor: ['#72b01d', '#673389', '#047f94'],
          borderColor: ['#72b01d', '#673389', '#047f94'],
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
      labels: detailedSubjectData.map(({ SubjectName }) => SubjectName),
      datasets: [
        {
          label: 'Marks Count',
          backgroundColor: '#673389',
          borderColor: '#673389',
          data: detailedSubjectData.map(({ Details }) => Details.MarksCount),
        },
        {
          label: 'Highest Mark',
          backgroundColor: '#72B01D',
          borderColor: '#72B01D',
          data: detailedSubjectData.map(({ Details }) => Details.HighestMark),
        },
        {
          label: 'Lowest Mark',
          backgroundColor: '#ffc107',
          borderColor: '#ffc107',
          data: detailedSubjectData.map(({ Details }) => Details.LowestMark),
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
      labels: classAvgComparison.map(({ ClassName }) => ClassName),
      datasets: [
        {
          label: 'Class Average Comparison',
          data: classAvgComparison.map(({ Average }) => Average),
          fill: true,
          borderColor: '#673389',
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
