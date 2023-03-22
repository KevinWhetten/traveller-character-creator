import { Injectable } from '@angular/core';
import {Planet} from "../../../../../models/sector-creator/hex/system/planet/planet";
import {SpectralType} from "../../../../../models/sector-creator/hex/system/star/spectral-type";
import {PlanetOrbit} from "../../../../../models/sector-creator/hex/system/planet/planet-orbit";
import {Chemistry} from "../../../../../models/sector-creator/hex/system/planet/chemistry";
import {Luminosity} from "../../../../../models/sector-creator/hex/system/star/luminosity";

import {PlanetType} from "../../../../../models/sector-creator/hex/system/planet/planet-type";

import {RollingService} from "../../../../data-services/rolling.service";

@Injectable({
  providedIn: 'root'
})
export class PlanetGenerationService {

  constructor(private _rollingService: RollingService) { }

  generatePlanet(planet: Planet): Planet {
    switch (planet.type) {
      case PlanetType.Acheronian:
        return this.generateAcheronian(planet);
      case PlanetType.Arean:
        return this.generateArean(planet);
      case PlanetType.Arid:
        return this.generateArid(planet);
      case PlanetType.Asphodelian:
        return this.generateAsphodelian(planet);
      case PlanetType.Chthonian:
        return this.generateChthonian(planet);
      case PlanetType.Hebean:
        return this.generateHebean(planet);
      case PlanetType.Helian:
        return this.generateHelian(planet);
      case PlanetType.JaniLithic:
        return this.generateJaniLithic(planet);
      case PlanetType.Jovian:
        return this.generateJovian(planet);
      case PlanetType.Meltball:
        return this.generateMeltball(planet);
      case PlanetType.Oceanic:
        return this.generateOceanic(planet);
      case PlanetType.Panthalassic:
        return this.generatePanthalassic(planet);
      case PlanetType.Promethean:
        return this.generatePromethean(planet);
      case PlanetType.Rockball:
        return this.generateRockball(planet);
      case PlanetType.SmallBody:
        return this.generateSmallBody(planet);
      case PlanetType.Snowball:
        return this.generateSnowball(planet);
      case PlanetType.Stygian:
        return this.generateStygian(planet);
      case PlanetType.Tectonic:
        return this.generateTectonic(planet);
      case PlanetType.Telluric:
        return this.generateTelluric(planet);
      case PlanetType.Vesperian:
        return this.generateVesperian(planet);
      default:
        return this.generateUnknown(planet);
    }
  }

  private generateAcheronian(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 4;
    planet.atmosphere = 1;
    planet.hydrosphere = 0;
    planet.biosphere = 0;

    return planet;
  }

  private generateArean(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) - 1;

    // Atmosphere
    let roll = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.WhiteDwarf ? -2 : 0);
    if (roll <= 3) {
      planet.atmosphere = 1;
    } else {
      planet.atmosphere = 10;
    }

    // Hydrosphere
    planet.hydrosphere = Math.max(0,
      this._rollingService.roll(2) + planet.size - 7
      + (planet.atmosphere == 1 ? -4 : 0));

    // Chemistry
    let chemistry = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.Red ? 2 : 0)
      + (planet.orbit == PlanetOrbit.Outer ? 2 : 0);

    let ageMod = 0;
    if (chemistry <= 4) {
      planet.chemistry = Chemistry.Water;
    }
    if (chemistry <= 6) {
      planet.chemistry = Chemistry.Ammonia;
      ageMod += 1;
    } else if (chemistry >= 7) {
      planet.chemistry = Chemistry.Methane;
      ageMod += 3;
    }

    // Biosphere
    if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2) + ageMod && planet.atmosphere == 1) {
      planet.biosphere = Math.max(this._rollingService.roll(1) - 4, 0);
    } else if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2) + ageMod && planet.atmosphere == 10) {
      planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
    } else if (planet.star.age >= 4 + ageMod && planet.atmosphere == 10) {
      planet.biosphere = this._rollingService.roll(1) + planet.size - 2;
    } else {
      planet.biosphere = 0;
    }

    return planet;
  }

  private generateArid(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 4;

    // Chemistry
    let chemistry = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.Yellow && planet.star.luminosity == Luminosity.V ? 2 : 0)
      + (planet.star.spectralType == SpectralType.Orange && planet.star.luminosity == Luminosity.V ? 4 : 0)
      + (planet.star.spectralType == SpectralType.Red ? 5 : 0)
      + (planet.orbit == PlanetOrbit.Outer ? 2 : 0);

    let ageMod = 0;
    if (chemistry <= 6) {
      planet.chemistry = Chemistry.Water;
    } else if (chemistry <= 8) {
      planet.chemistry = Chemistry.Ammonia;
      ageMod += 1;
    } else if (chemistry >= 9) {
      planet.chemistry = Chemistry.Methane;
      ageMod += 3;
    }

    // Biosphere
    if (planet.star.age >= 4 + ageMod) {
      planet.biosphere = Math.max(0, this._rollingService.roll(2)
        + (planet.star.spectralType == SpectralType.WhiteDwarf ? -3 : 0));
    } else if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2) + ageMod) {
      planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
    } else {
      planet.biosphere = 0;
    }

    // Atmosphere
    if (planet.biosphere >= 3 && planet.chemistry == Chemistry.Water) {
      planet.atmosphere = Math.min(9, Math.max(2, this._rollingService.roll(2) - 7 + planet.size));
    } else {
      planet.atmosphere = 10;
    }

    // Hydrosphere
    planet.hydrosphere = Math.ceil(this._rollingService.roll(1) / 2);

    return planet;
  }

  private generateAsphodelian(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 9;
    planet.atmosphere = 1;
    planet.hydrosphere = 0;
    planet.biosphere = 0;

    return planet;
  }

  private generateChthonian(planet: Planet): Planet {
    planet.size = 15;
    planet.atmosphere = 1;
    planet.hydrosphere = 0;
    planet.biosphere = 0;

    return planet;
  }

  private generateHebean(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) - 1;
    planet.atmosphere = Math.max(0, this._rollingService.roll(1) + planet.size - 6);
    if (planet.atmosphere >= 2) {
      planet.atmosphere = 10;
    }
    planet.hydrosphere = Math.max(0, this._rollingService.roll(2) + planet.size - 11);

    planet.biosphere = 0;

    return planet;
  }

  private generateHelian(planet: Planet): Planet {
    planet.size = Math.min(14, this._rollingService.roll(1) + 9);
    planet.atmosphere = 13;

    // Hydrosphere
    let roll = this._rollingService.roll(1);
    if (roll <= 2) {
      planet.hydrosphere = 0;
    } else if (roll <= 4) {
      planet.hydrosphere = this._rollingService.roll(2) - 1;
    } else {
      planet.hydrosphere = 12;
    }

    planet.biosphere = 0;

    return planet;
  }

  private generateJaniLithic(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 4;

    // Atmosphere
    let roll = this._rollingService.roll(1);
    if (roll <= 3) {
      planet.atmosphere = 1;
    } else {
      planet.atmosphere = 10;
    }

    planet.hydrosphere = 0;
    planet.biosphere = 0;

    return planet;
  }

  private generateJovian(planet: Planet): Planet {
    planet.size = 15;
    planet.atmosphere = 15;
    planet.hydrosphere = 15;

    // Biosphere
    let roll = this._rollingService.roll(1)
      + (planet.orbit == PlanetOrbit.Inner ? 2 : 0);

    if (roll <= 5) {
      planet.biosphere = 0;
    } else {
      if (planet.star.age >= 7) {
        planet.biosphere = Math.max(this._rollingService.roll(2)
          + (planet.star.spectralType == SpectralType.WhiteDwarf ? -3 : 0), 0);
      } else if (planet.star.age >= this._rollingService.roll(1)) {
        planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
      } else {
        planet.biosphere = 0;
      }
    }

    // Chemistry
    if (planet.biosphere >= 1) {
      let chemistry = this._rollingService.roll(1)
        + (planet.star.spectralType == SpectralType.Red ? 1 : 0)
        + (planet.orbit == PlanetOrbit.Epistellar ? -2 : 0)
        + (planet.orbit == PlanetOrbit.Outer ? 2 : 0);

      if (chemistry <= 3) {
        planet.chemistry = Chemistry.Water;
      } else if (chemistry >= 4) {
        planet.chemistry = Chemistry.Ammonia;
      }
    }

    return planet;
  }

  private generateMeltball(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) - 1;
    planet.atmosphere = 1;
    planet.hydrosphere = 12;
    planet.biosphere = 0;

    return planet;
  }

  private generateOceanic(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 4;

    // Chemistry
    let chemistry = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.Yellow && planet.star.luminosity == Luminosity.V ? 2 : 0)
      + (planet.star.spectralType == SpectralType.Orange && planet.star.luminosity == Luminosity.V ? 4 : 0)
      + (planet.star.spectralType == SpectralType.Red ? 5 : 0)
      + (planet.orbit == PlanetOrbit.Outer ? 2 : 0);

    let ageMod = 0;
    if (chemistry <= 6) {
      planet.chemistry = Chemistry.Water;
    } else if (chemistry <= 8) {
      planet.chemistry = Chemistry.Ammonia;
      ageMod += 1;
    } else {
      planet.chemistry = Chemistry.Methane;
      ageMod += 3;
    }

    // Biosphere
    if (planet.star.age >= 4 + ageMod) {
      planet.biosphere = Math.max(this._rollingService.roll(2)
        + (planet.star.spectralType == SpectralType.WhiteDwarf ? -3 : 0), 0);
    } else if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2) + ageMod) {
      planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
    } else {
      planet.biosphere = 0;
    }

    // Atmosphere
    if (planet.chemistry == Chemistry.Water) {
      planet.atmosphere = Math.min(12, Math.max(1, this._rollingService.roll(2) + planet.size - 6 +
        (planet.star.spectralType == SpectralType.Yellow && planet.star.luminosity == Luminosity.V ? -1 : 0) +
        (planet.star.spectralType == SpectralType.Orange && planet.star.luminosity == Luminosity.V ? -2 : 0) +
        (planet.star.spectralType == SpectralType.Red ? -3 : 0) +
        (planet.star.luminosity == Luminosity.IV ? -1 : 0)));
    } else {
      let roll = this._rollingService.roll(1);
      if (roll == 1) {
        planet.atmosphere = 1;
      } else if (roll <= 4) {
        planet.atmosphere = 10;
      } else {
        planet.atmosphere = 12;
      }
    }

    planet.hydrosphere = 11;

    return planet;
  }

  private generatePanthalassic(planet: Planet): Planet {
    planet.size = Math.min(14, this._rollingService.roll(1) + 9);
    planet.atmosphere = Math.min(13, this._rollingService.roll(1) + 8);
    planet.hydrosphere = 11;

    // Chemistry
    let chemistry = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.Yellow && planet.star.luminosity == Luminosity.V ? 2 : 0)
      + (planet.star.spectralType == SpectralType.Orange && planet.star.luminosity == Luminosity.V ? 4 : 0)
      + (planet.star.spectralType == SpectralType.Red ? 5 : 0);

    let ageMod = 0;
    if (chemistry <= 6) {
      chemistry = this._rollingService.roll(2);
      if (chemistry <= 8) {
        planet.chemistry = Chemistry.Water;
      } else if (chemistry <= 11) {
        planet.chemistry = Chemistry.Sulfur;
      } else {
        planet.chemistry = Chemistry.Chlorine;
      }
    } else if (chemistry <= 8) {
      planet.chemistry = Chemistry.Methane;
      ageMod += 1;
    } else if (chemistry >= 9) {
      planet.chemistry = Chemistry.Methane;
      ageMod += 3;
    }

    // Biosphere
    if (planet.star.age >= 4 + ageMod) {
      planet.biosphere = this._rollingService.roll(2);
    } else if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2) + ageMod) {
      planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
    } else {
      planet.biosphere = 0;
    }

    return planet;
  }

  private generatePromethean(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) - 1;

    // Chemistry
    let chemistry = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.Red ? 2 : 0)
      + (planet.orbit == PlanetOrbit.Epistellar ? -2 : 0)
      + (planet.orbit == PlanetOrbit.Outer ? 2 : 0);

    let ageMod = 0;
    if (chemistry <= 4) {
      planet.chemistry = Chemistry.Water;
    } else if (chemistry <= 6) {
      planet.chemistry = Chemistry.Ammonia;
      ageMod += 1;
    } else {
      planet.chemistry = Chemistry.Methane;
      ageMod += 3;
    }

    // Biosphere
    if (planet.star.age >= 4 + ageMod) {
      planet.biosphere = Math.max(this._rollingService.roll(2)
        + (planet.star.spectralType == SpectralType.WhiteDwarf ? -3 : 0), 0);
    } else if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2) + ageMod) {
      planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
    } else {
      planet.biosphere = 0;
    }

    // Atmosphere
    if (planet.biosphere >= 3 && planet.chemistry == Chemistry.Water) {
      planet.atmosphere = Math.min(9, Math.max(2, this._rollingService.roll(2) + planet.size - 7));
    } else {
      planet.atmosphere = 10;
    }

    planet.hydrosphere = this._rollingService.roll(2) - 2;

    return planet;
  }

  private generateRockball(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) - 1;
    planet.atmosphere = 0;
    planet.hydrosphere = Math.max(0,
      this._rollingService.roll(2) + planet.size - 11
      + (planet.star.spectralType == SpectralType.Red ? 1 : 0)
      + (planet.orbit == PlanetOrbit.Epistellar ? -2 : 0)
      + (planet.orbit == PlanetOrbit.Outer ? 2 : 0));
    planet.biosphere = 0;

    return planet;
  }

  private generateSmallBody(planet: Planet): Planet {
    planet.size = 0;
    planet.atmosphere = 0;
    planet.hydrosphere = 0;
    planet.biosphere = 0;

    return planet;
  }

  private generateSnowball(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) - 1;

    // Atmosphere
    let roll = this._rollingService.roll(1)
    if (roll <= 4) {
      planet.atmosphere = 0;
    } else {
      planet.atmosphere = 1;
    }

    // Hydrosphere
    roll = this._rollingService.roll(1);
    if (roll <= 3) {
      planet.hydrosphere = 10;
    } else {
      planet.hydrosphere = this._rollingService.roll(2) - 2;
    }

    // Chemistry
    let chemistry = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.Red ? 2 : 0)
      + (planet.orbit == PlanetOrbit.Outer ? 2 : 0);

    let ageMod = 0;
    if (chemistry <= 4) {
      planet.chemistry = Chemistry.Water;
    } else if (chemistry <= 6) {
      planet.chemistry = Chemistry.Ammonia;
      ageMod = 1;
    } else if (chemistry >= 7) {
      planet.chemistry = Chemistry.Methane;
      ageMod = 3;
    }

    // Biosphere
    if (planet.star.age >= 6 + ageMod) {
      planet.biosphere = Math.max(this._rollingService.roll(1) + planet.size - 2, 0);
    } else if (planet.star.age >= this._rollingService.roll(1)) {
      planet.biosphere = Math.max(this._rollingService.roll(1) - 3, 0);
    } else {
      planet.biosphere = 0;
    }

    return planet;
  }

  private generateStygian(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) - 1;
    planet.atmosphere = 0;
    planet.hydrosphere = 0;
    planet.biosphere = 0;

    return planet;
  }

  private generateTectonic(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 4;

    // Chemistry
    let chemistry = this._rollingService.roll(1)
      + (planet.star.spectralType == SpectralType.Yellow && planet.star.luminosity == Luminosity.V ? 2 : 0)
      + (planet.star.spectralType == SpectralType.Orange && planet.star.luminosity == Luminosity.V ? 4 : 0)
      + (planet.star.spectralType == SpectralType.Red ? 5 : 0)
      + (planet.orbit == PlanetOrbit.Outer ? 2 : 0);

    let ageMod = 0;
    if (chemistry <= 6) {
      let roll = this._rollingService.roll(2);
      if (roll <= 8) {
        planet.chemistry = Chemistry.Water;
      } else if (roll <= 11) {
        planet.chemistry = Chemistry.Sulfur;
      } else {
        planet.chemistry = Chemistry.Chlorine;
      }
    } else if (chemistry <= 8) {
      planet.chemistry = Chemistry.Ammonia;
      ageMod = 1;
    } else {
      planet.chemistry = Chemistry.Methane;
      ageMod = 3;
    }

    // Biosphere
    if (planet.star.age >= 4 + ageMod) {
      planet.biosphere = Math.max(0, this._rollingService.roll(2)
        + (planet.star.spectralType == SpectralType.WhiteDwarf ? -3 : 0));
    } else if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2) + ageMod) {
      planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
    } else {
      planet.biosphere = 0;
    }

    // Atmosphere
    if (planet.biosphere >= 3 && planet.chemistry == Chemistry.Water) {
      planet.atmosphere = Math.min(9, Math.max(0, this._rollingService.roll(2) + planet.size - 7));
    } else if (planet.biosphere >= 3 && (planet.chemistry == Chemistry.Sulfur || planet.chemistry == Chemistry.Chlorine)) {
      planet.atmosphere = 11;
    } else {
      planet.atmosphere = 10;
    }

    planet.hydrosphere = this._rollingService.roll(2) - 2;

    return planet;
  }

  private generateTelluric(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 4;
    planet.atmosphere = 12;

    // Hydrosphere
    let roll = this._rollingService.roll(1);
    if (roll <= 4) {
      planet.hydrosphere = 0;
    } else {
      planet.hydrosphere = 12;
    }

    planet.biosphere = 0;

    return planet;
  }

  private generateVesperian(planet: Planet): Planet {
    planet.size = this._rollingService.roll(1) + 4;

    // Chemistry
    let chemistry = this._rollingService.roll(2);
    if (chemistry <= 11) {
      planet.chemistry = Chemistry.Water;
    } else {
      planet.chemistry = Chemistry.Chlorine;
    }

    // Biosphere
    if (planet.star.age >= 4) {
      planet.biosphere = this._rollingService.roll(2);
    } else if (planet.star.age >= Math.ceil(this._rollingService.roll(1) / 2)) {
      planet.biosphere = Math.ceil(this._rollingService.roll(1) / 2);
    } else {
      planet.biosphere = 0;
    }

    // Atmosphere
    if (planet.biosphere >= 3 && planet.chemistry == Chemistry.Water) {
      planet.atmosphere = Math.min(9, Math.max(2, this._rollingService.roll(2) + planet.size - 7));
    } else if (planet.biosphere >= 3 && planet.chemistry == Chemistry.Chlorine) {
      planet.atmosphere = 11;
    } else {
      planet.atmosphere = 10;
    }

    planet.hydrosphere = this._rollingService.roll(2) - 2;

    return planet;
  }

  private generateUnknown(planet: Planet): Planet {
    planet.size = 0;
    planet.atmosphere = 0;
    planet.hydrosphere = 0;
    return planet;
  }
}
