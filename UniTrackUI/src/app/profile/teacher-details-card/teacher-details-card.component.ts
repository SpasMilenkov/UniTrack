import { Component, Input } from '@angular/core';
import { TeacherDetailsCardTypes } from 'src/app/shared/enums/teacher-details-card-types.enum';

@Component({
  selector: 'app-teacher-details-card',
  templateUrl: './teacher-details-card.component.html',
  styleUrls: ['./teacher-details-card.component.scss']
})
export class TeacherDetailsCardComponent {
  @Input() type!: TeacherDetailsCardTypes;
  @Input() purpleCard = false;

  teacherDetailsCardTypes = TeacherDetailsCardTypes;
}
