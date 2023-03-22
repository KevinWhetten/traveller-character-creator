import {Injectable} from '@angular/core';
import {Race} from '../../../../models/sector-creator/race/race';
import {StarType} from "../../../../models/sector-creator/hex/system/star/star-type";
import {Orbit} from "../../../../models/sector-creator/hex/system/star/orbit";
import {PlanetType} from "../../../../models/sector-creator/hex/system/planet/planet-type";
import {Luminosity} from "../../../../models/sector-creator/hex/system/star/luminosity";
import {SettlementType} from "../../../../models/sector-creator/hex/system/planet/settlement/settlement-type";
import {PlanetOrbit} from "../../../../models/sector-creator/hex/system/planet/planet-orbit";
import {System} from "../../../../models/sector-creator/hex/system/system";
import {Chemistry} from "../../../../models/sector-creator/hex/system/planet/chemistry";
import {Star} from "../../../../models/sector-creator/hex/system/star/star";
import {SpectralType} from "../../../../models/sector-creator/hex/system/star/spectral-type";
import {Planet} from "../../../../models/sector-creator/hex/system/planet/planet";
import {RingType} from "../../../../models/sector-creator/hex/system/planet/ring-type";
import {Races} from "../../../../models/sector-creator/race/races";
import {PlanetService} from "./planet-services/planet.service";
import {StarService} from "./star-services/star.service";
import {RollingService} from "../../../data-services/rolling.service";

@Injectable({
  providedIn: 'root'
})
export class SystemService {

  constructor(private _rollingService: RollingService, private _starService: StarService, private _planetService: PlanetService) { }

  createHomeworldSystem(race: Race): System {
    switch (race.name) {
      case "humans":
        return this.createHumanHomeworldSystem();
      case "aslan":
        return this.createAslanHomeworldSystem();
      case "mannu":
        return this.createMannuHomeworldSystem();
      case "largosians":
        return this.createLargosiansHomeworldSystem();
      case "tortosians":
        return this.createTortosianHomeworldSystem();
      case "ithromir":
        return this.createIthromirHomeworldSystem();
      case "chrotos":
        return this.createChrotosHomeworldSystem();
      case "theCollective":
        return this.createTheCollectiveHomeworldSystem();
      case "kaSara":
        return this.createKaSaraHomeworldSystem();
      case "scrappers":
        return this.createScrapperHomeworldSystem();
      case "vargr":
        return this.createVargrHomeworldSystem();
      default:
        return {} as System;
    }
  }

  createSystems(): System[] {
    let systems = [] as System[];

    this.getBrownDwarfSystem(systems);
    this.GetPrimarySystems(systems);

    this._planetService.getPlanetsForSystems(systems);

    return systems;
  }

  private getBrownDwarfSystem(systems: System[]) {
    if (this._rollingService.roll(1) <= 3) {
      let system = {stars: [this._starService.getNewBrownDwarfStar()]} as System;
      systems.push(system);
    }
  }

  private GetPrimarySystems(systems: System[]) {
    if (this._rollingService.roll(1) <= 3) {
      let system = {stars: [this._starService.GetNewPrimaryStar()]} as System;
      this.AddCompanionStars(systems, system);
      systems.push(system);
    }
  }

  private AddCompanionStars(systems: System[], system: System) {
    let systemRoll = this._rollingService.roll(3);
    if (systemRoll >= 11) {
      this.AddCompanionStar(systems, system);
    }
    if (systemRoll >= 16) {
      this.AddCompanionStar(systems, system)
    }
  }

  private AddCompanionStar(systems: System[], system: System) {
    let star = this._starService.GetNewCompanionStar();

    if (star.orbit == Orbit.distant) {
      systems.push({stars: [star]} as System);
    } else {
      system.stars.push(star);
    }
  }

  private createHumanHomeworldSystem(): System {
    let race = Races.Human;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let sun = {
      type: StarType.primary,
      spectralType: SpectralType.Yellow,
      spectrum: 0,
      luminosity: Luminosity.V,
      orbit: Orbit.primary,
      expansionSize: 0,
      age: 4
    } as Star;
    system.stars.push(sun);

    let mercury = this._planetService.getCustomPlanet(race.allegianceCode, "Mercury", 10, SettlementType.Uninhabited,
      "X", 10, 4, 2, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, {} as Planet, sun, 1, Chemistry.None,
      false, 4104.00, 87.97);

    let venus = this._planetService.getCustomPlanet(race.allegianceCode, "Venus", 12, SettlementType.Uninhabited,
      "X", 8, 13, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Telluric, RingType.None, PlanetOrbit.Inner, {} as Planet, sun, 2, Chemistry.None,
      false, 2802.00, 244.70);

    let earth = this._planetService.getCustomPlanet(race.allegianceCode, "Earth", 7, SettlementType.Homeworld,
      "A", 8, 6, 7, 15, 8, 4, 1, 11,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Inner, {} as Planet, sun, 3, Chemistry.Water,
      false, 24, 365.26);
    let luna = this._planetService.getCustomPlanet(race.allegianceCode, "Luna", 7, SettlementType.Colony,
      "B", 2, 2, 1, 0, 5, 4, 1, 11,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, earth, sun, 3, Chemistry.None,
      true, 655.72, 27.32);
    earth.satellites = [luna];
    earth.settlement.isCapitol = true;

    let mars = this._planetService.getCustomPlanet(race.allegianceCode, "Mars", 3, SettlementType.Colony,
      "B", 5, 3, 1, 0, 6, 4, 1, 11,
      [], PlanetType.Arid, RingType.None, PlanetOrbit.Inner, {} as Planet, sun, 4, Chemistry.Water,
      true, 24.66, 686.98);
    let phobos = this._planetService.getCustomPlanet(race.allegianceCode, "Phobos", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.SmallBody, RingType.None, PlanetOrbit.Inner, mars, sun, 4, Chemistry.None,
      false, 7.65, .32);
    let deimos = this._planetService.getCustomPlanet(race.allegianceCode, "Deimos", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.SmallBody, RingType.None, PlanetOrbit.Inner, mars, sun, 4, Chemistry.None,
      false, 30.31, 1.26);
    mars.satellites = [phobos, deimos];

    let asteroidBelt = this._planetService.getCustomPlanet(race.allegianceCode, "Asteroid Belt", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.AsteroidBelt, RingType.None, PlanetOrbit.Inner, {} as Planet, sun, 5, Chemistry.None,
      false, 0, 0);
    let ceres = this._planetService.getCustomPlanet(race.allegianceCode, "Ceres", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.SmallBody, RingType.None, PlanetOrbit.Inner, asteroidBelt, sun, 5, Chemistry.None,
      false, 9.07, 1680);
    let vesta = this._planetService.getCustomPlanet(race.allegianceCode, "Vesta", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.SmallBody, RingType.None, PlanetOrbit.Inner, asteroidBelt, sun, 5, Chemistry.None,
      false, 5.34, 1325.75);
    let pallas = this._planetService.getCustomPlanet(race.allegianceCode, "Pallas", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.SmallBody, RingType.None, PlanetOrbit.Inner, asteroidBelt, sun, 5, Chemistry.None,
      false, 7.81, 1684.9);
    let hygiea = this._planetService.getCustomPlanet(race.allegianceCode, "Hygeia", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.SmallBody, RingType.None, PlanetOrbit.Inner, asteroidBelt, sun, 5, Chemistry.None,
      false, 13.83, 2033.8);
    let euphrosyne = this._planetService.getCustomPlanet(race.allegianceCode, "Euphrosyne", 2, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.SmallBody, RingType.None, PlanetOrbit.Inner, asteroidBelt, sun, 5, Chemistry.None,
      false, 5.53, 2041.59);
    asteroidBelt.satellites = [ceres, vesta, pallas, hygiea, euphrosyne];

    let jupiter = this._planetService.getCustomPlanet(race.allegianceCode, "Jupiter", 2, SettlementType.Uninhabited,
      "X", 15, 15, 15, 0, 0, 0, 0, 0,
      [], PlanetType.Jovian, RingType.Minor, PlanetOrbit.Outer, {} as Planet, sun, 6, Chemistry.Water,
      false, 9.93, 4333);
    let io = this._planetService.getCustomPlanet(race.allegianceCode, "Io", 10, SettlementType.Uninhabited,
      "X", 2, 1, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Hebean, RingType.None, PlanetOrbit.Outer, jupiter, sun, 6, Chemistry.Sulfur,
      false, 42.48, 1.77);
    let europa = this._planetService.getCustomPlanet(race.allegianceCode, "Europa", 2, SettlementType.Uninhabited,
      "X", 2, 0, 5, 1, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, jupiter, sun, 6, Chemistry.Water,
      false, 85.20, 3.55);
    let ganymede = this._planetService.getCustomPlanet(race.allegianceCode, "Ganymede", 2, SettlementType.Uninhabited,
      "X", 3, 0, 9, 1, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, jupiter, sun, 6, Chemistry.Water,
      false, 171.60, 7.15);
    let callisto = this._planetService.getCustomPlanet(race.allegianceCode, "Callisto", 2, SettlementType.Uninhabited,
      "X", 3, 0, 2, 0, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, jupiter, sun, 6, Chemistry.Ammonia,
      false, 400.56, 16.69);
    jupiter.satellites = [io, europa, ganymede, callisto];

    let saturn = this._planetService.getCustomPlanet(race.allegianceCode, "Saturn", 2, SettlementType.Uninhabited,
      "X", 15, 15, 15, 0, 0, 0, 0, 0,
      [], PlanetType.Jovian, RingType.Complex, PlanetOrbit.Outer, {} as Planet, sun, 7, Chemistry.Ammonia,
      false, 10.51, 10759);
    let titan = this._planetService.getCustomPlanet(race.allegianceCode, "Titan", 4, SettlementType.Outpost,
      "E", 3, 6, 4, 2, 2, 2, 1, 11,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Outer, saturn, sun, 7, Chemistry.Methane,
      false, 383, 15.95);
    let rhea = this._planetService.getCustomPlanet(race.allegianceCode, "Rhea", 2, SettlementType.Uninhabited,
      "X", 1, 1, 10, 0, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, saturn, sun, 7, Chemistry.Water,
      false, 108, 4.52);
    let enceladus = this._planetService.getCustomPlanet(race.allegianceCode, "Enceladus", 0, SettlementType.Uninhabited,
      "X", 0, 0, 0, 1, 0, 0, 0, 0,
      [], PlanetType.Hebean, RingType.None, PlanetOrbit.Outer, saturn, sun, 7, Chemistry.Water,
      false, 33, 1.37);
    saturn.satellites = [titan, rhea, enceladus];

    let uranus = this._planetService.getCustomPlanet(race.allegianceCode, "Uranus", 2, SettlementType.Uninhabited,
      "X", 15, 15, 15, 0, 0, 0, 0, 0,
      [], PlanetType.Jovian, RingType.Minor, PlanetOrbit.Outer, {} as Planet, sun, 8, Chemistry.Methane,
      false, 17.23, 30688);
    let miranda = this._planetService.getCustomPlanet(race.allegianceCode, "Miranda", 0, SettlementType.Uninhabited,
      "X", 0, 0, 10, 0, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, uranus, sun, 8, Chemistry.Water,
      false, 34, 1.41);
    let ariel = this._planetService.getCustomPlanet(race.allegianceCode, "Ariel", 0, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, uranus, sun, 8, Chemistry.Water,
      false, 60, 2.52);
    let umbriel = this._planetService.getCustomPlanet(race.allegianceCode, "Umbriel", 0, SettlementType.Uninhabited,
      "X", 0, 0, 10, 0, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, uranus, sun, 8, Chemistry.Water,
      false, 99, 4.14);
    let titania = this._planetService.getCustomPlanet(race.allegianceCode, "Titania", 0, SettlementType.Uninhabited,
      "X", 1, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, uranus, sun, 8, Chemistry.Ammonia,
      false, 209, 8.70);
    let oberon = this._planetService.getCustomPlanet(race.allegianceCode, "Oberon", 0, SettlementType.Uninhabited,
      "X", 1, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, uranus, sun, 8, Chemistry.Ammonia,
      false, 323, 13.46);
    uranus.satellites = [miranda, ariel, umbriel, titania, oberon];

    let neptune = this._planetService.getCustomPlanet(race.allegianceCode, "Neptune", 2, SettlementType.Uninhabited,
      "X", 15, 15, 15, 0, 0, 0, 0, 0,
      [], PlanetType.Jovian, RingType.Minor, PlanetOrbit.Outer, {} as Planet, sun, 9, Chemistry.Water,
      false, 16.11, 60195);
    let triton = this._planetService.getCustomPlanet(race.allegianceCode, "Triton", 0, SettlementType.Uninhabited,
      "X", 1, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, neptune, sun, 9, Chemistry.Water,
      false, 141, 5.87);
    neptune.satellites = [triton];

    let pluto = this._planetService.getCustomPlanet(race.allegianceCode, "Pluto", 0, SettlementType.Uninhabited,
      "X", 1, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, neptune, sun, 9, Chemistry.Water,
      false, 153, 90560);
    let charon = this._planetService.getCustomPlanet(race.allegianceCode, "Charon", 0, SettlementType.Uninhabited,
      "X", 1, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, neptune, sun, 9, Chemistry.Water,
      false, 153, 6.39);
    pluto.satellites = [charon];

    system.planets.push(mercury);
    system.planets.push(venus);
    system.planets.push(earth);
    system.planets.push(mars);
    system.planets.push(asteroidBelt);
    system.planets.push(jupiter);
    system.planets.push(saturn);
    system.planets.push(uranus);
    system.planets.push(neptune);
    system.planets.push(pluto);

    system = this.finishPlanets(system);

    return system;
  }

  private createAslanHomeworldSystem(): System {
    let race = Races.Aslan;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let tyeyo = {
      type: StarType.primary,
      spectralType: SpectralType.Yellow,
      spectrum: 4,
      luminosity: Luminosity.V,
      orbit: Orbit.primary,
      expansionSize: 0,
      age: 4
    } as Star;
    let saietaie = {
      type: StarType.companion,
      spectralType: SpectralType.WhiteDwarf,
      luminosity: Luminosity.D,
      orbit: Orbit.close,
      expansionSize: 0,
      age: 6
    } as Star;
    system.stars.push(tyeyo);
    system.stars.push(saietaie);

    let planet1 = this._planetService.getNewPlanet(tyeyo, PlanetOrbit.Epistellar, 1);

    let kusyu = this._planetService.getCustomPlanet(race.allegianceCode, "Kusyu", 3, SettlementType.Homeworld,
      "A", 8, 7, 6, 15, 9, 7, 6, 10,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Inner, {} as Planet, tyeyo, 2, Chemistry.Water,
      false, 36.04, 320.02);
    let aoshi = this._planetService.getCustomPlanet(race.allegianceCode, "Aoshi", 0, SettlementType.Colony,
      "C", 0, 2, 1, 0, 3, 7, 6, 10,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, kusyu, tyeyo, 2, Chemistry.Water,
      false, 30.24, 1.26);
    let aokaah = this._planetService.getCustomPlanet(race.allegianceCode, "Aoka'ah", 10, SettlementType.Uninhabited,
      "X", 0, 11, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Hebean, RingType.None, PlanetOrbit.Inner, kusyu, tyeyo, 2, Chemistry.Methane,
      false, 512.40, 21.35);
    kusyu.satellites = [aoshi, aokaah];
    kusyu.settlement.isCapitol = true;

    let planet3 = this._planetService.getNewPlanet(tyeyo, PlanetOrbit.Inner, 1);
    let planet4 = this._planetService.getNewPlanet(tyeyo, PlanetOrbit.Inner, 1);
    let planet5 = this._planetService.getNewPlanet(tyeyo, PlanetOrbit.Inner, 1);
    let planet6 = this._planetService.getNewPlanet(tyeyo, PlanetOrbit.Inner, 1);
    let planet7 = this._planetService.getNewJovianPlanet(tyeyo, PlanetOrbit.Inner, 1);
    let planet8 = this._planetService.getNewJovianPlanet(tyeyo, PlanetOrbit.Inner, 1);
    let planet9 = this._planetService.getNewJovianPlanet(tyeyo, PlanetOrbit.Inner, 1);

    system.planets.push(planet1);
    system.planets.push(kusyu);
    system.planets.push(planet3);
    system.planets.push(planet4);
    system.planets.push(planet5);
    system.planets.push(planet6);
    system.planets.push(planet7);
    system.planets.push(planet8);
    system.planets.push(planet9);

    system = this.finishPlanets(system);

    return system;
  }

  private createMannuHomeworldSystem(): System {
    let race = Races.Mannu;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.primary,
      spectralType: SpectralType.Orange,
      spectrum: 3,
      luminosity: Luminosity.IV,
      orbit: Orbit.primary,
      expansionSize: 2,
      age: 6
    } as Star;

    system.stars.push(star);

    let planet1 = this._planetService.getNewPlanet(star, PlanetOrbit.Epistellar, 1);
    let planet2 = this._planetService.getNewPlanet(star, PlanetOrbit.Epistellar, 2);
    let planet3 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 3);

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Mannus", 5, SettlementType.Homeworld,
      "A", 6, 8, 8, 15, 6, 14, 15, 11,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Inner, {} as Planet, star, 4, Chemistry.Water,
      false, 64.31, 685.12);
    let satellite1 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite2 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite3 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    planet.satellites = [satellite1, satellite2, satellite3];
    planet.settlement.isCapitol = true;

    let planet4 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 5);
    let planet5 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 6);
    let planet6 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 7);
    let planet7 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 8);
    let planet8 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 9);

    system.planets.push(planet1);
    system.planets.push(planet2);
    system.planets.push(planet3);
    system.planets.push(planet);
    system.planets.push(planet4);
    system.planets.push(planet5);
    system.planets.push(planet6);
    system.planets.push(planet7);
    system.planets.push(planet8);

    system = this.finishPlanets(system);

    return system;
  }

  private createLargosiansHomeworldSystem(): System {
    let race = Races.Largosians;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.primary,
      spectralType: SpectralType.White,
      spectrum: 1,
      luminosity: Luminosity.IV,
      orbit: Orbit.primary,
      expansionSize: 2,
      age: 2
    } as Star;

    system.stars.push(star);
    system.stars.push(this._starService.GetNewCompanionStar());

    let planet1 = this._planetService.getNewPlanet(star, PlanetOrbit.Epistellar, 1);
    let planet2 = this._planetService.getNewPlanet(star, PlanetOrbit.Epistellar, 2);
    let planet3 = this._planetService.getNewPlanet(star, PlanetOrbit.Epistellar, 3);

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Largo", 11, SettlementType.Homeworld,
      "A", 5, 7, 2, 15, 10, 1, 4, 10,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Epistellar, {} as Planet, star, 4, Chemistry.Water,
      false, 64.31, 685.12);
    planet.settlement.isCapitol = true;

    let planet4 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 5);
    let planet5 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 6);
    let planet6 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 7);
    let planet7 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 8);
    let planet8 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 9);
    let planet9 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 10);
    let planet10 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 11);
    let planet11 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 12);
    let planet12 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 13);
    let planet13 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 14);
    let planet14 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 15);
    let planet15 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 16);
    let planet16 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 17);
    let planet17 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 18);

    system.planets.push(planet1);
    system.planets.push(planet2);
    system.planets.push(planet3);
    system.planets.push(planet);
    system.planets.push(planet4);
    system.planets.push(planet5);
    system.planets.push(planet6);
    system.planets.push(planet7);
    system.planets.push(planet8);
    system.planets.push(planet9);
    system.planets.push(planet10);
    system.planets.push(planet11);
    system.planets.push(planet12);
    system.planets.push(planet13);
    system.planets.push(planet14);
    system.planets.push(planet15);
    system.planets.push(planet16);
    system.planets.push(planet17);

    system = this.finishPlanets(system);

    return system;
  }

  private createTortosianHomeworldSystem(): System {
    let race = Races.Tortosians;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.primary,
      spectralType: SpectralType.White,
      spectrum: 1,
      luminosity: Luminosity.IV,
      orbit: Orbit.primary,
      expansionSize: 2,
      age: 2
    } as Star;

    system.stars.push(star);
    system.stars.push(this._starService.GetNewCompanionStar());

    let planet1 = this._planetService.getNewPlanet(star, PlanetOrbit.Epistellar, 1);

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Tort", 9, SettlementType.Homeworld,
      "A", 4, 9, 6, 15, 9, 8, 7, 9,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Epistellar, {} as Planet, star, 2, Chemistry.Water,
      false, 64.31, 685.12);
    let satellite1 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite2 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    planet.satellites = [satellite1, satellite2];
    planet.settlement.isCapitol = true;

    let planet2 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 3);
    let planet3 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 4);
    let planet4 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 5);
    let planet5 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 6);
    let planet6 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 7);
    let planet7 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 8);
    let planet8 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 9);
    let planet9 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 10);

    system.planets.push(planet1);
    system.planets.push(planet);
    system.planets.push(planet2);
    system.planets.push(planet3);
    system.planets.push(planet4);
    system.planets.push(planet5);
    system.planets.push(planet6);
    system.planets.push(planet7);
    system.planets.push(planet8);
    system.planets.push(planet9);

    system = this.finishPlanets(system);

    return system;
  }

  private createIthromirHomeworldSystem(): System {
    let race = Races.Ithromir;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.primary,
      spectralType: SpectralType.YellowWhite,
      spectrum: 4,
      luminosity: Luminosity.V,
      orbit: Orbit.primary,
      expansionSize: 3,
      age: 3
    } as Star;

    system.stars.push(star);
    system.stars.push(this._starService.GetNewCompanionStar());
    system.stars.push(this._starService.GetNewCompanionStar());

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Ithros", 9, SettlementType.Homeworld,
      "A", 10, 13, 10, 15, 13, 12, 12, 13,
      [], PlanetType.Oceanic, RingType.Complex, PlanetOrbit.Inner, {} as Planet, star, 1, Chemistry.Water,
      false, 45.68, 541.27);
    let satellite1 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite2 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite3 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite4 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite5 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite6 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite7 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite8 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    let satellite9 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    planet.satellites = [satellite1, satellite2, satellite3, satellite4, satellite5, satellite6, satellite7, satellite8, satellite9];
    planet.settlement.isCapitol = true;

    let planet1 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 2);
    let planet2 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 3);
    let planet3 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 4);
    let planet4 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 5);
    let planet5 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 6);
    let planet6 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 7);
    let planet7 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 8);
    let planet8 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 9);
    let planet9 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 10);
    let planet10 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 11);
    let planet11 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 12);
    let planet12 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 13);

    system.planets.push(planet);
    system.planets.push(planet1);
    system.planets.push(planet2);
    system.planets.push(planet3);
    system.planets.push(planet4);
    system.planets.push(planet5);
    system.planets.push(planet6);
    system.planets.push(planet7);
    system.planets.push(planet8);
    system.planets.push(planet9);
    system.planets.push(planet10);
    system.planets.push(planet11);
    system.planets.push(planet12);

    system = this.finishPlanets(system);

    return system;
  }

  private createChrotosHomeworldSystem(): System {
    let race = Races.Chrotos;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.primary,
      spectralType: SpectralType.Red,
      spectrum: 5,
      luminosity: Luminosity.VI,
      orbit: Orbit.primary,
      expansionSize: 5,
      age: 7
    } as Star;

    system.stars.push(star);

    let planet1 = this._planetService.getNewPlanet(star, PlanetOrbit.Epistellar, 1);
    let planet2 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 2);
    let planet3 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 3);
    let planet4 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 4);
    let planet5 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 5);

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Chrotol", 2, SettlementType.Homeworld,
      "A", 3, 1, 4, 15, 5, 5, 10, 12,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Inner, {} as Planet, star, 6, Chemistry.Water,
      false, 23.54, 418.63);
    let satellite1 = this._planetService.getNewSatellite(PlanetType.Dwarf, planet, star);
    planet.satellites = [satellite1];
    planet.settlement.isCapitol = true;

    let planet6 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 7);
    let planet7 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 8);
    let planet8 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 9);

    system.planets.push(planet1);
    system.planets.push(planet2);
    system.planets.push(planet3);
    system.planets.push(planet4);
    system.planets.push(planet5);
    system.planets.push(planet);
    system.planets.push(planet6);
    system.planets.push(planet7);
    system.planets.push(planet8);

    system = this.finishPlanets(system);

    return system;
  }

  private createVargrHomeworldSystem(): System {
    let race = Races.Vargr;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let kneng = {
      type: StarType.primary,
      spectralType: SpectralType.Yellow,
      spectrum: 5,
      luminosity: Luminosity.V,
      orbit: Orbit.primary,
      expansionSize: 0,
      age: 4
    } as Star;
    system.stars.push(kneng);

    let ogOrz = this._planetService.getCustomPlanet(race.allegianceCode, "Og Orz", 10, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, {} as Planet, kneng, 1, Chemistry.Water,
      false, 3030, 126.25);

    let erzikhDhadh = this._planetService.getCustomPlanet(race.allegianceCode, "Erzikh Dhadh", 9, SettlementType.Outpost,
      "H", 3, 1, 0, 0, 3, 0, 0, 11,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, {} as Planet, kneng, 2, Chemistry.Water,
      false, 3030, 215.36);

    let foulours = this._planetService.getCustomPlanet(race.allegianceCode, "Foulours", 11, SettlementType.Uninhabited,
      "X", 6, 3, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.Minor, PlanetOrbit.Inner, {} as Planet, kneng, 3, Chemistry.Water,
      false, 14.36, 295.14);
    let servonaz = this._planetService.getCustomPlanet(race.allegianceCode, "Servonaz'", 4, SettlementType.Uninhabited,
      "X", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, foulours, kneng, 1, Chemistry.Water,
      false, 256.36, 10.68);
    let olou = this._planetService.getCustomPlanet(race.allegianceCode, "Olou'", 5, SettlementType.Uninhabited,
      "X", 3, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, foulours, kneng, 1, Chemistry.Water,
      false, 369.15, 15.38);
    foulours.satellites = [servonaz, olou];

    let lair = this._planetService.getCustomPlanet(race.allegianceCode, "Lair", 5, SettlementType.Homeworld,
      "A", 8, 8, 5, 15, 9, 11, 9, 11,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Inner, {} as Planet, kneng, 4, Chemistry.Water,
      false, 25.97, 376.72);
    let errgh = this._planetService.getCustomPlanet(race.allegianceCode, "Errgh'", 2, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, lair, kneng, 4, Chemistry.Water,
      false, 65.36, 2.72);
    let sanko = this._planetService.getCustomPlanet(race.allegianceCode, "Sanko'", 11, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Meltball, RingType.None, PlanetOrbit.Inner, lair, kneng, 4, Chemistry.Water,
      false, 126.75, 5.28);
    let ouksadoFan = this._planetService.getCustomPlanet(race.allegianceCode, "Ouksado Fan'", 2, SettlementType.Uninhabited,
      "F", 2, 0, 0, 0, 3, 1, 12, 11,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Inner, lair, kneng, 4, Chemistry.Water,
      false, 36.91, 34.87);
    lair.satellites = [errgh, sanko, ouksadoFan];
    lair.settlement.isCapitol = true;

    let goullaengrak = this._planetService.getCustomPlanet(race.allegianceCode, "Goullaengrak", 7, SettlementType.Uninhabited,
      "Y", 15, 15, 15, 0, 0, 0, 0, 0,
      [], PlanetType.Jovian, RingType.Complex, PlanetOrbit.Outer, {} as Planet, kneng, 1, Chemistry.Water,
      false, 14.36, 0.60);
    let raekfong = this._planetService.getCustomPlanet(race.allegianceCode, "Raekfong'", 1, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 65.23, 2.72);
    let raekgzoe = this._planetService.getCustomPlanet(race.allegianceCode, "Raekgzoe'", 3, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 93.47, 3.89);
    let knoen = this._planetService.getCustomPlanet(race.allegianceCode, "Knoen'", 4, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 108.32, 4.51);
    let suekh = this._planetService.getCustomPlanet(race.allegianceCode, "Suekh'", 2, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 206.23, 8.59);
    let vadzeAengkoe = this._planetService.getCustomPlanet(race.allegianceCode, "Vadze Aengkoe'", 5, SettlementType.Uninhabited,
      "Y", 3, 3, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 32.48, 9.21);
    let fiItsoudzi = this._planetService.getCustomPlanet(race.allegianceCode, "Fi Itsoudzi'", 1, SettlementType.Outpost,
      "G", 4, 0, 0, 0, 1, 1, 11, 11,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 207.26, 8.63);
    let uerul = this._planetService.getCustomPlanet(race.allegianceCode, "Uerul'", 2, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 295.35, 12.31);
    let dzaer = this._planetService.getCustomPlanet(race.allegianceCode, "Dzaer'", 4, SettlementType.Uninhabited,
      "Y", 2, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Snowball, RingType.None, PlanetOrbit.Outer, goullaengrak, kneng, 1, Chemistry.Water,
      false, 348.67, 14.53);
    goullaengrak.satellites = [raekfong, raekgzoe, knoen, suekh, vadzeAengkoe, fiItsoudzi, uerul, dzaer];

    let garzingnallBelt = this._planetService.getCustomPlanet(race.allegianceCode, "Garzingnall Belt", 0, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 1, 0, 0, 11,
      [], PlanetType.AsteroidBelt, RingType.None, PlanetOrbit.Outer, {} as Planet, kneng, 1, Chemistry.Water,
      false, 0, 0);

    let saghurrkhoer = this._planetService.getCustomPlanet(race.allegianceCode, "Saghurrkhoer", 3, SettlementType.Uninhabited,
      "X", 15, 15, 15, 0, 0, 0, 0, 0,
      [], PlanetType.Jovian, RingType.Minor, PlanetOrbit.Outer, {} as Planet, kneng, 1, Chemistry.Water,
      false, 3030, 126.25);
    let atourrkou = this._planetService.getCustomPlanet(race.allegianceCode, "Atourrkou'", 2, SettlementType.Uninhabited,
      "X", 3, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, saghurrkhoer, kneng, 1, Chemistry.Water,
      false, 3030, 126.25);
    let rrorUsuersogh = this._planetService.getCustomPlanet(race.allegianceCode, "Rror Usuersogh'", 1, SettlementType.Uninhabited,
      "H", 3, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, saghurrkhoer, kneng, 1, Chemistry.Water,
      false, 3030, 126.25);
    saghurrkhoer.satellites = [atourrkou, rrorUsuersogh];

    let ngolkfaedh = this._planetService.getCustomPlanet(race.allegianceCode, "Ngolkfaedh", 1, SettlementType.Uninhabited,
      "Y", 15, 15, 15, 0, 0, 0, 0, 0,
      [], PlanetType.Jovian, RingType.None, PlanetOrbit.Outer, {} as Planet, kneng, 1, Chemistry.Water,
      false, 4531, 78563.42);
    let uzil = this._planetService.getCustomPlanet(race.allegianceCode, "Uzil'", 2, SettlementType.Outpost,
      "H", 4, 0, 0, 0, 1, 6, 7, 11,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, ngolkfaedh, kneng, 1, Chemistry.Water,
      false, 1095.6, 45.65);
    let olahgz = this._planetService.getCustomPlanet(race.allegianceCode, "Olaghz'", 3, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, ngolkfaedh, kneng, 1, Chemistry.Water,
      false, 1818.72, 75.78);
    let knokaLlan = this._planetService.getCustomPlanet(race.allegianceCode, "Knoka Llan'", 12, SettlementType.Uninhabited,
      "Y", 5, 10, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Hebean, RingType.None, PlanetOrbit.Outer, ngolkfaedh, kneng, 1, Chemistry.Water,
      false, 1889.76, 78.74);
    let dhuggzung = this._planetService.getCustomPlanet(race.allegianceCode, "Dhuggzung'", 4, SettlementType.Uninhabited,
      "Y", 0, 0, 0, 0, 0, 0, 0, 0,
      [], PlanetType.Rockball, RingType.None, PlanetOrbit.Outer, ngolkfaedh, kneng, 1, Chemistry.Water,
      false, 2335.68, 97.32);
    ngolkfaedh.satellites = [uzil, olahgz, knokaLlan, dhuggzung];

    let rrallanAng = this._planetService.getCustomPlanet(race.allegianceCode, "Rrallan Ang", 3, SettlementType.Uninhabited,
      "H", 9, 10, 5, 4, 2, 3, 9, 11,
      [], PlanetType.Tectonic, RingType.None, PlanetOrbit.Outer, {} as Planet, kneng, 1, Chemistry.Water,
      false, 85.67, 654178);

    system.planets.push(ogOrz);
    system.planets.push(erzikhDhadh);
    system.planets.push(foulours);
    system.planets.push(lair);
    system.planets.push(goullaengrak);
    system.planets.push(garzingnallBelt);
    system.planets.push(saghurrkhoer);
    system.planets.push(ngolkfaedh);
    system.planets.push(rrallanAng);

    system = this.finishPlanets(system);

    return system;
  }

  private createTheCollectiveHomeworldSystem(): System {
    let race = Races.TheCollective;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.primary,
      spectralType: SpectralType.Blue,
      spectrum: 1,
      luminosity: Luminosity.III,
      orbit: Orbit.primary,
      expansionSize: 2,
      age: 1
    } as Star;

    system.stars.push(star);
    system.stars.push(this._starService.GetNewCompanionStar());

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Aphrodite", 7, SettlementType.Homeworld,
      "A", 0, 0, 1, 0, 2, 10, 8, 13,
      [], PlanetType.AsteroidBelt, RingType.None, PlanetOrbit.Outer, {} as Planet, star, 1, Chemistry.Water,
      false, 64.31, 685.12);
    let satellite1 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite2 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite3 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite4 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite5 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite6 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite7 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite8 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite9 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    let satellite10 = this._planetService.getNewSatellite(PlanetType.SmallBody, planet, star);
    planet.satellites = [satellite1, satellite2, satellite3, satellite4, satellite5, satellite6, satellite7, satellite8, satellite9, satellite10];
    planet.settlement.isCapitol = true;

    let planet1 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 2);
    let planet2 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 3);
    let planet3 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 4);
    let planet4 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 5);

    system.planets.push(planet);
    system.planets.push(planet1);
    system.planets.push(planet2);
    system.planets.push(planet3);
    system.planets.push(planet4);

    system = this.finishPlanets(system);

    return system;
  }

  private createKaSaraHomeworldSystem(): System {
    let race = Races.KaSara;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.primary,
      spectralType: SpectralType.BlueWhite,
      spectrum: 3,
      luminosity: Luminosity.V,
      orbit: Orbit.primary,
      expansionSize: 3,
      age: 4
    } as Star;

    system.stars.push(star);

    let planet1 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 1);
    let planet2 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 2);
    let planet3 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 3);
    let planet4 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 4);
    let planet5 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 5);
    let planet6 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 6);
    let planet7 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 7);

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Kassar", 12, SettlementType.Homeworld,
      "A", 9, 12, 0, 15, 15, 0, 15, 12,
      [], PlanetType.Telluric, RingType.None, PlanetOrbit.Inner, {} as Planet, star, 8, Chemistry.Water,
      false, 34.18, 371.89);
    planet.settlement.isCapitol = true;

    let planet8 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 9);
    let planet9 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 10);
    let planet10 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 11);
    let planet11 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 12);
    let planet12 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 13);
    let planet13 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 14);
    let planet14 = this._planetService.getNewPlanet(star, PlanetOrbit.Inner, 15);
    let planet15 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 16);
    let planet16 = this._planetService.getNewPlanet(star, PlanetOrbit.Outer, 17);

    system.planets.push(planet1);
    system.planets.push(planet2);
    system.planets.push(planet3);
    system.planets.push(planet4);
    system.planets.push(planet5);
    system.planets.push(planet6);
    system.planets.push(planet7);
    system.planets.push(planet);
    system.planets.push(planet8);
    system.planets.push(planet9);
    system.planets.push(planet10);
    system.planets.push(planet11);
    system.planets.push(planet12);
    system.planets.push(planet13);
    system.planets.push(planet14);
    system.planets.push(planet15);
    system.planets.push(planet16);

    system = this.finishPlanets(system);

    return system;
  }

  private createScrapperHomeworldSystem(): System {
    let race = Races.Scrappers;

    let system = {
      stars: [] as Star[],
      planets: [] as Planet[]
    } as System;

    let star = {
      type: StarType.BrownDwarf,
      spectralType: SpectralType.BrownDwarf,
      spectrum: 0,
      luminosity: Luminosity.BD,
      orbit: Orbit.primary,
      expansionSize: 6,
      age: 12
    } as Star;

    system.stars.push(star);

    let planet = this._planetService.getCustomPlanet(race.allegianceCode, "Scrap-A", 0, SettlementType.Homeworld,
      "X", 1, 0, 0, 0, 12, 0, 0, 15,
      [], PlanetType.Scrap, RingType.None, PlanetOrbit.Inner, {} as Planet, star, 1, Chemistry.None,
      false, 2.56, 34.21);
    planet.settlement.isCapitol = true;

    system.planets.push(planet);

    system = this.finishPlanets(system);

    return system;
  }

  private finishPlanets(system: System): System {
      for (let planet of system.planets) {
        this._planetService.finishPlanet(planet);
      }
      return system;
  }
}
