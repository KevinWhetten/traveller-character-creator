import { Injectable } from '@angular/core';
import {SystemService} from "./system-services/system.service";
import {System} from "../../../models/sector-creator/hex/system/system";
import {Races} from "../../../models/sector-creator/race/races";
import {RollingService} from "../../data-services/rolling.service";
import {Hex} from "../../../models/sector-creator/hex/hex";
import {Coordinates} from "../../../models/sector-creator/hex/coordinates";

@Injectable({
  providedIn: 'root'
})
export class HexService {
  private hex: Hex;

  constructor(private _rollingService: RollingService, private _systemService: SystemService) { }

  createNewHex(coordinates: Coordinates): Hex {
    this.hex = {} as Hex;
    this.hex.coordinates = coordinates;

    for (let race of Races.AllRaces) {
      if (coordinates.row == race.homeworldCoordinates.row
        && coordinates.column == race.homeworldCoordinates.column) {
        this.hex.systems.push(this._systemService.createHomeworldSystem(race));
        return this.hex;
      }
    }

    this.hex.systems = this.getSystems();
    return this.hex;
  }

  private getSystems(): System[] {
    return this._systemService.createSystems();
  }
}
