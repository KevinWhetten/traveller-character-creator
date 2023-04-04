import {Component, Input, OnInit} from '@angular/core';
import {StarFrontiersSubsector} from "../../models/star-frontiers/star-frontiers-subsector";
import {StarFrontiersHex} from "../../models/star-frontiers/star-frontiers-hex";
import {StarFrontiersPlanet} from "../../models/star-frontiers/star-frontiers-planet";
import {StarFrontiersUWPService} from "../../services/star-frontiers.uwp.service";

@Component({
  selector: 'app-star-frontiers-subsector',
  templateUrl: './star-frontiers-subsector.component.html',
  styleUrls: ['./star-frontiers-subsector.component.css']
})
export class StarFrontiersSubsectorComponent implements OnInit {
  @Input() subsector: StarFrontiersSubsector;
  planet: StarFrontiersPlanet | null;

  constructor(private _uwpService: StarFrontiersUWPService) {
  }

  ngOnInit(): void {
  }

  getSubsectorCoordinates() {
    return `${this.subsector.coordinates.x}, ${this.subsector.coordinates.y}`;
  }

  getCoordinates(hex: StarFrontiersHex): string {
    this.planet = this._uwpService.GetBestStarFrontiersPlanet(hex);

    return this._uwpService.getCoordinates(hex);
  }

  getPlanetName(): string {
    if (this.planet != null) {
      return this.planet.name;
    }
    return '';
  }

  getUWP(): string {
    if (this.planet != null) {
      return this._uwpService.GetUWP(this.planet);
    }
    return '';
  }

  getTradeCodes(): string {
    if (this.planet != null) {
      return this._uwpService.getTradeCodes(this.planet);
    }
    return '';
  }

  getIx(): string {
    if (this.planet != null) {
      return `{ ${this._uwpService.GetIx(this.planet)} }`;
    }
    return '';
  }

  getEx(): string {
    if (this.planet != null) {
      return `${this._uwpService.GetEx(this.planet)}`;
    }
    return '';
  }

  getCx(): string {
    if (this.planet != null) {
      return `${this._uwpService.GetCx(this.planet)}`;
    }
    return '';
  }

  getN(): string {
    return '';
  }

  getBases(): string {
    if(this.planet != null){
      return this._uwpService.getBases(this.planet);
    }
    return '';
  }

  getZ(): string {
    return '';
  }

  getPBG(hex: StarFrontiersHex): string {
    return this._uwpService.GetPBG(hex)
  }

  getWorldNum(hex: StarFrontiersHex): string {
    let worldNum = 0;

    if (hex.starSystems.length > 0) {
      hex.starSystems.forEach(starSystem => {
        if (starSystem != null && starSystem.planets != null) {
          starSystem.planets.forEach(planet => {
            worldNum++;
          });
        }
      });
    }

    return this._uwpService.GetHexadecimal(worldNum);
  }

  getAllegiance(hex: StarFrontiersHex): string {
    return '';
  }

  getStellar(hex: StarFrontiersHex): string {
    return '';
  }

  notEmpty() {
    return this.planet != null;
  }
}
