import { ProfileTypes } from "../enums/profile-types.enum";

export interface Profile{
  id: string;
  uniId: string;
  avatarUrl: string;
  firstName: string;
  lastName: string;
  type: ProfileTypes;
  classId: string;
  className: string;
}
