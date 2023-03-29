import {IHex} from "../interfaces/hex";
import {Coordinates} from "../other/coordinates";
import {BasicStarSystem} from "./basic-star-system";

export class BasicHex implements IHex {
  coordinates: Coordinates;
  starSystems: BasicStarSystem[] = [];

  constructor(coordinates: Coordinates) {
    this.coordinates = coordinates;
  }
}
