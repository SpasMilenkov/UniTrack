import { ProfileTypes } from "../enums/profile-types.enum";

export interface UserRequest {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  type: ProfileTypes;
  approved: boolean
}
