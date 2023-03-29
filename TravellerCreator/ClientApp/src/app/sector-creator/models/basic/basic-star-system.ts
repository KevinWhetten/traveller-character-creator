import {IStarSystem} from "../interfaces/star-system";
import {StarSystemType} from "../enums/star-system-type";
import {BasicPlanet} from "./basic-planet";
import {IStar} from "../interfaces/star";

export class BasicStarSystem implements IStarSystem {
  type: StarSystemType;
  gasGiant: boolean;
  planet: BasicPlanet;
  planets: BasicPlanet[];
  stars: IStar[];
}
