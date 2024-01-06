import { Component, Input } from '@angular/core';
import { Absence } from 'src/app/shared/models/absence';

@Component({
  selector: 'app-absences-table',
  templateUrl: './absences-table.component.html',
  styleUrls: ['./absences-table.component.scss']
})
export class AbsencesTableComponent {
  @Input() absences!: Absence[];

  displayedColumns: string[] = [
    'subject',
    'absenceCount',
    'excused',
    'date',
    'teacherFirstName',
    'teacherLastName',
  ];
}
