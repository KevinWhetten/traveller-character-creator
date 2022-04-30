export interface Skill {
  name: string;
  description: string;
  score: number;
  subskills: Skill[];
  learnedThisTerm: boolean;
}
