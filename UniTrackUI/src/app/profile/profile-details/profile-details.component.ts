import { Component, Input } from '@angular/core';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { Profile } from 'src/app/shared/models/profile';
import { StudentProfile } from 'src/app/shared/models/student-profile';
import { TeacherProfile } from 'src/app/shared/models/teacher-profile';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.scss'],
})
export class ProfileDetailsComponent {
  @Input() profile!: Profile | StudentProfile;
  roles = Roles;

  constructor() {}

  ngOnInit(): void {}

  getStudentProfile(): StudentProfile {
    return this.profile as StudentProfile;
  }

  getTeacherProfile(): TeacherProfile {
    return this.profile as TeacherProfile;
  }
}
