export interface DetailedSubjectPerformance {
  SubjectName: string;
  Details: {
    MarksCount: number;
    HighestMark: number;
    LowestMark: number;
  };
}
