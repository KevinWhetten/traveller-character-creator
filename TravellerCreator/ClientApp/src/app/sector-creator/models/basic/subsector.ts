import {Coordinates} from "../coordinates";
import {Hex} from "./hex";


export interface Subsector {
  coordinates: Coordinates;
  hexes: Hex[];
}
