export interface DetailedSubjectPerformance {
  subjectName: string;
  details: {
    marksCount: number;
    highestMark: number;
    lowestMark: number;
  };
}
