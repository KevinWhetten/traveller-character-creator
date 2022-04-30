import {Characteristics} from "./characteristics";
import {Equipment} from "./equipment";
import {Finances} from "./finances";
import {Connections} from "./connections";
import {Skill} from "./skill";

export interface Character {
  log: string[];
  name: string;
  age: number;
  termNumber: number;
  Rads: number;
  Species: string;
  SpeciesTraits: string;
  Homeworld: string;
  characteristics: Characteristics;
  Skills: Skill[];
  Equipment: Equipment;
  Finances: Finances;
  Connections: Connections;
  currentUrl: string;
  nextCareer: string;
}
