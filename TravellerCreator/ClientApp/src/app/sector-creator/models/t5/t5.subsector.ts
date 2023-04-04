import {ISubsector} from "../interfaces/subsector";
import {Coordinates} from "../other/coordinates";
import {T5Hex} from "./t5.hex";

export class T5Subsector implements ISubsector{
  coordinates: Coordinates;
  hexes: T5Hex[];
}
