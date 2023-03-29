import {Coordinates} from "../other/coordinates";
import {IHex} from "./hex";

export interface ISubsector {
  coordinates: Coordinates;
  hexes: IHex[];
}
