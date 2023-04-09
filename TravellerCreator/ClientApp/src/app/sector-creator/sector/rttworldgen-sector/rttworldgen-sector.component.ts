import {Component, Input, OnInit} from '@angular/core';
import {RttWorldgenSector} from "../../models/rtt-worldgen/rtt-worldgen.sector";

@Component({
  selector: 'app-rttworldgen-sector',
  templateUrl: './rttworldgen-sector.component.html',
  styleUrls: ['./rttworldgen-sector.component.css']
})
export class RttWorldgenSectorComponent implements OnInit {
  @Input() sector: RttWorldgenSector;

  constructor() { }

  ngOnInit(): void {
  }

}
