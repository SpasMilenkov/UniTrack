import { TeacherDetailsCardTypes } from "../enums/teacher-details-card-types.enum";

export interface AddGradeAbsenceDialogData {
  type: TeacherDetailsCardTypes;
  studentId: string;
  studentFirstName: string;
  studentLastName: string;
}
