import {Injectable} from "@angular/core";
import {Planet} from "src/app/models/sector-creator/hex/system/planet/planet";
import {PlanetType} from "src/app/models/sector-creator/hex/system/planet/planet-type";
import {System} from "src/app/models/sector-creator/hex/system/system";
import {RollingService} from "../../../../data-services/rolling.service";
import {RandomLargosianNameService} from "./random-largosian-name.service";
import {Orbit} from "../../../../../models/sector-creator/hex/system/star/orbit";
import {TradeCode} from "../../../../../models/sector-creator/hex/system/planet/trade-code";
import {Base} from "../../../../../models/sector-creator/hex/system/planet/settlement/base";
import {PlanetGenerationService} from "./planet-generation.service";
import {PlanetDesirabilityService} from "./planet-desirability.service";
import {Chemistry} from "../../../../../models/sector-creator/hex/system/planet/chemistry";
import {SpectralType} from "../../../../../models/sector-creator/hex/system/star/spectral-type";
import {Luminosity} from "../../../../../models/sector-creator/hex/system/star/luminosity";
import {PlanetOrbit} from "../../../../../models/sector-creator/hex/system/planet/planet-orbit";
import {Star} from "../../../../../models/sector-creator/hex/system/star/star";
import {SettlementType} from "../../../../../models/sector-creator/hex/system/planet/settlement/settlement-type";
import {RingType} from "../../../../../models/sector-creator/hex/system/planet/ring-type";
import {Desirability} from "../../../../../models/sector-creator/hex/system/planet/desirability";
import {SettlementService} from "./settlement-services/settlement.service";
import {PBG} from "../../../../../models/sector-creator/hex/system/planet/pbg";

@Injectable({
  providedIn: 'root'
})
export class PlanetService {

  constructor(private _rollingService: RollingService,
              private _planetGenerationService: PlanetGenerationService,
              private _planetDesirabilityService: PlanetDesirabilityService,
              private _settlementService: SettlementService,
              private _randomNameService: RandomLargosianNameService) {
  }

  getPlanetsForSystems(systems: System[]): System[] {
    for (let system of systems) {
      if (system.stars[0].orbit == Orbit.primary || system.stars[0].orbit == Orbit.distant) {
        system.planets = this.getPlanetsForSystem(system);
      }
    }
    return systems;
  }

  getPlanetsForSystem(system: System): Planet[] {
    let epistellarPlanets = this.getEpistellarPlanets(system);
    let innerPlanets = this.getInnerPlanets(system, epistellarPlanets.length);
    let outerPlanets = this.getOuterPlanets(system, innerPlanets.length);

    return epistellarPlanets.concat(innerPlanets).concat(outerPlanets);
  }

  private getEpistellarPlanets(system: System): Planet[] {
    let planets = [] as Planet[];
    let star = system.stars[0];
    if (star.spectralType == SpectralType.WhiteDwarf || star.spectralType == SpectralType.Red || star.luminosity == Luminosity.III) {
      return [] as Planet[];
    }
    let roll = this._rollingService.roll(1) - 3;

    if (star.spectralType == SpectralType.Orange && star.luminosity == Luminosity.V) {
      roll -= 1;
    }

    for (let i = 0; i < roll && i < 2; i++) {
      planets.push(this.getNewPlanet(star, PlanetOrbit.Epistellar, i + 1));
    }

    return planets;
  }

  private getInnerPlanets(system: System, position: number): Planet[] {
    let planets = [] as Planet[];
    let star = system.stars[0];
    if (system.stars.length > 1) {
      for (let star of system.stars) {
        if (star.orbit == Orbit.close) {
          return planets;
        }
      }
    }
    let roll = this._rollingService.roll(1) - 1;
    if (star.spectralType == SpectralType.Orange && star.luminosity == Luminosity.V) {
      roll -= 1;
    } else if (star.spectralType == SpectralType.Red) {
      roll = Math.ceil(roll / 2);
    }

    for (let i = 0; i < roll; i++) {
      planets.push(this.getNewPlanet(star, PlanetOrbit.Inner, position + i));
    }

    return planets;
  }

  private getOuterPlanets(system: System, position: number) {
    let planets = [] as Planet[];
    let star = system.stars[0];
    if (system.stars.length > 1) {
      for (let star of system.stars) {
        if (star.orbit == Orbit.close) {
          return planets;
        }
      }
    }
    let roll = this._rollingService.roll(1) - 1;
    if (system.stars.length > 1) {
      for (let star of system.stars) {
        if (star.orbit == Orbit.moderate) {
          return planets;
        }
      }
    }
    if ((star.spectralType == SpectralType.Orange && star.luminosity == Luminosity.V)
      || star.spectralType == SpectralType.Red) {
      roll -= 1;
    }

    for (let i = 0; i < roll; i++) {
      planets.push(this.getNewPlanet(star, PlanetOrbit.Outer, position + i));
    }
    return planets;
  }

  getNewPlanet(star: Star, orbit: PlanetOrbit, position: number): Planet {
    let planet = {} as Planet;

    planet.star = star;
    planet.orbit = orbit;
    planet.parent = {} as Planet;
    planet.position = position;
    planet.type = this.getPlanetType(star);
    planet.satellites = this.getPlanetSatellites(planet, star);
    planet.rings = this.getPlanetRings(planet.type);
    planet.type = this.getWorldType(star, planet);
    planet = this._planetGenerationService.generatePlanet(planet);
    planet.temperature = this.getTemperature(planet.atmosphere);
    planet.desirability = this._planetDesirabilityService.getDesirability(planet);
    planet.settlement = this._settlementService.getSettlement(planet);
    planet.tradeCodes = this.getPlanetTradeCodes(planet);

    return planet;
  }

  getNewJovianPlanet(star: Star, orbit: PlanetOrbit, position: number) {
    let planet = {} as Planet;

    planet.star = star;
    planet.orbit = orbit;
    planet.parent = {} as Planet;
    planet.position = position;
    planet.type = PlanetType.Jovian;
    planet.satellites = this.getPlanetSatellites(planet, star);
    planet.rings = this.getPlanetRings(planet.type);
    planet.type = this.getWorldType(star, planet);
    planet = this._planetGenerationService.generatePlanet(planet);
    planet.temperature = this.getTemperature(planet.atmosphere);
    planet.desirability = this._planetDesirabilityService.getDesirability(planet);
    planet.settlement = this._settlementService.getSettlement(planet);
    planet.tradeCodes = this.getPlanetTradeCodes(planet);

    return planet;
  }

  getCustomPlanet(allegiance: string, name: string, temperature: number, settlementType: SettlementType,
                  starport: string, size: number, atmosphere: number, hydrosphere: number, biosphere: number,
                  population: number, government: number, lawLevel: number, techLevel: number, satellites: Planet[],
                  type: PlanetType, ringType: RingType, planetOrbit: PlanetOrbit, parent: Planet, star: Star,
                  position: number, chemistry: Chemistry, terraformed: boolean, dayLength: number, orbitalPeriod: number) {
    let planet = {
      star: star,
      parent: parent,
      position: position,

      // Planet Info
      name: name,
      size: size,
      atmosphere: atmosphere,
      hydrosphere: hydrosphere,
      biosphere: biosphere,
      temperature: temperature,
      dayLength: dayLength,
      orbitalPeriod: orbitalPeriod,
      terraformed: terraformed,

      type: type,
      rings: this.getPlanetRings(type),
      orbit: planetOrbit,
      chemistry: chemistry,

      // Satellites
      satellites: satellites,

      // Settlement
      importance: 0,
      desirability: {} as Desirability,
      tradeCodes: {} as TradeCode[]
    } as Planet;

    planet.pbg = this.getPBG(planet);
    planet.settlement = this._settlementService.getSettlement(planet);
    planet.desirability = this._planetDesirabilityService.getDesirability(planet);
    planet.tradeCodes = this.getPlanetTradeCodes(planet);

    return planet;
  }

  getNewSatellite(type: PlanetType, parent: Planet, star: Star): Planet {
    let satellite = {} as Planet;

    satellite.parent = parent as Planet;
    satellite.type = type;
    satellite.orbit = parent.orbit;
    satellite.star = parent.star;
    satellite.satellites = this.getPlanetSatellites(satellite, star);
    satellite.rings = this.getPlanetRings(satellite.type);
    satellite.type = this.getWorldType(star, satellite);
    satellite = this._planetGenerationService.generatePlanet(satellite);
    satellite.desirability = this._planetDesirabilityService.getDesirability(satellite);
    satellite.tradeCodes = this.getPlanetTradeCodes(satellite);
    satellite.settlement = this._settlementService.getSettlement(satellite);

    return satellite;
  }

  private getPlanetType(star: Star): PlanetType {
    let roll = this._rollingService.roll(1);

    if (star.spectralType == SpectralType.Red) {
      roll -= 1;
    }

    switch (roll) {
      case 0:
      case 1:
        return PlanetType.AsteroidBelt;
      case 2:
        return PlanetType.Dwarf;
      case 3:
        return PlanetType.Terrestrial;
      case 4:
        return PlanetType.Helian;
      case 5:
      case 6:
        return PlanetType.Jovian;
      default:
        return PlanetType.AsteroidBelt;
    }
  }

  private getPlanetSatellites(planet: Planet, star: Star): Planet[] {
    let roll = this._rollingService.roll(1);
    let satelliteNum = 0;
    let satellites = [] as Planet[];
    switch (planet.type) {
      case PlanetType.AsteroidBelt:
        if (roll <= 4) {
          roll = this._rollingService.roll(2);
          for (let i = 0; i < roll - 2; i++) {
            satellites.push(this.getNewSatellite(PlanetType.SmallBody, planet, star));
          }
        } else {
          satellites.push(this.getNewSatellite(PlanetType.Dwarf, planet, star));

          roll = this._rollingService.roll(2);
          for (let i = 1; i < roll - 2; i++) {
            satellites.push(this.getNewSatellite(PlanetType.SmallBody, planet, star));
          }
        }
        return satellites;

      case PlanetType.Dwarf:
        if (roll <= 5) {
          return [] as Planet[];
        } else {
          return [this.getNewSatellite(PlanetType.Dwarf, planet, star)] as Planet[];
        }

      case PlanetType.Terrestrial:
        if (roll <= 4) {
          return [] as Planet[];
        } else {
          return [this.getNewSatellite(PlanetType.Dwarf, planet, star)] as Planet[];
        }

      case PlanetType.Helian:
        satelliteNum = roll - 3;
        if (satelliteNum > 0) {
          roll = this._rollingService.roll(1);
          if (roll <= 5) {
            for (let i = 0; i < satelliteNum; i++) {
              satellites.push(this.getNewSatellite(PlanetType.Dwarf, planet, star));
            }
          } else {
            satellites.push(this.getNewSatellite(PlanetType.Terrestrial, planet, star));
            for (let i = 1; i < satelliteNum; i++) {
              satellites.push(this.getNewSatellite(PlanetType.Dwarf, planet, star));
            }
          }
        }
        return satellites;

      case PlanetType.Jovian:
        satelliteNum = roll;
        satellites = [] as Planet[];
        roll = this._rollingService.roll(1);
        if (roll <= 5) {
          for (let i = 0; i < satelliteNum; i++) {
            satellites.push(this.getNewSatellite(PlanetType.Dwarf, planet, star));
          }
        } else {
          roll = this._rollingService.roll(1);
          if (roll <= 5) {
            satellites.push(this.getNewSatellite(PlanetType.Terrestrial, planet, star));
            for (let i = 1; i < satelliteNum; i++) {
              satellites.push(this.getNewSatellite(PlanetType.Dwarf, planet, star));
            }
          } else {
            satellites.push(this.getNewSatellite(PlanetType.Helian, planet, star));
            for (let i = 1; i < satelliteNum; i++) {
              satellites.push(this.getNewSatellite(PlanetType.Dwarf, planet, star));
            }
          }
        }
        return satellites;

      default:
        return satellites;
    }
  }

  private getPlanetRings(type: PlanetType) {
    if (type == PlanetType.Jovian) {
      let roll = this._rollingService.roll(1);
      if (roll <= 4) {
        return RingType.Minor;
      }
      return RingType.Complex;
    }
    return RingType.None;
  }

  private getWorldType(star: Star, planet: Planet): PlanetType {
    if (star.spectralType == SpectralType.WhiteDwarf || star.luminosity == Luminosity.III && planet.position <= star.expansionSize) {
      return this.GetExpandedPlanets(planet);
    } else if (planet.type == PlanetType.AsteroidBelt) {
      return PlanetType.AsteroidBelt;
    } else if (planet.type == PlanetType.Dwarf) {
      return this.GetDwarfPlanetType(planet);
    } else if (planet.type == PlanetType.Terrestrial) {
      return this.GetTerrestrialPlanetType(planet);
    } else if (planet.type == PlanetType.Helian) {
      return this.GetHelianPlanetType(planet);
    } else if (planet.type == PlanetType.Jovian) {
      return this.GetJovianPlanetType(planet);
    }
    return planet.type;
  }

  private GetExpandedPlanets(planet: Planet): PlanetType {
    if (planet.type == PlanetType.Dwarf) {
      return PlanetType.Stygian;
    } else if (planet.type == PlanetType.Terrestrial) {
      return PlanetType.Acheronian;
    } else if (planet.type == PlanetType.Helian) {
      return PlanetType.Asphodelian;
    } else if (planet.type == PlanetType.Jovian) {
      return PlanetType.Chthonian;
    } else {
      return PlanetType.Unknown;
    }
  }

  private GetDwarfPlanetType(planet: Planet): PlanetType {
    if (planet.orbit == PlanetOrbit.Epistellar) {
      return this.GetEpistellarDwarfPlanetType(planet);
    } else if (planet.orbit == PlanetOrbit.Inner) {
      return this.GetInnerDwarfPlanetType(planet);
    } else if (planet.orbit == PlanetOrbit.Outer) {
      return this.GetOuterDwarfPlanetType(planet);
    } else {
      return PlanetType.Unknown;
    }
  }

  private GetEpistellarDwarfPlanetType(planet: Planet) {
    let roll = this._rollingService.roll(1);
    if (planet.parent != null && planet.parent.type == PlanetType.AsteroidBelt) {
      roll -= 2;
    }
    if (roll <= 3) {
      return PlanetType.Rockball;
    } else if (4 <= roll && roll <= 5) {
      return PlanetType.Meltball;
    } else {
      roll = this._rollingService.roll(1);
      if (roll <= 4) {
        return PlanetType.Hebean;
      } else {
        return PlanetType.Promethean;
      }
    }
  }

  private GetInnerDwarfPlanetType(planet: Planet): PlanetType {
    let roll = this._rollingService.roll(1);

    if (planet.parent != null && planet.parent.type == PlanetType.AsteroidBelt) {
      roll -= 2;
    } else if (planet.parent != null && planet.parent.type == PlanetType.Helian) {
      roll += 1;
    } else if (planet.parent != null && planet.parent.type == PlanetType.Jovian) {
      roll += 2;
    }
    if (roll <= 4) {
      return PlanetType.Rockball;
    } else if (roll <= 6) {
      return PlanetType.Arean;
    } else if (roll == 7) {
      return PlanetType.Meltball;
    } else {
      roll = this._rollingService.roll(1);
      if (roll <= 4) {
        return PlanetType.Hebean;
      } else {
        return PlanetType.Promethean;
      }
    }
  }

  private GetOuterDwarfPlanetType(planet: Planet): PlanetType {
    let roll = this._rollingService.roll(1);
    if (planet.parent != null && planet.parent.type == PlanetType.AsteroidBelt) {
      roll -= 1;
    } else if (planet.parent != null && planet.parent.type == PlanetType.Helian) {
      roll += 1;
    } else if (planet.parent != null && planet.parent.type == PlanetType.Jovian) {
      roll += 2;
    }

    if (roll == 0) {
      return PlanetType.Rockball;
    } else if (roll <= 4) {
      return PlanetType.Snowball;
    } else if (roll <= 6) {
      return PlanetType.Rockball;
    } else if (roll == 7) {
      return PlanetType.Meltball;
    } else {
      roll = this._rollingService.roll(1);
      if (roll <= 3) {
        return PlanetType.Hebean;
      } else if (roll <= 5) {
        return PlanetType.Arean;
      } else {
        return PlanetType.Promethean;
      }
    }
  }

  private GetTerrestrialPlanetType(planet: Planet) {
    if (planet.orbit == PlanetOrbit.Epistellar) {
      return this.GetEpistellarTerrestrialPlanetType();
    } else if (planet.orbit == PlanetOrbit.Inner) {
      return this.GetInnerTerrestrialPlanetType();
    } else if (planet.orbit == PlanetOrbit.Outer) {
      return this.GetOuterTerrestrialPlanetType(planet);
    } else {
      return PlanetType.Unknown;
    }
  }

  private GetEpistellarTerrestrialPlanetType() {
    let roll = this._rollingService.roll(1);
    if (roll <= 4) {
      return PlanetType.JaniLithic;
    } else if (roll == 5) {
      return PlanetType.Vesperian;
    } else {
      return PlanetType.Telluric;
    }
  }

  private GetInnerTerrestrialPlanetType() {
    let roll = this._rollingService.roll(2);

    if (roll <= 4 || roll >= 11) {
      return PlanetType.Telluric;
    } else if (roll <= 6) {
      return PlanetType.Arid;
    } else if (roll == 7 || roll == 10) {
      return PlanetType.Tectonic;
    } else {
      return PlanetType.Oceanic;
    }
  }

  private GetOuterTerrestrialPlanetType(planet: Planet): PlanetType {
    let roll = this._rollingService.roll(1);
    if (planet.parent != null) {
      roll += 2;
    }

    if (roll <= 4) {
      return PlanetType.Arid;
    } else if (roll <= 6) {
      return PlanetType.Tectonic;
    } else {
      return PlanetType.Oceanic;
    }
  }

  private GetHelianPlanetType(planet: Planet): PlanetType {
    if (planet.orbit == PlanetOrbit.Epistellar) {
      return this.GetEpistellarHelianPlanetType();
    } else if (planet.orbit == PlanetOrbit.Inner) {
      return this.GetInnerHelianPlanetType();
    } else if (planet.orbit == PlanetOrbit.Outer) {
      return PlanetType.Helian;
    } else {
      return PlanetType.Unknown;
    }
  }

  private GetEpistellarHelianPlanetType(): PlanetType {
    let roll = this._rollingService.roll(1);

    if (roll <= 5) {
      return PlanetType.Helian;
    } else {
      return PlanetType.Asphodelian;
    }
  }

  private GetInnerHelianPlanetType(): PlanetType {
    let roll = this._rollingService.roll(1);

    if (roll <= 4) {
      return PlanetType.Helian;
    } else {
      return PlanetType.Panthalassic;
    }
  }

  private GetJovianPlanetType(planet: Planet) {
    let roll = this._rollingService.roll(1);
    if (planet.orbit == PlanetOrbit.Epistellar && roll >= 6) {
      return PlanetType.Chthonian;
    } else {
      return PlanetType.Jovian;
    }
  }

  private getTemperature(atmosphere: number) {
    let roll = this._rollingService.roll(2);
    let mod = 0;

    if ((0 <= atmosphere && atmosphere <= 1)
      || (6 <= atmosphere && atmosphere <= 7)) {
      return roll;
    } else if (2 <= atmosphere && atmosphere <= 3) {
      mod -= 2;
    } else if ((4 <= atmosphere && atmosphere <= 5) || atmosphere == 14) {
      mod -= 1;
    } else if (8 <= atmosphere && atmosphere <= 9) {
      mod += 1;
    } else if (atmosphere == 10 || atmosphere == 13 || atmosphere == 15) {
      mod += 2;
    } else if (11 <= atmosphere && atmosphere <= 12) {
      mod += 6;
    }

    if (this._rollingService.roll(2) >= 12) {
      switch (Math.round(Math.random())) {
        case 0:
          mod -= 4;
          break;
        case 1:
          mod += 4
          break;
      }
    }

    let temperature = roll + mod;
    return Math.min(Math.max(temperature, 0), 15);
  }

  getRemarks(planet: Planet) {
    let remarks = [];

    if (planet.settlement.bases.includes(Base.dataRepository)) {
      remarks.push(TradeCode.DataRepository)
    }

    if ((4 <= planet.atmosphere && planet.atmosphere <= 9)
      && (4 <= planet.hydrosphere && planet.hydrosphere <= 8)
      && (5 <= planet.settlement.population && planet.settlement.population <= 7)) {
      remarks.push(TradeCode.Agricultural);
    }

    if (planet.settlement.bases.includes(Base.ancientSite)) {
      remarks.push(TradeCode.AncientSite)
    }

    if (planet.size == 0) {
      remarks.push(TradeCode.AsteroidBelt);
    }

    if (planet.settlement.population == 0 && planet.settlement.government == 0 && planet.settlement.techLevel == 0) {
      remarks.push(TradeCode.Barren);
    }

    if ((planet.size < 6 || planet.size > 9)
      && (planet.atmosphere < 4 || planet.atmosphere > 9)
      && (planet.hydrosphere < 3 || planet.hydrosphere > 7)
      && (3 <= planet.temperature && planet.temperature <= 4)) {
      remarks.push(TradeCode.Cold);
    }

    if (planet.settlement.isCapitol) {
      remarks.push(TradeCode.Capitol);
    }

    if ((2 <= planet.atmosphere && planet.atmosphere <= 9)
      && (planet.hydrosphere == 0)) {
      remarks.push(TradeCode.Desert);
    }

    if (planet.settlement.population == 0 && planet.settlement.government == 0 && planet.settlement.lawLevel == 0 && planet.settlement.techLevel >= 1) {
      remarks.push(TradeCode.Dieback);
    }

    if (10 <= planet.atmosphere && planet.atmosphere <= 12 && planet.hydrosphere >= 1) {
      remarks.push(TradeCode.FluidOceans);
    }

    if ((2 <= planet.size && planet.size <= 9)
      && planet.hydrosphere >= 1 && planet.temperature <= 2) {
      remarks.push(TradeCode.Frozen);
    }

    if (this.isGarden(planet)) {
      remarks.push(TradeCode.Garden);
    }

    if ((planet.size >= 3)
      && (planet.atmosphere == 2 || planet.atmosphere == 4 || planet.atmosphere == 7 || (9 <= planet.atmosphere && planet.atmosphere <= 12))
      && (planet.hydrosphere <= 2)) {
      remarks.push(TradeCode.Hellworld);
    }

    if (planet.settlement.population >= 9) {
      remarks.push(TradeCode.HighPopulation);
    }

    if (((planet.size < 6 || planet.size > 9)
        || (planet.atmosphere < 4 || planet.atmosphere > 9)
        || (planet.hydrosphere < 3 || planet.hydrosphere > 7))
      && planet.temperature >= 10) {
      remarks.push(TradeCode.Hot);
    }

    if (planet.settlement.techLevel >= 12) {
      remarks.push(TradeCode.HighTech);
    }

    if ((0 <= planet.atmosphere && planet.atmosphere <= 1)
      && planet.hydrosphere >= 1) {
      remarks.push(TradeCode.IceCapped);
    }

    if ((planet.atmosphere == 0 || planet.atmosphere == 1 || planet.atmosphere == 2 || planet.atmosphere == 4 || planet.atmosphere == 7 || (9 <= planet.atmosphere && planet.atmosphere <= 12))
      && (planet.settlement.population >= 9)) {
      remarks.push(TradeCode.Industrial);
    }

    if (planet.type == PlanetType.JaniLithic || planet.type == PlanetType.Vesperian) {
      remarks.push(TradeCode.TidallyLocked);
    }

    if (1 <= planet.settlement.population && planet.settlement.population <= 3) {
      remarks.push(TradeCode.LowPopulation);
    }

    if (planet.settlement.techLevel <= 5 && planet.settlement.population >= 1) {
      remarks.push(TradeCode.LowTech);
    }

    if (planet.settlement.government == 6 && planet.settlement.bases.includes(Base.prison)) {
      remarks.push(TradeCode.MilitaryRule);
    }

    if (planet.atmosphere <= 3 && planet.hydrosphere <= 3 && planet.settlement.population >= 6) {
      remarks.push(TradeCode.NonAgricultural);
    }

    if (4 <= planet.settlement.population && planet.settlement.population <= 6) {
      remarks.push(TradeCode.NonIndustrial);
    }

    if (planet.size >= 10 && planet.hydrosphere == 10) {
      remarks.push(TradeCode.Ocean);
    }

    if ((2 <= planet.atmosphere && planet.atmosphere <= 5)
      && planet.hydrosphere <= 3) {
      remarks.push(TradeCode.Poor);
    }

    if (planet.settlement.government == 6 && planet.settlement.bases.includes(Base.prison)) {
      remarks.push(TradeCode.PrisonCamp);
    }

    if (planet.settlement.government == 6 && planet.settlement.bases.includes(Base.naturePreserve)) {
      remarks.push(TradeCode.Reserve);
    }

    if ((planet.atmosphere == 6 || planet.atmosphere == 8)
      && (6 <= planet.settlement.population && planet.settlement.population <= 8)) {
      remarks.push(TradeCode.Rich);
    }

    if (planet.settlement.bases.includes(Base.researchStation)) {
      remarks.push(TradeCode.ResearchStation);
    }

    if (planet.parent != null && planet.type != PlanetType.AsteroidBelt) {
      remarks.push(TradeCode.Satellite);
    }

    if (planet.biosphere == 0) {
      remarks.push(TradeCode.Sterile);
    }

    if ((6 <= planet.size && planet.size <= 9)
      && (4 <= planet.atmosphere && planet.atmosphere <= 9)
      && (3 <= planet.hydrosphere && planet.hydrosphere <= 7)
      && (planet.temperature >= 10)) {
      remarks.push(TradeCode.Tropical);
    }

    if ((6 <= planet.size && planet.size <= 9)
      && (4 <= planet.atmosphere && planet.atmosphere <= 9)
      && (3 <= planet.hydrosphere && planet.hydrosphere <= 7)
      && (planet.temperature <= 4)) {
      remarks.push(TradeCode.Tundra);
    }

    if (planet.type == PlanetType.Vesperian) {
      remarks.push(TradeCode.TwilightZone)
    }

    if (planet.atmosphere == 0) {
      remarks.push(TradeCode.Vacuum);
    }

    if (this.isWaterWorld(planet)) {
      remarks.push(TradeCode.WaterWorld);
    }

    if (planet.biosphere >= 7) {
      remarks.push(TradeCode.Zoo);
    }

    return remarks;
  }

  private isGarden(planet: Planet): boolean {
    return (6 <= planet.size && planet.size <= 8)
      && (planet.atmosphere == 5 || planet.atmosphere == 6 || planet.atmosphere == 8)
      && (5 <= planet.hydrosphere && planet.hydrosphere <= 7);
  }

  private isWaterWorld(planet: Planet) {
    return (3 <= planet.size && planet.size <= 9)
      && (3 <= planet.atmosphere && planet.atmosphere <= 9)
      && planet.hydrosphere == 10;
  }

  private getPlanetTradeCodes(planet: Planet): TradeCode[] {
    let codes = [] as TradeCode[];

    if (4 <= planet.atmosphere && planet.atmosphere <= 9
      && 4 <= planet.hydrosphere && planet.hydrosphere <= 8
      && 5 <= planet.settlement.population && planet.settlement.population <= 7) {
      codes.push(TradeCode.Agricultural);
    }

    if (planet.type == PlanetType.AsteroidBelt) {
      codes.push(TradeCode.AsteroidBelt);
    }

    if (2 <= planet.atmosphere && planet.atmosphere <= 13 && planet.hydrosphere == 0) {
      codes.push(TradeCode.Desert);
    }

    if ((planet.atmosphere >= 10 || (planet.chemistry != null && planet.chemistry != Chemistry.Water))
      && (1 <= planet.hydrosphere && planet.hydrosphere <= 12)) {
      codes.push(TradeCode.FluidOceans);
    }

    if (5 <= planet.size && planet.size <= 10
      && 4 <= planet.atmosphere && planet.atmosphere <= 9
      && 4 <= planet.hydrosphere && planet.hydrosphere <= 8) {
      codes.push(TradeCode.Garden);
    }

    if (planet.settlement.population >= 9) {
      codes.push(TradeCode.HighPopulation);
    }

    if (planet.settlement.industry >= 12) {
      codes.push(TradeCode.HighTech);
    }

    if (0 <= planet.atmosphere && planet.atmosphere <= 1
      && planet.hydrosphere >= 1) {
      codes.push(TradeCode.IceCapped);
    }

    if (planet.settlement.population >= 9 && planet.settlement.industry >= 6) {
      codes.push(TradeCode.Industrial);
    }

    if (1 <= planet.settlement.population && planet.settlement.population <= 3) {
      codes.push(TradeCode.LowPopulation);
    }

    if (planet.settlement.industry <= 6) {
      codes.push(TradeCode.LowTech);
    }

    if (2 <= planet.atmosphere && planet.atmosphere <= 5
      && 0 <= planet.hydrosphere && planet.hydrosphere <= 3) {
      codes.push(TradeCode.Poor);
    }

    if ((planet.atmosphere == 6 || planet.atmosphere == 8)
      && 6 <= planet.settlement.population && planet.settlement.population <= 8) {
      codes.push(TradeCode.Rich);
    }

    if (planet.biosphere == 0) {
      codes.push(TradeCode.Sterile);
    }

    if (planet.atmosphere >= 2
      && (planet.hydrosphere == 10 || planet.hydrosphere == 11)) {
      codes.push(TradeCode.WaterWorld);
    }

    if (planet.atmosphere == 0) {
      codes.push(TradeCode.Vacuum);
    }

    if (planet.biosphere >= 7) {
      codes.push(TradeCode.Zoo);
    }

    return codes;
  }

  getPBG(planet: Planet) {
    let pbg = {} as PBG;
    pbg.population = this._rollingService.rollBenford();
    pbg.belts = 0;
    pbg.giants = 0;

    for (let systemPlanet of planet.star.system.planets) {
      if (planet.type == PlanetType.AsteroidBelt) {
        pbg.belts++;
      } else if (planet.type == PlanetType.Jovian) {
        pbg.giants++;
      }
    }

    return pbg;
  }

  finishPlanet(planet: Planet) {
    planet.pbg = this.getPBG(planet);
    planet.tradeCodes = this.getRemarks(planet);
    planet.importance = this._settlementService.getImportance(planet);
    planet.settlement.economics = this._settlementService.getEconomics(planet);
    planet.settlement.culture = this._settlementService.getCulture(planet);
  }
}
