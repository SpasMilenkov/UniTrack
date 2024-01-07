import { StudentProfile } from "./student-profile";
import { TeacherProfile } from "./teacher-profile";

export interface StudentsClass{
  classId: number;
  className: string;
  students: StudentProfile[];
  classTeacher?: TeacherProfile;
}
