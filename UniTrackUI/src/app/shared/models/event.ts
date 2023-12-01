import { EventTypes } from "../enums/event-types.enum";

export interface Event {
  type: EventTypes;
  title: string;
  createdBy: string;
  description: string;
  required: boolean;
  time: string;
}
