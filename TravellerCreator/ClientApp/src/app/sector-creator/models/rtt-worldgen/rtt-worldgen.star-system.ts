import {IStarSystem} from "../interfaces/star-system";
import {StarSystemType} from "../enums/star-system-type";
import {RttWorldgenPlanet} from "./rtt-worldgen.planet";
import {RttWorldgenStar} from "./rtt-worldgen.star";

export class RttWorldgenStarSystem implements IStarSystem{
  stars: RttWorldgenStar[];
  type: StarSystemType;

  gasGiant: boolean;
  planets: RttWorldgenPlanet[];
}
