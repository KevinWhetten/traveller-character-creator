import {ISubsector} from "../interfaces/subsector";
import {Coordinates} from "../other/coordinates";
import {StarFrontiersHex} from "./star-frontiers-hex";

export class StarFrontierSubsector implements ISubsector {
  coordinates: Coordinates;
  hexes: StarFrontiersHex[];
}
