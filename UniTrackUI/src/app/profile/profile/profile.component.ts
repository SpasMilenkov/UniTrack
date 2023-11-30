import { Component } from '@angular/core';
import { ProfileTypes } from 'src/app/shared/enums/profile-types.enum';
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
  profileTypes = ProfileTypes;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.profile = this.userService.getCurrentUserProfile();
  }

  getStudentProfile(): StudentProfile{
    return this.profile as StudentProfile;
  }
}
