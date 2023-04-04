import {Component, Input, OnInit} from '@angular/core';
import {StarFrontiersSector} from "../../models/star-frontiers/star-frontiers-sector";

@Component({
  selector: 'app-star-frontiers-sector',
  templateUrl: './star-frontiers-sector.component.html',
  styleUrls: ['./star-frontiers-sector.component.css']
})
export class StarFrontiersSectorComponent implements OnInit {
  @Input() sector: StarFrontiersSector;

  constructor() { }

  ngOnInit(): void {
  }

}
