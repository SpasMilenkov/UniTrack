import { Component, Input } from '@angular/core';
import { EventTypes } from '../../enums/event-types.enum';
import { Event } from '../../models/event';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.scss']
})
export class EventCardComponent {
  @Input() event!: Event;
  eventTypes = EventTypes;
}
