import {Chemistry} from "./chemistry";
import {PBG} from "./pbg";
import {Star} from "../star/star";
import {PlanetType} from "./planet-type";
import {PlanetOrbit} from "./planet-orbit";
import {RingType} from "./ring-type";
import {Desirability} from "./desirability";
import {Settlement} from "./settlement/settlement";
import {TradeCode} from "./trade-code";

export interface Planet {
  // Hex Info
  star: Star;
  parent: Planet;
  position: number;

  // Planet Info
  name: string;
  size: number;
  atmosphere: number;
  hydrosphere: number;
  biosphere: number;
  temperature: number;
  pbg: PBG;
  dayLength: number;
  orbitalPeriod: number;
  terraformed: boolean;

  type: PlanetType;
  rings: RingType;
  orbit: PlanetOrbit;
  chemistry: Chemistry;

  // Satellites
  satellites: Planet[];

  // Settlement
  importance: number;
  desirability: Desirability;

  settlement: Settlement;
  tradeCodes: TradeCode[];
}
