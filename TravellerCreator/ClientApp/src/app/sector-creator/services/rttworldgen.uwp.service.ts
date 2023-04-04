import { Injectable } from '@angular/core';
import {UWPService} from "./uwp.service";
import {RollingService} from "../../services/rolling.service";
import {RTTWorldgenHex} from "../models/rtt-worldgen/rtt-worldgen.hex";
import {RTTWorldgenPlanet} from "../models/rtt-worldgen/rtt-worldgen.planet";
import {PlanetType} from "../models/enums/planet-type";

@Injectable({
  providedIn: 'root'
})
export class RTTWorldgenUWPService extends UWPService {

  constructor(protected _rollingService: RollingService) {
    super(_rollingService);
  }

  GetBestRTTWorldgenPlanet(hex: RTTWorldgenHex): RTTWorldgenPlanet | null {
    let bestPlanet = null;
    let mostImportant = -10;
    hex.starSystems.forEach(starSystem => {
      if (starSystem != null) {
        starSystem.planets.forEach(planet => {
          if (planet != null && planet.planetType == PlanetType.Terrestrial) {
            let importance = this.GetIx(planet);
            if (importance > mostImportant) {
              bestPlanet = planet;
              mostImportant = importance;
            }
          }
        });
      }
    });
    return bestPlanet;
  }
}
