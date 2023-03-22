import {System} from "./system/system";
import {Coordinates} from "./coordinates";

export interface Hex {
  systems: System[];
  coordinates: Coordinates;
}
