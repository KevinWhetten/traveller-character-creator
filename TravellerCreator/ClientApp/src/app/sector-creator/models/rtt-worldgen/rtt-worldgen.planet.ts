import {PlanetChemistry} from "../enums/planet-chemistry";
import {Rings} from "../enums/rings";
import {WorldType} from "../enums/world-type";
import {PlanetOrbit} from "../enums/planet-orbit";
import {CompanionOrbit} from "../enums/companion-orbit";
import {Planet} from "../basic/planet";

export class RTTWorldgenPlanet extends Planet {
  id: string;

  biosphere: number;
  chemistry: PlanetChemistry;
  rings: Rings;

  industrialBase: number;

  desirability: number;

  worldType: WorldType;
  planetOrbit: PlanetOrbit;
  orbitPosition: number;
  isMainWorld: boolean;

  parentId: string;
  starId: string;
  satelliteOrbit: CompanionOrbit;
}
