﻿import {ISubsector} from "../interfaces/subsector";
import {Coordinates} from "../other/coordinates";
import {StarFrontiersHex} from "./star-frontiers-hex";

export class StarFrontiersSubsector implements ISubsector {
  coordinates: Coordinates;
  hexes: StarFrontiersHex[];
}
