import {IHex} from "../interfaces/hex";
import {Coordinates} from "../other/coordinates";
import {T5StarSystem} from "./t5.star-system";

export class T5Hex implements IHex {
  coordinates: Coordinates;
  starSystems: T5StarSystem[];
}
