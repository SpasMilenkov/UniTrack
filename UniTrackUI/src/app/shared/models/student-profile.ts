import { Roles } from "../enums/roles.enum";
import { Absence } from "./absence";
import { Grade } from "./grade";
import { Profile } from "./profile";

export interface StudentProfile extends Profile{
  type: Roles;
  classId: string;
  className: string;
  number: number;
  marks?: Grade[];
  absences?: Absence[];
  classTeacherId: string;
  classTeacherFirstName: string;
  classTeacherLastName: string;
}
