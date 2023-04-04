import {IHex} from "../interfaces/hex";
import {Coordinates} from "../other/coordinates";
import {MongooseStarSystem} from "./mongoose-star-system";

export class MongooseHex implements IHex {
  coordinates: Coordinates;
  starSystems: MongooseStarSystem[] = [];

  constructor(coordinates: Coordinates) {
    this.coordinates = coordinates;
  }
}
