import {IHex} from "../interfaces/hex";
import {Coordinates} from "../other/coordinates";
import {RTTWorldgenStarSystem} from "./rtt-worldgen.star-system";

export class RTTWorldgenHex implements IHex {
  coordinates: Coordinates;
  starSystems: RTTWorldgenStarSystem[];
}
