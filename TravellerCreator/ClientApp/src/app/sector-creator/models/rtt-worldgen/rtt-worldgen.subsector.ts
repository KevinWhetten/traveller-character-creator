import {ISubsector} from "../interfaces/subsector";
import {Coordinates} from "../other/coordinates";
import {RttWorldgenHex} from "./rtt-worldgen.hex";

export class RttWorldgenSubsector implements ISubsector{
  coordinates: Coordinates;
  hexes: RttWorldgenHex[];
}
