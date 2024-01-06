import { Roles } from "../enums/roles.enum";

export interface Profile{
  id: string;
  schoolId: string;
  avatarUrl: string;
  firstName: string;
  lastName: string;
  type?: Roles;
  className?: string;
}
