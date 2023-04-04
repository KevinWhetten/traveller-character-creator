import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MongooseSector} from "./models/mongoose/mongoose-sector";
import {SectorType} from "./models/enums/sector-type";
import {StarFrontiersSector} from "./models/star-frontiers/star-frontiers-sector";
import {MongooseSubsector} from "./models/mongoose/mongoose-subsector";
import {StarFrontiersSubsector} from "./models/star-frontiers/star-frontiers-subsector";
import {RTTWorldgenSector} from "./models/rtt-worldgen/rtt-worldgen.sector";
import {RTTWorldgenSubsector} from "./models/rtt-worldgen/rtt-worldgen.subsector";
import {T5Sector} from "./models/t5/t5.sector";
import {T5Subsector} from "./models/t5/t5.subsector";

@Component({
  selector: 'app-sector-creator',
  templateUrl: './sector-creator.component.html',
  styleUrls: ['./sector-creator.component.scss']
})
export class SectorCreatorComponent implements OnInit {
  sectorType: SectorType;
  mongooseSector: MongooseSector;
  starFrontiersSector: StarFrontiersSector;
  t5Sector: T5Sector;
  rttWorldgenSector: RTTWorldgenSector;

  constructor(private _router: Router,
              private _httpClient: HttpClient) {
  }

  ngOnInit() {
    this.mongooseSector = {} as MongooseSector;
    this.mongooseSector.subsectors = [] as MongooseSubsector[];
    this.starFrontiersSector = {} as StarFrontiersSector;
    this.starFrontiersSector.subsectors = [] as StarFrontiersSubsector[];
    this.t5Sector = {} as T5Sector;
    this.t5Sector.subsectors = [] as T5Subsector[];
    this.rttWorldgenSector = {} as RTTWorldgenSector;
    this.rttWorldgenSector.subsectors = [] as RTTWorldgenSubsector[];
  }

  generateSector() {
    this.ngOnInit();

    let url = "http://localhost:5000/CreateSector/";
    switch(this.sectorType){
      case SectorType.Basic:
        url += "BasicSector";
        this._httpClient.get<MongooseSector>(url).subscribe((x: MongooseSector) => {
          this.mongooseSector = x;
        });
        break;
      case SectorType.SpaceOpera:
        url += "SpaceOperaSector";
        this._httpClient.get<MongooseSector>(url).subscribe((x: MongooseSector) => {
          this.mongooseSector = x;
        });
        break;
      case SectorType.HardScience:
        url += "HardScienceSector";
        this._httpClient.get<MongooseSector>(url).subscribe((x: MongooseSector) => {
          this.mongooseSector = x;
        });
        break;
      case SectorType.StarFrontiers:
        url += "StarFrontiersSector";
        this._httpClient.get<StarFrontiersSector>(url).subscribe((x: StarFrontiersSector) => {
          this.starFrontiersSector = x;
        });
        break;
      case SectorType.T5:
        url += "T5Sector";
        this._httpClient.get<StarFrontiersSector>(url).subscribe((x: StarFrontiersSector) => {
          this.t5Sector = x;
        });
        break;
      case SectorType.RTTWorldgen:
        url += "RTTWorldgenSector";
        this._httpClient.get<RTTWorldgenSector>(url).subscribe((x: RTTWorldgenSector) => {
          this.rttWorldgenSector = x;
        });
        break;
    }
  }

  downloadSector() {
    // let fileText = UWPService.GetFileFormatHeader();
    //
    // this.sector.subsectors.forEach(subsector => {
    //   subsector.hexes.forEach(hex => {
    //     fileText += UWPService.GetFileFormat(hex);
    //   });
    // });
  }

  isMongooseSector() {
    return this.mongooseSector.subsectors.length >= 1;
  }

  isStarFrontiersSector() {
    return this.starFrontiersSector.subsectors.length >= 1;
  }

  isRTTWorldgenSector() {
    return this.rttWorldgenSector.subsectors.length >= 1;
  }
}
