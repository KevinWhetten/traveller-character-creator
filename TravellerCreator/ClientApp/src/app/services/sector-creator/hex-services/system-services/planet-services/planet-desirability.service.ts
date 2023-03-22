import { Injectable } from '@angular/core';
import {Planet} from "../../../../../models/sector-creator/hex/system/planet/planet";
import {Desirability} from "../../../../../models/sector-creator/hex/system/planet/desirability";
import {Race} from "../../../../../models/sector-creator/race/race";
import {PlanetOrbit} from "../../../../../models/sector-creator/hex/system/planet/planet-orbit";
import {SpectralType} from "../../../../../models/sector-creator/hex/system/star/spectral-type";
import {Luminosity} from "../../../../../models/sector-creator/hex/system/star/luminosity";
import {Chemistry} from "../../../../../models/sector-creator/hex/system/planet/chemistry";
import {RollingService} from "../../../../data-services/rolling.service";

@Injectable({
  providedIn: 'root'
})
export class PlanetDesirabilityService {

  constructor(private _rollingService: RollingService) { }

  getDesirability(planet: Planet): Desirability {
    let desirability = {} as Desirability;
    desirability.Human = this.getHumanDesirability(planet);
    desirability.Aslan = this.getAslanDesirability(planet);
    desirability.Mannu = this.getMannuDesirability(planet);
    desirability.Largosian = this.getLargosianDesirability(planet);
    desirability.Tortosian = this.getTortosianDesirability(planet);
    desirability.Ithromir = this.getIthromirDesirability(planet);
    desirability.Chrotos = this.getChrotosDesirability(planet);
    desirability.KaSara = this.getKaSaraDesirability(planet);
    desirability.TheCollective = this.getTheCollectiveDesirability(planet);
    desirability.Vargr = this.getVargrDesirability(planet);
    desirability.Scrapper = 10;

    return desirability;
  }

  getDesirabilityFor(race: Race, planet: Planet) {
    switch (race.name) {
      case "humans":
        return this.getHumanDesirability(planet);
      case "aslan":
        return this.getAslanDesirability(planet);
      case "mannu":
        return this.getMannuDesirability(planet);
      case "largosians":
        return this.getLargosianDesirability(planet);
      case "tortosians":
        return this.getTortosianDesirability(planet);
      case "ithromir":
        return this.getIthromirDesirability(planet);
      case "chrotos":
        return this.getChrotosDesirability(planet);
      case "theCollective":
        return this.getTheCollectiveDesirability(planet);
      case "kaSara":
        return this.getKaSaraDesirability(planet);
      case "vargr":
        return this.getVargrDesirability(planet);
      case "scrappers":
        return 10;
      default:
        return 0;
    }
  }

  private getHumanDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.Yellow ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.V ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 8) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 6 ? 2 : 0)
      + (planet.atmosphere == 5 || planet.atmosphere == 8 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 7) / 2) + 2)
      // Temperature
      + (6 <= planet.temperature && planet.temperature <= 8 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Water ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getAslanDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.Orange ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.V ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 8) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 7 ? 2 : 0)
      + (planet.atmosphere == 4 || planet.atmosphere == 9 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 6) / 2) + 2)
      // Temperature
      + (planet.temperature == 5 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Water ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getMannuDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.Orange ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.IV ? 1 : 0)
      // Size
      + Math.max(-2, Math.ceil(-Math.abs(planet.size - 6) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 8 ? 2 : 0)
      + (planet.atmosphere == 6 || planet.atmosphere == 13 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 8) / 2) + 2)
      // Temperature
      + (planet.temperature == 5 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Water ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getLargosianDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.BlueWhite ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.IV ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 5) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 7 ? 2 : 0)
      + (planet.atmosphere == 4 || planet.atmosphere == 9 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 2) / 2) + 2)
      // Temperature
      + (planet.temperature == 9 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Water ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getTortosianDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.BlueWhite ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.IV ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 4) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 9 ? 2 : 0)
      + (planet.atmosphere == 7 || planet.atmosphere == 10 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 6) / 2) + 2)
      // Temperature
      + (planet.temperature == 9 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Water ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getIthromirDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.White ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.V ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 10) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 13 ? 2 : 0)
      + (planet.atmosphere == 8 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 10) / 2) + 2)
      // Temperature
      + (10 <= planet.temperature && planet.temperature <= 11 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Water ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getChrotosDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.Red ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.VI ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 3) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 1 ? 2 : 0)
      + (planet.atmosphere == 0 || planet.atmosphere == 3 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 4) / 2) + 2)
      // Temperature
      + (planet.temperature <= 2 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Ammonia ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getKaSaraDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.Blue ? 2 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.BlueWhite ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.V ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 9) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 12 ? 2 : 0)
      + (planet.atmosphere == 11 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-planet.hydrosphere / 2) + 2)
      // Temperature
      + (planet.temperature >= 12 ? 1 : 0)
      // Biosphere
      + (planet.biosphere <= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getVargrDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.spectralType == SpectralType.Yellow ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.VI ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-Math.abs(planet.size - 7) / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 5 ? 2 : 0)
      + (planet.atmosphere == 3 || planet.atmosphere == 8 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-Math.abs(planet.hydrosphere - 5) / 2) + 2)
      // Temperature
      + (planet.temperature == 5 ? 1 : 0)
      // Biosphere
      + (planet.chemistry != Chemistry.Water ? -2 : 0)
      + (planet.biosphere >= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  private getTheCollectiveDesirability(planet: Planet) {
    return Math.ceil(this._rollingService.roll(1) / 2)
      + (planet.orbit == PlanetOrbit.Inner && planet.star.luminosity == Luminosity.III ? 2 : 0)
      + (planet.orbit == PlanetOrbit.Inner && (planet.star.luminosity == Luminosity.IV || planet.star.luminosity == Luminosity.II) ? 1 : 0)
      // Size
      + Math.max(-2, Math.round(-planet.size / 3) + 2)
      // Atmosphere
      + (planet.atmosphere == 0 ? 2 : 0)
      + (planet.atmosphere == 1 || planet.atmosphere == 2 ? 1 : 0)
      // Hydrosphere
      + Math.max(-2, Math.ceil(-planet.hydrosphere / 2) + 2)
      // Temperature
      + (6 <= planet.temperature && planet.temperature <= 8 ? 1 : 0)
      // Biosphere
      + (planet.biosphere <= 5 ? 1 : 0)
      + Math.floor(this.getResources(planet) / 2);
  }

  getResources(planet: Planet) {
    let resources = this._rollingService.roll(2);

    if (planet.settlement.techLevel >= 8) {
      resources += planet.pbg.giants + planet.pbg.belts;
    }

    return Math.min(15, resources);
  }
}
