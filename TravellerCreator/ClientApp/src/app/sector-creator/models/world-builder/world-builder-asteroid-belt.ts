import {WorldBuilderPlanet} from "./world-builder-planet";
import {AsteroidBeltComposition} from "./asteroid-belt-composition";

export class WorldBuilderAsteroidBelt extends WorldBuilderPlanet {
  // Bodies
  beltSpan: number;
  bulk: number;
  composition: AsteroidBeltComposition;
}
