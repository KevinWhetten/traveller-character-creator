import {Coordinates} from "./coordinates";
import {WorldBuilderStar} from "./world-builder-star";
import {WorldBuilderPlanet} from "./world-builder-planet";
import {WorldBuilderStarSystem} from "./world-builder-star-system";

export class WorldBuilderHex {
  totalAvailableOrbits: number;
  name: string;
  age: number;
  coordinates: Coordinates;
  stars: WorldBuilderStar[];
  planets: WorldBuilderPlanet[];
  mainSystem: WorldBuilderStarSystem;
  secondarySystems: WorldBuilderStarSystem[];
  gasGiantQuantity: number;
  beltQuantity: number;
  terrestrialPlanetQuantity: number;
  emptyOrbits: number;
  totalWorlds: number;
  starSystems: WorldBuilderStarSystem[];
}
