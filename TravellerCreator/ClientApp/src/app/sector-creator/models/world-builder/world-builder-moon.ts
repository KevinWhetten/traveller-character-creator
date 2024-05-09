import {WorldBuilderPlanet} from "./world-builder-planet";
import {MoonOrbit} from "./Enums/moon-orbit";

export class WorldBuilderMoon extends WorldBuilderPlanet {
  parentDiameter: number;
  moonOrbit: MoonOrbit;
  planetTidalEffect: number;
  tidalStressFactor: number;

  // Orbit
  orbitDistanceInKM: number;
  orbitDistanceInDiameters: number;
}

