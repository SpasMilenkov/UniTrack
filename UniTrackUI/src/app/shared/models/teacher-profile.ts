import { Roles } from "../enums/roles.enum";
import { Profile } from "./profile";
import { StudentsClass } from "./students-class";

export interface TeacherProfile extends Profile{
  type: Roles;
  classId: string;
  className: string;
  subjects: string[];
  classes: StudentsClass[];
}
