import { Roles } from "../enums/roles.enum";

export interface Profile{
  id: string;
  uniId: string;
  avatarUrl: string;
  firstName: string;
  lastName: string;
  type: Roles;
  classId: string;
  className: string;
}
