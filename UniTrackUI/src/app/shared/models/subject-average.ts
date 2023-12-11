import { GradeTypes } from "../enums/grade-types.enum";

export interface SubjectAverage {
  SubjectName: string;
  Average: GradeTypes | string;
}
