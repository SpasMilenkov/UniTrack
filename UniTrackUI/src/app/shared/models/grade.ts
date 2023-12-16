import { GradeTypes } from "../enums/grade-types.enum";

export interface Grade {
  grade: GradeTypes | string;
  subject: string;
  date: string;
  teacherId: string;
  teacherFirstName: string;
  teacherLastName: string;
  topic: string
}
