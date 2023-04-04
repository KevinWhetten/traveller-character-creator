import {ISubsector} from "../interfaces/subsector";
import {Coordinates} from "../other/coordinates";
import {MongooseHex} from "./mongoose-hex";

export class MongooseSubsector implements ISubsector{
  coordinates: Coordinates;
  hexes: MongooseHex[];
}
