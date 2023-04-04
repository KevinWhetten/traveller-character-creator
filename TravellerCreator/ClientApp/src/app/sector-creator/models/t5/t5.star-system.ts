import {IStarSystem} from "../interfaces/star-system";
import {StarSystemType} from "../enums/star-system-type";
import {T5Planet} from "./t5.planet";
import {T5Star} from "./t5.star";

export class T5StarSystem implements IStarSystem{
  gasGiant: boolean;
  planet: T5Planet;
  planets: T5Planet[];
  stars: T5Star[];
  type: StarSystemType;
}
