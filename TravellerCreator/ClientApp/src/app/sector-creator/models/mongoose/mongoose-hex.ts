import {Coordinates} from "../other/coordinates";
import {Hex} from "../basic/hex";

export class MongooseHex extends Hex {
  constructor(coordinates: Coordinates) {
    super();
    this.coordinates = coordinates;
  }
}
