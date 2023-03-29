import {Component, Input, OnInit} from "@angular/core";
import {BasicSubsector} from "../../models/basic/basic-subsector";
import {BasicPlanet} from "../../models/basic/basic-planet";
import {BasicHex} from "../../models/basic/basic-hex";
import {UWPService} from "../../services/uwp.service";
import {StarSystemType} from "../../models/enums/star-system-type";
import {BasicStarSystem} from "../../models/basic/basic-star-system";

@Component({
  selector: 'app-basic-subsector',
  templateUrl: './basic-subsector.component.html',
  styleUrls: ['./basic-subsector.component.css']
})
export class BasicSubsectorComponent implements OnInit {
  @Input() subsector: BasicSubsector;
  private planet: BasicPlanet;

  constructor() {
  }

  ngOnInit(): void {
  }

  getSubsectorCoordinates() {
    return `${this.subsector.coordinates.x}, ${this.subsector.coordinates.y}`;
  }

  getCoordinates(hex: BasicHex): string {
    this.planet = (hex.starSystems[0] as BasicStarSystem).planet as BasicPlanet;
    return UWPService.getCoordinates(hex)
  }

  getPlanetName(): string {
    return this.planet != null ? this.planet.name : '';
  }

  getUWP(): string {
    return UWPService.GetUWP(this.planet);
  }

  getTradeCodes(): string {
    return UWPService.getTradeCodes(this.planet)
  }

  getIx(): string {
    return UWPService.GetIx(this.planet);
  }

  getEx(): string {
    return UWPService.GetEx(this.planet);
  }

  getCx(): string {
    return UWPService.GetCx(this.planet);
  }

  getN(): string {
    return UWPService.GetN(this.planet);
  }

  getBases(): string {
    return UWPService.getBases(this.planet);
  }

  getZ(): string {
    return UWPService.GetZ(this.planet);
  }

  getPBG(hex: BasicHex): string {
    if (hex !== null) {
      return UWPService.GetPBG(hex);
    }
    return '';
  }

  getWorldNum(hex: BasicHex): number {
    let worldNum = 0;

    if (hex.starSystems.length > 0) {
      let starSystem = hex.starSystems[0] as BasicStarSystem;
      if (starSystem != null && starSystem.type == StarSystemType.Basic) {
        if (starSystem.planet !== null) {
          worldNum++;
        }
        if (starSystem.gasGiant) {
          worldNum++;
        }
      }
    }

    return worldNum;
  }

  getAllegiance(hex: BasicHex): string {
    return UWPService.GetAllegiance(this.planet);
  }

  getStellar(hex: BasicHex): string {
    return UWPService.GetStellar(this.planet);
  }
}
