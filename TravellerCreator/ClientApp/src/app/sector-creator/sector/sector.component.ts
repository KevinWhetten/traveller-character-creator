import {Component, Input, OnInit} from '@angular/core';
import {ISector} from "../models/interfaces/sector";
import {ISubsector} from "../models/interfaces/subsector";
import {BasicSubsector} from "../models/basic/basic-subsector";
import {SectorType} from "../models/enums/sector-type";

@Component({
  selector: 'app-sector',
  templateUrl: './sector.component.html',
  styleUrls: ['./sector.component.css']
})
export class SectorComponent implements OnInit {
  @Input() sector: ISector;

  constructor() {
  }

  ngOnInit(): void {
  }

  isBasicSector() {
    return this.sector.sectorType == SectorType.Basic;
  }

  getBasicSubsector(subsector: ISubsector) {
    return subsector as BasicSubsector;
  }
}
