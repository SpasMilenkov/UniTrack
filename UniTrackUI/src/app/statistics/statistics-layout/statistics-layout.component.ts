import { Component, OnInit } from '@angular/core';
import { Attendance } from 'src/app/shared/models/attendance';
import { Statistic } from 'src/app/shared/models/statistic';
import { StatisticsService } from 'src/app/shared/services/statistics.service';

@Component({
  selector: 'app-statistics-layout',
  templateUrl: './statistics-layout.component.html',
  styleUrls: ['./statistics-layout.component.scss'],
})
export class StatisticsLayoutComponent implements OnInit {
  statisticObj!: Statistic;
  options: any;
  attendanceData: any;

  constructor(private statisticsService: StatisticsService) {}

  ngOnInit(): void {
    this.statisticObj = this.statisticsService.getCurrentStudentStatistics('');

    const { Attendance: attendance } = this.statisticObj;

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

    this.options = {
      plugins: {
        legend: {
          labels: {
            usePointStyle: true,
            color: '#454955',
          },
        },
      },
    };
  }
}
