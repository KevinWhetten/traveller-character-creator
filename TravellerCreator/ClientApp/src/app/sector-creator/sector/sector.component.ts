import {Component, Input, OnInit} from '@angular/core';
import {Sector as BasicSector} from "../models/basic/sector";

@Component({
  selector: 'app-sector',
  templateUrl: './sector.component.html',
  styleUrls: ['./sector.component.css']
})
export class SectorComponent implements OnInit {
  @Input() basicSector: BasicSector;

  constructor() {
  }

  ngOnInit(): void {
  }

}
