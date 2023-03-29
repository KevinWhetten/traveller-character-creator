import {ISubsector} from "../interfaces/subsector";
import {Coordinates} from "../other/coordinates";
import {BasicHex} from "./basic-hex";

export class BasicSubsector implements ISubsector{
  coordinates: Coordinates;
  hexes: BasicHex[];
}
