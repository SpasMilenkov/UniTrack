import { GradeTypes } from "../enums/grade-types.enum";

export interface Grade {
  grade: GradeTypes;
  subject: string;
}
