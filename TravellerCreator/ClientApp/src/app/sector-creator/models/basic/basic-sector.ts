import {ISector} from "../interfaces/sector";
import {BasicSubsector} from "./basic-subsector";
import {SectorType} from "../enums/sector-type";

export class BasicSector implements ISector {
  sectorType: SectorType = SectorType.Basic;
  subsectors: BasicSubsector[] = [];
}
