import {ISector} from "../interfaces/sector";
import {MongooseSubsector} from "./mongoose-subsector";
import {SectorType} from "../enums/sector-type";

export class MongooseSector implements ISector {
  sectorType: SectorType = SectorType.Basic;
  subsectors: MongooseSubsector[] = [];
}
