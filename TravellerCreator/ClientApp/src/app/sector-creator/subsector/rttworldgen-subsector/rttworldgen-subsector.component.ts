import {Component, Input, OnInit} from '@angular/core';
import {RTTWorldgenSubsector} from "../../models/rtt-worldgen/rtt-worldgen.subsector";
import {RTTWorldgenPlanet} from "../../models/rtt-worldgen/rtt-worldgen.planet";
import {RTTWorldgenHex} from "../../models/rtt-worldgen/rtt-worldgen.hex";
import {RTTWorldgenUWPService} from "../../services/rttworldgen.uwp.service";


@Component({
  selector: 'app-rttworldgen-subsector',
  templateUrl: './rttworldgen-subsector.component.html',
  styleUrls: ['./rttworldgen-subsector.component.css']
})
export class RTTWorldgenSubsectorComponent implements OnInit {
  @Input() subsector: RTTWorldgenSubsector;
  planet: RTTWorldgenPlanet | null;

  constructor(private _uwpService: RTTWorldgenUWPService) {
  }

  ngOnInit(): void {
  }

  getSubsectorCoordinates() {
    return `${this.subsector.coordinates.x}, ${this.subsector.coordinates.y}`;
  }

  getCoordinates(hex: RTTWorldgenHex): string {
    this.planet = this._uwpService.GetBestRTTWorldgenPlanet(hex);

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

  getPBG(hex: RTTWorldgenHex): string {
    return this._uwpService.GetPBG(hex)
  }

  getWorldNum(hex: RTTWorldgenHex): string {
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

  getAllegiance(hex: RTTWorldgenHex): string {
    return '';
  }

  getStellar(hex: RTTWorldgenHex): string {
    return '';
  }

  notEmpty() {
    return this.planet != null;
  }
}
