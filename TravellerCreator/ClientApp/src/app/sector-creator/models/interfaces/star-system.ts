import {StarSystemType} from "../enums/star-system-type";
import {IStar} from "./star";
import {IPlanet} from "./planet";

export interface IStarSystem {
  stars: IStar[];
  type: StarSystemType;
  gasGiant: boolean;
  planets: IPlanet[];
}
