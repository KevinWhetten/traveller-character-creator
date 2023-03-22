import {Component, Input} from '@angular/core';
import {Planet} from "../../../models/sector-creator/hex/system/planet/planet";

@Component({
  selector: 'app-planet',
  templateUrl: './planet.component.html',
  styleUrls: ['./planet.component.scss']
})
export class PlanetComponent {
  @Input() planet: Planet;

  constructor() {
  }
}
