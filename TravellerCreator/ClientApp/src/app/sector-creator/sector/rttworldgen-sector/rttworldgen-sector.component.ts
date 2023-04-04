import {Component, Input, OnInit} from '@angular/core';
import {RTTWorldgenSector} from "../../models/rtt-worldgen/rtt-worldgen.sector";

@Component({
  selector: 'app-rttworldgen-sector',
  templateUrl: './rttworldgen-sector.component.html',
  styleUrls: ['./rttworldgen-sector.component.css']
})
export class RTTWorldgenSectorComponent implements OnInit {
  @Input() sector: RTTWorldgenSector;

  constructor() { }

  ngOnInit(): void {
  }

}
