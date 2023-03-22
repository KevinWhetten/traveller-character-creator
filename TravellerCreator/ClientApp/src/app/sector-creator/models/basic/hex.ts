import {Coordinates} from "../coordinates";
import {Planet} from "./planet";

export interface Hex {
  coordinates: Coordinates;
  planet: Planet;
}
