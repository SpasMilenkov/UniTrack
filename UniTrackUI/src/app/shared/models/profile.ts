import { ProfileTypes } from "../enums/profile-types.enum";

export interface Profile{
  id: string;
  avatarUrl: string;
  firstName: string;
  lastName: string;
  type: ProfileTypes;
  classNumber: string;
  className: string;
}
