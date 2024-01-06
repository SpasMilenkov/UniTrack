import { GradeTypes } from "../enums/grade-types.enum";

export interface Grade {
  value: number,
  studentId: string,
  teacherId: string,
  subjectId: string,
  topic: string,
  gradedOn: string,
  subjectName: string;
  teacherFirstName: string;
  teacherLastName: string;
}
