import { Component, Input } from '@angular/core';
import { StudentDetailsCardTypes } from 'src/app/shared/enums/student-details-card-types.enum';
import { Absence } from 'src/app/shared/models/absence';
import { Grade } from 'src/app/shared/models/grade';

@Component({
  selector: 'app-student-details-card',
  templateUrl: './student-details-card.component.html',
  styleUrls: ['./student-details-card.component.scss']
})
export class StudentDetailsCardComponent {
  @Input() type!: StudentDetailsCardTypes;
  @Input() purpleCard = false;
  @Input() grades!: Grade[];
  @Input() absences!: Absence[];
  excusedAbsences = 0;
  unexcusedAbsences = 0;
  averageGrades = 2;

  profileDetailsCardTypes = StudentDetailsCardTypes;

  constructor() { }

  ngOnInit(): void {
    this.calculateAbsences();
    this.calculateGrades();
  }

  private calculateAbsences(): void{
    if(this.absences?.length){
      this.absences?.forEach(({excused}) => {
        if(excused){
          this.excusedAbsences++;
          return;
        }
        this.unexcusedAbsences++;
      });
    }
  }

  private calculateGrades(): void{
    if(this.grades?.length){
      this.averageGrades = this.grades?.reduce((p, c) => p + c.grade, 0) / this.grades.length;
    }
  }
}
