import {IPlanet} from "../interfaces/planet";
import {Base} from "../enums/base";
import {PlanetType} from "../enums/planet-type";
import {TradeCode} from "../enums/trade-code";
import {TravelCode} from "../enums/travel-code";

export class T5Planet implements IPlanet {
  atmosphere: number;
  bases: Base[];
  government: number;
  hydrographics: number;
  lawLevel: number;
  satellites: IPlanet[];
  name: string;
  planetType: PlanetType;
  population: number;
  size: number;
  starport: string;
  techLevel: number;
  temperature: number;
  tradeCodes: TradeCode[];
  travelCode: TravelCode;
}
