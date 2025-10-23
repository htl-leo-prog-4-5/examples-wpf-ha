export interface ExaminationDataStream {
  id: number,
  seqNo: number,
  name: string,
  period: number,
  values: number[],
  width: number,
  height: number
  minY: number,
  maxY: number,
}

export interface Examination {
  id: number;
  examinationDate: Date
  medicalFindingsDate: Date,
  medicalFindings: string,
  svNumber: string,
  firstName: string,
  lastName: string,
  dataStreams : ExaminationDataStream[]
}