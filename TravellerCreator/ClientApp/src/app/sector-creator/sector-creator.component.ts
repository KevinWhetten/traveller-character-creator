import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {UWPService} from "./services/uwp.service";
import {BasicSector} from "./models/basic/basic-sector";
import {BasicSubsector} from "./models/basic/basic-subsector";
import {SectorType} from "./models/enums/sector-type";
import {ISector} from "./models/interfaces/sector";
import {ISubsector} from "./models/interfaces/subsector";

@Component({
  selector: 'app-sector-creator',
  templateUrl: './sector-creator.component.html',
  styleUrls: ['./sector-creator.component.scss']
})
export class SectorCreatorComponent implements OnInit {
  sectorType: SectorType;
  sector: ISector;

  constructor(private _router: Router,
              private _httpClient: HttpClient) {
  }

  ngOnInit() {
    this.sector = {} as ISector;
    this.sector.subsectors = [] as ISubsector[];
  }

  generateSector() {
    let url = "http://localhost:5000/CreateSector/";
    switch(this.sectorType){
      case SectorType.Basic:
        url += "BasicSector";
        break;
      case SectorType.SpaceOpera:
        url += "SpaceOperaSector";
        break;
      case SectorType.HardScience:
        url += "HardScienceSector";
        break;
      case SectorType.StarFrontiers:
        url += "StarFrontiersSector";
        break;
      case SectorType.T5:
        url += "T5Sector";
        break;
      case SectorType.RTTWorldgen:
        url += "RTTWorldgenSector";
        break;
    }
    this._httpClient.get<BasicSector>(url).subscribe((x: ISector) => {
      this.sector = x;
    });
  }

  downloadSector() {
    let fileText = UWPService.GetFileFormatHeader();

    this.sector.subsectors.forEach(subsector => {
      subsector.hexes.forEach(hex => {
        fileText += UWPService.GetFileFormat(hex);
      });
    });
  }
}
