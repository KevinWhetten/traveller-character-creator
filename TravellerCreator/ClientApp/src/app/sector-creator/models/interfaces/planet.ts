import {TradeCode} from "../enums/trade-code";
import {Base} from "../enums/base";
import {TravelCode} from "../enums/travel-code";
import {PlanetType} from "../enums/planet-type";

export interface IPlanet {
  name: string;
  size: number;
  atmosphere: number;
  temperature: number;
  hydrographics: number;
  population: number;
  government: number;
  lawLevel: number;
  starport: number;
  techLevel: number;
  tradeCodes: TradeCode[];
  bases: Base[];
  travelCode: TravelCode;
  planetType: PlanetType;
  moons: IPlanet[];
}
