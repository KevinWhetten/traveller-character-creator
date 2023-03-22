import {Component, Input, OnInit} from '@angular/core';
import {Subsector as BasicSubsector} from "../models/basic/subsector";

@Component({
  selector: 'app-subsector',
  templateUrl: './subsector.component.html',
  styleUrls: ['./subsector.component.css']
})
export class SubsectorComponent implements OnInit {
  @Input() basicSubsector: BasicSubsector;

  constructor() {
  }

  ngOnInit(): void {
    const test = this.basicSubsector;
    let i = 0;
  }

}
