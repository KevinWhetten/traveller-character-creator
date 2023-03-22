import {TradeCode} from "./tradeCode";

export interface Planet {
  size: number;
  atmosphere: number;
  hydrographics: number;
  population: number;
  government: number;
  lawLevel: number;
  starport: string;
  techLevel: number;
  tradeCodes: TradeCode[];
}
