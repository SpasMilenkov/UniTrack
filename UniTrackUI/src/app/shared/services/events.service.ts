import { Injectable } from '@angular/core';
import { Event } from 'src/app/shared/models/event';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EventsService {
  constructor() {}

  getEvents(): Observable<Event[]> {
    return of([
      {
        title: 'Breaking News Briefing',
        createdBy: 'News Team',
        description:
          'Stay informed with our live breaking news briefing. Our experienced news team will provide updates on current events, ensuring you are up-to-date with the latest developments around the world.',
        required: true,
        time: '2023-12-01T12:00:00',
        type: 'NEWS',
      },
      {
        title: 'Community Meetup',
        createdBy: 'Community Organizer',
        description:
          'Join us for a casual community meetup. Connect with neighbors, share experiences, and enjoy a friendly atmosphere. This is a great opportunity to build a stronger sense of community in our neighborhood.',
        required: false,
        time: '2023-12-05T17:30:00',
        type: 'SOCIAL',
      },
      {
        title: 'Coding Workshop',
        createdBy: 'Ethan Smith',
        description: 'Learn advanced coding techniques and best practices.',
        required: false,
        time: '2023-12-05T14:30:00',
        type: 'LECTURE',
      },
      {
        title: 'Friendly Soccer Match',
        createdBy: 'Sports Club',
        description:
          "Calling all sports enthusiasts! Participate in a friendly soccer match with fellow members of the sports club. Whether you're a seasoned player or a beginner, come enjoy a fun-filled afternoon of sports and camaraderie.",
        required: true,
        time: '2023-12-08T15:00:00',
        type: 'SPORT',
      },
      {
        title: 'Final Exam - Mathematics',
        createdBy: 'Department of Mathematics',
        description:
          'Prepare for the final exam in mathematics. This comprehensive assessment will cover key concepts and applications learned throughout the semester. Make sure to review your notes and be ready for a challenging yet rewarding examination.',
        required: true,
        time: '2023-12-20T10:00:00',
        type: 'EXAM',
      },
    ] as Event[]);
  }
}
