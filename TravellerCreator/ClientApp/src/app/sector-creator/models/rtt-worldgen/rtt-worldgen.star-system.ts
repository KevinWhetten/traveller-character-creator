import {IStarSystem} from "../interfaces/star-system";
import {StarSystemType} from "../enums/star-system-type";
import {RTTWorldgenPlanet} from "./rtt-worldgen.planet";
import {RTTWorldgenStar} from "./rtt-worldgen.star";

export class RTTWorldgenStarSystem implements IStarSystem{
  stars: RTTWorldgenStar[];
  type: StarSystemType;

  gasGiant: boolean;
  planets: RTTWorldgenPlanet[];
}
