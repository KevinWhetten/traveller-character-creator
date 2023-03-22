import {Characteristics} from "./characteristics";
import {Weapon} from "./weapon";
import {Augment} from "./augment";
import {Equipment} from "./equipment";
import {Connections} from "./connections";
import {Finances} from "./finances";
import {Armor} from "./armor";

export class Character {
  Name: string = '';
  Age: number = 14;
  Rads: number = 0;
  Species: string = 'Human';
  SpeciesTraits: string = '';
  Homeworld: string = '';
  Characteristics: Characteristics = new Characteristics();
  Skills: Record<string, number> = {} as Record<string, number>;
  Weapons: Weapon[] = [];
  Augments: Augment[] = [];
  Equipment: Equipment[] = [];
  CarriedMass: number = 0;
  Armor: Armor[] = [];
  Finances: Finances = new Finances();
  Connections: Connections = new Connections();
}
