import { Profile } from "./profile";
import { StudentsClass } from "./students-class";

export interface TeacherProfile extends Profile{
  classes: StudentsClass[]
  id: string;
  subjects: string[];
}
