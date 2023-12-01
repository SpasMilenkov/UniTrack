import { Component, Input } from '@angular/core';
import { ProfileTypes } from 'src/app/shared/enums/profile-types.enum';
import { Profile } from 'src/app/shared/models/profile';
import { StudentProfile } from 'src/app/shared/models/student-profile';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.scss'],
})
export class ProfileDetailsComponent {
  @Input() profile!: Profile | StudentProfile;
  profileTypes = ProfileTypes;

  constructor() {}

  ngOnInit(): void {}

  getStudentProfile(): StudentProfile {
    return this.profile as StudentProfile;
  }
}
