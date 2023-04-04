import {ISector} from "../interfaces/sector";
import {SectorType} from "../enums/sector-type";
import {RTTWorldgenSubsector} from "./rtt-worldgen.subsector";

export class RTTWorldgenSector implements ISector {
  sectorType: SectorType;
  subsectors: RTTWorldgenSubsector[];
}
