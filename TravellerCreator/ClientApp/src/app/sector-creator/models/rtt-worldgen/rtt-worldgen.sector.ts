import {ISector} from "../interfaces/sector";
import {SectorType} from "../enums/sector-type";
import {RttWorldgenSubsector} from "./rtt-worldgen.subsector";

export class RttWorldgenSector implements ISector {
  sectorType: SectorType;
  subsectors: RttWorldgenSubsector[];
}
