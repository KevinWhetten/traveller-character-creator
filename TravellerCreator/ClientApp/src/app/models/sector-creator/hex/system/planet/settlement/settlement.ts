import {SettlementType} from "./settlement-type";
import {Culture} from "./culture";
import {Economics} from "./economics";
import {Base} from "./base";
import {Planet} from "../planet";

export interface Settlement {
  planet: Planet;

  allegiance: string;
  isCapitol: boolean;

  settlementType: SettlementType;

  population: number;
  government: number;
  lawLevel: number;
  techLevel: number;
  starport: string;
  industry: number;

  culture: Culture;
  economics: Economics;
  bases: Base[];
}
