import { GradeTypes } from "../enums/grade-types.enum";

export interface SubjectAverage {
  subjectName: string;
  average: GradeTypes | string;
}
