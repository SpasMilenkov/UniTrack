import { Roles } from "../enums/roles.enum";

export interface UserRequest {
  id: string;
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  type: Roles;
  approved: boolean
}
