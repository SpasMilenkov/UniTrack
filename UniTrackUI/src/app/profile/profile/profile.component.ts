import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ProfileTypes } from 'src/app/shared/enums/profile-types.enum';
import { StudentDetailsCardTypes } from 'src/app/shared/enums/student-details-card-types.enum';
import { TeacherDetailsCardTypes } from 'src/app/shared/enums/teacher-details-card-types.enum';
import { Event } from 'src/app/shared/models/event';
import { Profile } from 'src/app/shared/models/profile';
import { StudentProfile } from 'src/app/shared/models/student-profile';
import { EventsService } from 'src/app/shared/services/events.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  profile!: Profile;
  events$!: Observable<Event[]>;
  studentDetailsCardTypes = StudentDetailsCardTypes;
  teacherDetailsCardTypes = TeacherDetailsCardTypes;
  profileTypes = ProfileTypes;

  constructor(private userService: UserService, private eventsService: EventsService) { }

  ngOnInit(): void {
    this.profile = this.userService.getTeacherProfile();
    this.events$ = this.eventsService.getEvents();
  }

  getStudentProfile(): StudentProfile{
    return this.profile as StudentProfile;
  }
}
//TODO: Add radio button for absences
