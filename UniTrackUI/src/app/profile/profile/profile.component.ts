import { Component } from '@angular/core';
import { ProfileTypes } from 'src/app/shared/enums/profile-types.enum';
import { StudentDetailsCardTypes } from 'src/app/shared/enums/student-details-card-types.enum';
import { TeacherDetailsCardTypes } from 'src/app/shared/enums/teacher-details-card-types.enum';
import { Profile } from 'src/app/shared/models/profile';
import { StudentProfile } from 'src/app/shared/models/student-profile';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  profile!: Profile;
  studentDetailsCardTypes = StudentDetailsCardTypes;
  teacherDetailsCardTypes = TeacherDetailsCardTypes;
  profileTypes = ProfileTypes;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.profile = this.userService.getTeacherProfile();
  }

  getStudentProfile(): StudentProfile{
    return this.profile as StudentProfile;
  }
}
