import {Subsector} from "./subsector";
import {SectorType} from "../enums/sector-type";

export class Sector {
  sectorType: SectorType;
  subsectors: Subsector[];
}
