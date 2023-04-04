import {IStarSystem} from "../interfaces/star-system";
import {StarSystemType} from "../enums/star-system-type";
import {MongoosePlanet} from "./mongoose-planet";
import {IStar} from "../interfaces/star";

export class MongooseStarSystem implements IStarSystem {
  type: StarSystemType;
  gasGiant: boolean;
  planets: MongoosePlanet[];
  stars: IStar[];
}
