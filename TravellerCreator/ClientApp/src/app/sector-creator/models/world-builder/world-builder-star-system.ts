import {WorldBuilderStar} from "./world-builder-star";
import {WorldBuilderPlanet} from "./world-builder-planet";

export class WorldBuilderStarSystem {
  star: WorldBuilderStar;
  planets: WorldBuilderPlanet[];
  worldNum: number;
  emptyOrbits: number;
  baselineNumber: number;
  baselineOrbit: number;
  spread: number;
  age: number;
  mass: number;
  luminosity: number;
  orbitNumber: number;
  component: string;
  orbitDistance: number;
  eccentricity: number;
  mao: number;
  hzco: number;
  name: string;
}
