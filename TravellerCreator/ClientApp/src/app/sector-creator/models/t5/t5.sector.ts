import {ISector} from "../interfaces/sector";
import {SectorType} from "../enums/sector-type";
import {T5Subsector} from "./t5.subsector";

export class T5Sector implements ISector{
  sectorType: SectorType;
  subsectors: T5Subsector[];
}
