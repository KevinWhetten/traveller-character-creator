import {Coordinates} from "../other/coordinates";
import {IStarSystem} from "./star-system";

export interface IHex {
  coordinates: Coordinates;
  starSystems: IStarSystem[];
}

