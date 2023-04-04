﻿import {IPlanet} from "../interfaces/planet";
import {PlanetType} from "../enums/planet-type";
import {TradeCode} from "../enums/trade-code";
import {TravelCode} from "../enums/travel-code";
import {Base} from "../enums/base";
import {PlanetChemistry} from "../enums/planet-chemistry";
import {Rings} from "../enums/rings";
import {WorldType} from "../enums/world-type";
import {PlanetOrbit} from "../enums/planet-orbit";

export class RTTWorldgenPlanet implements IPlanet {
  id: string;
  name: string;

  size: number;
  atmosphere: number;
  temperature: number;
  hydrographics: number;
  biosphere: number;
  chemistry: PlanetChemistry;
  rings: Rings;

  population: number;
  government: number;
  lawLevel: number;
  starport: string;
  techLevel: number;

  tradeCodes: TradeCode[];
  bases: Base[];
  travelCode: TravelCode;

  planetType: PlanetType;
  worldType: WorldType;
  planetOrbit: PlanetOrbit;
  orbitPosition: number;

  parentId: string;
  starId: string;

  satellites: RTTWorldgenPlanet[];
}
