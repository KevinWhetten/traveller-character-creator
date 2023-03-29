import {IStarSystem} from "../interfaces/star-system";
import {StarSystemType} from "../enums/star-system-type";
import {StarFrontiersStar} from "./star-frontiers-star";
import {StarFrontiersPlanet} from "./star-frontiers-planet";

export class StarFrontiersStarSystem implements IStarSystem {
  stars: StarFrontiersStar[];
  type: StarSystemType;
  gasGiant: boolean;
  planet: StarFrontiersPlanet;
  planets: StarFrontiersPlanet[];
}
