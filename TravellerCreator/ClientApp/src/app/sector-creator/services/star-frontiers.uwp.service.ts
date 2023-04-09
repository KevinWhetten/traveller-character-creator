import {Injectable} from '@angular/core';
import {StarFrontiersPlanet} from "../models/star-frontiers/star-frontiers-planet";
import {TradeCode} from "../models/enums/trade-code";
import {StarFrontiersHex} from "../models/star-frontiers/star-frontiers-hex";
import {PlanetType} from "../models/enums/planet-type";
import {UWPService} from "./uwp.service";
import {RollingService} from "../../services/rolling.service";

@Injectable({
  providedIn: 'root'
})
export class StarFrontiersUWPService extends UWPService {
  constructor(_rollingService: RollingService) {
    super(_rollingService);
  }

  override GetEx(planet: StarFrontiersPlanet): string {
    let resources = this._rollingService.roll(2);

    if (planet.techLevel >= 8) {
      resources += 1;
    }
    let labor = planet.population - 1;

    let infrastructure = this._rollingService.roll(2) + this.GetIx(planet);
    if (planet.tradeCodes.includes(TradeCode.Barren) && planet.tradeCodes.includes(TradeCode.LowPopulation)) {
      infrastructure = 0;
    } else if (planet.tradeCodes.includes(TradeCode.LowPopulation)) {
      infrastructure = 1;
    } else if (planet.tradeCodes.includes(TradeCode.NonIndustrial)) {
      infrastructure = this._rollingService.roll(1) + this.GetIx(planet);
    }

    let efficiency = this._rollingService.roll(1) - this._rollingService.roll(1);

    resources = resources < 0 ? 0 : resources;
    labor = labor < 0 ? 0 : labor;
    infrastructure = infrastructure < 0 ? 0 : infrastructure;

    return `(${this.GetHexadecimal(resources)}${this.GetHexadecimal(labor)}${this.GetHexadecimal(infrastructure)}${efficiency >= 0 ? '+' + this.GetHexadecimal(efficiency) : this.GetHexadecimal(efficiency)})`;
  }

  override GetCx(planet: StarFrontiersPlanet): string {
    let homogeneity = planet.population + this._rollingService.roll(1) - this._rollingService.roll(1);
    homogeneity = homogeneity <= 0 ? 1 : homogeneity;
    let acceptance = planet.population + this.GetIx(planet);
    acceptance = acceptance <= 0 ? 1 : acceptance;
    let strangeness = this._rollingService.roll(1) - this._rollingService.roll(1) + 5;
    strangeness = strangeness <= 0 ? 1 : strangeness;
    let symbols = this._rollingService.roll(1) - this._rollingService.roll(1) + planet.techLevel;
    symbols = symbols <= 0 ? 1 : symbols;

    return `[${this.GetHexadecimal(homogeneity)}${this.GetHexadecimal(acceptance)}${this.GetHexadecimal(strangeness)}${this.GetHexadecimal(symbols)}]`;
  }

  override GetPBG(hex: StarFrontiersHex): string {let planetNum = 0;
    let beltNum = 0;
    let gasGiantNum = 0;

    if (hex.starSystems.length > 0) {
      hex.starSystems.forEach(starSystem => {
        if (starSystem != null && starSystem.planets != null) {
          starSystem.planets.forEach(planet => {
            if (planet.planetType == PlanetType.AsteroidBelt) {
              beltNum++;
            } else if (planet.planetType == PlanetType.Jovian) {
              gasGiantNum++;
            } else {
              planetNum++;
            }
          });
        }
      });
    }

    return `${this.GetHexadecimal(planetNum)}${this.GetHexadecimal(beltNum)}${this.GetHexadecimal(gasGiantNum)}`;

  }

  GetBestStarFrontiersPlanet(hex: StarFrontiersHex): StarFrontiersPlanet | null {
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
