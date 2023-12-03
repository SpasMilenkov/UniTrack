import { Absence } from "./absence";
import { Grade } from "./grade";
import { Profile } from "./profile";

export interface StudentProfile extends Profile{
  id: string;
  number: number;
  grades?: Grade[];
  absences?: Absence[];
  classTeacherId: string;
  classTeacherFirstName: string;
  classTeacherLastName: string;
}
