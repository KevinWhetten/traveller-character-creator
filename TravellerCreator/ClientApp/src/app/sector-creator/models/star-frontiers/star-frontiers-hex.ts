import {IHex} from "../interfaces/hex";
import {Coordinates} from "../other/coordinates";
import {StarFrontiersStarSystem} from "./star-frontiers-star-system";

export class StarFrontiersHex implements IHex {
  coordinates: Coordinates;
  starSystems: StarFrontiersStarSystem[];
}
