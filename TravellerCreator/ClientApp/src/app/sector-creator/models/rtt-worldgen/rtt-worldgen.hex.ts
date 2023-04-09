import {IHex} from "../interfaces/hex";
import {Coordinates} from "../other/coordinates";
import {RttWorldgenStarSystem} from "./rtt-worldgen.star-system";

export class RttWorldgenHex implements IHex {
  coordinates: Coordinates;
  starSystems: RttWorldgenStarSystem[];
}
