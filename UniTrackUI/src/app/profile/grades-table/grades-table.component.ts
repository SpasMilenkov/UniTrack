import { Component, Input } from '@angular/core';
import { Grade } from 'src/app/shared/models/grade';

@Component({
  selector: 'app-grades-table',
  templateUrl: './grades-table.component.html',
  styleUrls: ['./grades-table.component.scss'],
})
export class GradesTableComponent {
  @Input() grades!: Grade[];

  displayedColumns: string[] = [
    'subject',
    'grade',
    'date',
    'teacherFirstName',
    'teacherLastName',
    'topic'
  ];
}
