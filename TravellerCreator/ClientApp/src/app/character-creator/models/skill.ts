export interface Skill {
  Name: string;
  Description: string;
}

export interface BaseSkill extends Skill {
  Subskills: string[];
}

export interface Subskill extends Skill {
  ParentSkill: string;
}
