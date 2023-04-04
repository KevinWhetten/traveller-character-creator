import {Component, Input, OnInit} from "@angular/core";
import {MongooseSubsector} from "../../models/mongoose/mongoose-subsector";
import {MongoosePlanet} from "../../models/mongoose/mongoose-planet";
import {MongooseHex} from "../../models/mongoose/mongoose-hex";
import {UWPService} from "../../services/uwp.service";
import {StarSystemType} from "../../models/enums/star-system-type";
import {MongooseStarSystem} from "../../models/mongoose/mongoose-star-system";

@Component({
  selector: 'app-mongoose-subsector',
  templateUrl: './mongoose-subsector.component.html',
  styleUrls: ['./mongoose-subsector.component.css']
})
export class MongooseSubsectorComponent implements OnInit {
  @Input() subsector: MongooseSubsector;
  private planet: MongoosePlanet;

  constructor(private _uwpService: UWPService) {
  }

  ngOnInit(): void {
  }

  getSubsectorCoordinates() {
    return `${this.subsector.coordinates.x}, ${this.subsector.coordinates.y}`;
  }

  getCoordinates(hex: MongooseHex): string {
    this.planet = (hex.starSystems[0] as MongooseStarSystem).planets[0] as MongoosePlanet;
    return this._uwpService.getCoordinates(hex)
  }

  getPlanetName(): string {
    return this.planet != null ? this.planet.name : '';
  }

  getUWP(): string {
    return this._uwpService.GetUWP(this.planet);
  }

  getTradeCodes(): string {
    return this._uwpService.getTradeCodes(this.planet)
  }

  getIx(): string {
    if (this.planet != null) {
      return `{ ${this._uwpService.GetIx(this.planet)} }`;
    }
    return '';
  }

  getEx(): string {
    return this._uwpService.GetEx(this.planet);
  }

  getCx(): string {
    return this._uwpService.GetCx(this.planet);
  }

  getN(): string {
    return this._uwpService.GetN(this.planet);
  }

  getBases(): string {
    return this._uwpService.getBases(this.planet);
  }

  getZ(): string {
    return this._uwpService.GetZ(this.planet);
  }

  getPBG(hex: MongooseHex): string {
    if (hex !== null) {
      return this._uwpService.GetPBG(hex);
    }
    return '';
  }

  getWorldNum(hex: MongooseHex): number {
    let worldNum = 0;

    if (hex.starSystems.length > 0) {
      let starSystem = hex.starSystems[0] as MongooseStarSystem;
      if (starSystem != null && starSystem.type == StarSystemType.Mongoose) {
        worldNum += starSystem.planets.length;
        if (starSystem.gasGiant) {
          worldNum++;
        }
      }
    }

    return worldNum;
  }

  getAllegiance(hex: MongooseHex): string {
    return this._uwpService.GetAllegiance(this.planet);
  }

  getStellar(hex: MongooseHex): string {
    return this._uwpService.GetStellar(this.planet);
  }
}
