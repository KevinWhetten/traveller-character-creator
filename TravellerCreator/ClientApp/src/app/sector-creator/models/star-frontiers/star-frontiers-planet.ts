import {IPlanet} from "../interfaces/planet";
import {Base} from "../enums/base";
import {TradeCode} from "../enums/trade-code";
import {TravelCode} from "../enums/travel-code";
import {PlanetType} from "../enums/planet-type";

export class StarFrontiersPlanet implements IPlanet{
  atmosphere: number;
  bases: Base[];
  government: number;
  hydrographics: number;
  lawLevel: number;
  name: string;
  population: number;
  size: number;
  starport: number;
  techLevel: number;
  temperature: number;
  tradeCodes: TradeCode[];
  travelCode: TravelCode;
  planetType: PlanetType;
  moons: StarFrontiersPlanet[];
}
