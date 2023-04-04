import {Component, Input, OnInit} from '@angular/core';
import {MongooseSector} from "../../models/mongoose/mongoose-sector";

@Component({
  selector: 'app-mongoose-sector',
  templateUrl: './mongoose-sector.component.html',
  styleUrls: ['./mongoose-sector.component.css']
})
export class MongooseSectorComponent implements OnInit {
  @Input() sector: MongooseSector;

  constructor() { }

  ngOnInit(): void {
  }

}
