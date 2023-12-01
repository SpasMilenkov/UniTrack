import { Absence } from "./absence";
import { Grade } from "./grade";
import { Profile } from "./profile";

export interface StudentProfile extends Profile{
  number?: number;
  grades?: Grade[];
  absences?: Absence[]
}
