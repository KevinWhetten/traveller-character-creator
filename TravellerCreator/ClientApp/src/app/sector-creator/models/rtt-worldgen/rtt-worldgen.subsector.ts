import {ISubsector} from "../interfaces/subsector";
import {Coordinates} from "../other/coordinates";
import {RTTWorldgenHex} from "./rtt-worldgen.hex";

export class RTTWorldgenSubsector implements ISubsector{
  coordinates: Coordinates;
  hexes: RTTWorldgenHex[];
}
