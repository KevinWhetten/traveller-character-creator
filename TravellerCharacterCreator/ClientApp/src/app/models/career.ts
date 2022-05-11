export interface Assignment {
  Name: string;
  Description: string;
  Survival: Qualification;
  Advancement: Qualification;
}

export interface Benefit {
  Cash: number;
  BenefitName: string;
}

export interface Training {
  BenefitName: string;
}

export interface TrainingTable {
  Name: string;
  Trainings: Record<number, Training>;
}

export interface Rank {
  Name: string;
  Bonus: string;
}

export interface RanksTable {
  Name: string;
  Assignments: string[];
  Ranks: Record<number, Rank>;
}

export interface Mishap {
  Description: string;
}

export interface CareerEvent {
  Description: string;
}

export interface Qualification {
  special: string;
  characteristic: string;
  target: number;
}

export interface Career {
  Designation: string;
  Name: string;
  Description: string;
  Qualification: Qualification;
  Commission: Qualification;
  Assignments: Assignment[];
  BenefitTable: Record<number, Benefit>;
  TrainingTables: TrainingTable[];
  RankTables: RanksTable[];
  Mishaps: Record<number, Mishap>;
  Events: Record<number, CareerEvent>;
}
