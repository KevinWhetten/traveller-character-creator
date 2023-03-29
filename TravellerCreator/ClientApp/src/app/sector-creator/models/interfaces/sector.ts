import {ISubsector} from "./subsector";
import {SectorType} from "../enums/sector-type";

export interface ISector {
  sectorType: SectorType;
  subsectors: ISubsector[];
}
