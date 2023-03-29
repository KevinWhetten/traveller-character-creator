import {ISector} from "../interfaces/sector";
import {StarFrontierSubsector} from "./star-frontiers-subsector";
import {SectorType} from "../enums/sector-type";

export class StarFrontiersSector implements ISector {
  sectorType: SectorType = SectorType.StarFrontiers;
  subsectors: StarFrontierSubsector[];
}
