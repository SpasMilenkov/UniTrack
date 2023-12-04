import { Profile } from "./profile";
import { StudentsClass } from "./students-class";

export interface TeacherProfile extends Profile{
  id: string;
  subjects: string[];
  classes: StudentsClass[];
}
