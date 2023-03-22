import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {Sector as BasicSector} from "./models/basic/sector";
import {Subsector} from "./models/basic/subsector";

@Component({
  selector: 'app-sector-creator',
  templateUrl: './sector-creator.component.html',
  styleUrls: ['./sector-creator.component.scss']
})
export class SectorCreatorComponent implements OnInit {
  basicSector: BasicSector;

  constructor(private _router: Router,
              private _httpClient: HttpClient) {
  }

  ngOnInit() {
    this.basicSector.subsectors = [] as Subsector[];
  }

  generateBasicSector() {
    this._httpClient.get<BasicSector>("http://localhost:5000/CreateSector/BasicSector").subscribe((x: BasicSector) => {
      this.basicSector = x;
    });
  }

  generateSpaceOperaSector() {

  }

  generateHardScienceSector() {

  }

  generateSecondSurveySector() {

  }

  generateRttSector() {
  }
}
