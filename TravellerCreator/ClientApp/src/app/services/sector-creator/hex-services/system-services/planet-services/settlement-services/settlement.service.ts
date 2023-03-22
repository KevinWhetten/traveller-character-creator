import { Injectable } from '@angular/core';
import {Planet} from "../../../../../../models/sector-creator/hex/system/planet/planet";
import {TradeCode} from "../../../../../../models/sector-creator/hex/system/planet/trade-code";
import {Chemistry} from "../../../../../../models/sector-creator/hex/system/planet/chemistry";
import {SettlementType} from "../../../../../../models/sector-creator/hex/system/planet/settlement/settlement-type";
import {Base} from "../../../../../../models/sector-creator/hex/system/planet/settlement/base";
import {Economics} from "../../../../../../models/sector-creator/hex/system/planet/settlement/economics";
import {Culture} from "../../../../../../models/sector-creator/hex/system/planet/settlement/culture";
import {CloseRace} from "../../../../../../models/sector-creator/race/close-race";
import {Races} from "../../../../../../models/sector-creator/race/races";
import {Coordinates} from "../../../../../../models/sector-creator/hex/coordinates";
import {Race} from "../../../../../../models/sector-creator/race/race";
import {RollingService} from "../../../../../data-services/rolling.service";
import {PlanetDesirabilityService} from "../planet-desirability.service";
import {Settlement} from "../../../../../../models/sector-creator/hex/system/planet/settlement/settlement";
import {RandomNameService} from "../random-name.service";

@Injectable({
  providedIn: 'root'
})
export class SettlementService {

  constructor(private _rollingService: RollingService,
              private _randomNameService: RandomNameService,
              private _planetDesirabilityService: PlanetDesirabilityService) { }

  getSettlement(planet: Planet): Settlement {
    let settlement = {
      planet: planet,
      allegiance: this.getAllegiance(),
      isCapitol: this.getIsCapitol(),

      settlementType: this.getType(),

      population: this.getPopulation(),
      government: this.getGovernment(),
      lawLevel: this.getLawLevel(planet),
      techLevel: this.getTechLevel(planet),
      starport: this.getStarport(planet),
      industry: this.getIndustry(planet),

      culture: this.getCulture(planet),
      economics: this.getEconomics(planet),
      bases: this.getBases(planet)
    } as Settlement;

    return settlement;
  }

  private getAllegiance() {
    return "";
  }

  private getIsCapitol() {
    return false;
  }

  private getType() {
    return SettlementType.Uninhabited;
  }

  private getPopulation() {
    return 0;
  }

  private getGovernment() {
    return 0;
  }

  private getLawLevel(planet: Planet): number {
    return planet.settlement.population > 0
      ? Math.max(0, planet.settlement.government + this._rollingService.roll(2) - 7)
      : 0;
  }

  private getIndustry(planet: Planet): number {
    if (planet.settlement.population == 0) {
      return 0;
    }

    let industry = 0;

    // Industry
    industry = Math.max(0,
      planet.settlement.population + this._rollingService.roll(2) - 7
      + (1 <= planet.settlement.lawLevel && planet.settlement.lawLevel <= 3 ? 1 : 0)
      + (6 <= planet.settlement.lawLevel && planet.settlement.lawLevel <= 9 ? -1 : 0)
      + (10 <= planet.settlement.lawLevel && planet.settlement.lawLevel <= 12 ? -2 : 0)
      + (planet.settlement.lawLevel >= 13 ? -3 : 0)
      + (planet.atmosphere <= 4 || planet.atmosphere == 7 || planet.atmosphere >= 9 || planet.hydrosphere == 15 ? 1 : 0)
      + 2);

    if (industry == 0) {
      planet.settlement.population = Math.max(0, planet.settlement.population - 1);
    } else if (industry <= 9) {
      planet.settlement.population += 1;
      if (planet.atmosphere == 3) {
        planet.atmosphere = 2;
      } else if (planet.atmosphere == 5) {
        planet.atmosphere = 4;
      } else if (planet.atmosphere == 6) {
        planet.atmosphere = 7;
      } else if (planet.atmosphere == 8) {
        planet.atmosphere = 9;
      }
    } else if (industry >= 10) {
      let roll = this._rollingService.roll(1);
      if (roll <= 3) {
        planet.settlement.population += 1;
        if (planet.atmosphere == 3) {
          planet.atmosphere = 2;
        } else if (planet.atmosphere == 5) {
          planet.atmosphere = 4;
        } else if (planet.atmosphere == 6) {
          planet.atmosphere = 7;
        } else if (planet.atmosphere == 8) {
          planet.atmosphere = 9;
        }
      } else {
        planet.settlement.population += 2;
      }
    }
    return industry;
  }

  private getStarport(planet: Planet): string {
    if (planet.settlement.settlementType == SettlementType.Outpost && planet.settlement.population == 0) {
      return "E";
    } else if (planet.settlement.settlementType == SettlementType.Uninhabited) {
      return "X";
    }

    let starport = this._rollingService.roll(2) + planet.settlement.industry - 7
      + (planet.tradeCodes.includes(TradeCode.Agricultural) ? 1 : 0)
      + (planet.tradeCodes.includes(TradeCode.Garden) ? 1 : 0)
      + (planet.tradeCodes.includes(TradeCode.HighPopulation) ? 1 : 0)
      + (planet.tradeCodes.includes(TradeCode.HighTech) ? 1 : 0)
      + (planet.tradeCodes.includes(TradeCode.Industrial) ? 1 : 0)
      + (planet.tradeCodes.includes(TradeCode.NonAgricultural) ? 1 : 0)
      + (planet.tradeCodes.includes(TradeCode.Rich) ? 1 : 0)
      + (planet.tradeCodes.includes(TradeCode.LowPopulation) ? -1 : 0)
      + (planet.tradeCodes.includes(TradeCode.Poor) ? -1 : 0)
      + (planet.settlement.techLevel <= 9 ? -1 : 0)
      + (12 <= planet.settlement.techLevel && planet.settlement.techLevel <= 14 ? 1 : 0)
      + (planet.settlement.techLevel >= 15 ? 2 : 0);

    if (starport <= 2) {
      if (planet.settlement.industry >= 5) {
        return "E";
      } else if ((0 <= planet.atmosphere && planet.atmosphere <= 4 || planet.atmosphere >= 9)
        || (planet.hydrosphere == 15)) {
        return "E";
      }
      return "X";
    } else if (starport <= 4) {
      return "E";
    } else if (starport <= 6) {
      return "D";
    } else if (starport <= 8) {
      return "C";
    } else if (starport <= 10) {
      return "B";
    } else {
      return "A";
    }
  }

  private getBases(planet: Planet): Base[] {
    let bases = [] as Base[];

    let roll = this._rollingService.roll(2);
    if (planet.settlement.starport == "A" && roll >= 6) {
      if (planet.settlement.settlementType == SettlementType.Homeworld || planet.settlement.settlementType == SettlementType.Colony) {
        bases.push(Base.imperialConsulate);
      } else {
        bases.push(Base.governorsEstate);
      }
      if (roll >= 9) {
        bases.push(Base.foreignEmbassy);
      }
    } else if (planet.settlement.starport == "B" && roll >= 8) {
      if (planet.settlement.settlementType == SettlementType.Homeworld || planet.settlement.settlementType == SettlementType.Colony) {
        bases.push(Base.imperialConsulate);
      } else {
        bases.push(Base.governorsEstate);
      }
      if (roll >= 11) {
        bases.push(Base.foreignEmbassy);
      }
    } else if (planet.settlement.starport == "C" && roll >= 10) {
      if (planet.settlement.settlementType == SettlementType.Homeworld || planet.settlement.settlementType == SettlementType.Colony) {
        bases.push(Base.imperialConsulate);
      } else {
        bases.push(Base.governorsEstate);
      }
    }

    roll = this._rollingService.roll(2);
    if (planet.settlement.starport == "A" && roll >= 6) {
      bases.push(Base.merchantBase);
      if (roll >= 9) {
        bases.push(Base.shipyard);
      }
      if (roll >= 12) {
        bases.push(Base.megacorporateBase);
      }
    } else if (planet.settlement.starport == "B" && roll >= 8) {
      bases.push(Base.merchantBase);
      if (roll >= 11) {
        bases.push(Base.shipyard);
      }
    } else if (planet.settlement.starport == "C" && roll >= 10) {
      bases.push(Base.merchantBase);
    }

    roll = this._rollingService.roll(2);
    if ((planet.settlement.starport == "A" || planet.settlement.starport == "B" || planet.settlement.starport == "C") && roll >= 8) {
      bases.push(Base.navalBase);
      if (roll >= 11) {
        roll = this._rollingService.roll(1);
        if (roll <= 3) {
          bases.push(Base.shipyard);
        } else {
          bases.push(Base.navalDepot);
        }
      }
    }

    roll = this._rollingService.roll(2);
    if ((planet.settlement.starport == "B" && roll >= 12)
      || (planet.settlement.starport == "C" && roll >= 10)
      || ((planet.settlement.starport == "D" || planet.settlement.starport == "E") && roll >= 12)) {
      bases.push(Base.pirateBase);
    }

    roll = this._rollingService.roll(2);
    if (roll >= 12) {
      bases.push(Base.psionicsInstitute);
    }

    roll = this._rollingService.roll(2);
    if (planet.settlement.starport == "A" && roll >= 8) {
      bases.push(Base.researchStation);
      if (roll >= 11) {
        roll = this._rollingService.roll(1);
        if (roll <= 2) {
          bases.push(Base.hospital);
        } else if (roll <= 4) {
          bases.push(Base.galacticUniversity);
        } else {
          bases.push(Base.dataRepository);
        }
      }
    } else if (planet.settlement.starport == "B" && roll >= 10) {
      bases.push(Base.researchStation);
    } else if (planet.settlement.settlementType == SettlementType.Outpost && roll >= 9) {
      bases.push(Base.researchStation);
      if (roll >= 12) {
        bases.push(Base.dataRepository);
      }
    }

    roll = this._rollingService.roll(2);
    if (roll <= planet.settlement.population) {
      bases.push(Base.galacticChurch);
    }

    roll = this._rollingService.roll(2);
    if (planet.settlement.starport == "A" && roll >= 10) {
      if (planet.settlement.settlementType == SettlementType.Homeworld || planet.settlement.settlementType == SettlementType.Colony) {
        bases.push(Base.scoutBase);
      } else {
        bases.push(Base.explorationBase);
      }
    } else if ((planet.settlement.starport == "B" || planet.settlement.starport == "C") && roll >= 8) {
      if (planet.settlement.settlementType == SettlementType.Homeworld || planet.settlement.settlementType == SettlementType.Colony) {
        bases.push(Base.scoutBase);
      } else {
        bases.push(Base.explorationBase);
      }
      if (roll >= 11) {
        bases.push(Base.scoutHostel);
      }
    } else if (planet.settlement.starport == "D" && roll >= 7) {
      if (planet.settlement.settlementType == SettlementType.Homeworld || planet.settlement.settlementType == SettlementType.Colony) {
        bases.push(Base.scoutBase);
      } else {
        bases.push(Base.explorationBase);
      }
      if (roll >= 10) {
        bases.push(Base.scoutHostel);
      }
    }

    roll = this._rollingService.roll(2);
    if (planet.settlement.population >= 1 && roll >= 10) {
      bases.push(Base.prison);
    } else if (planet.biosphere >= 1 && roll >= 10) {
      bases.push(Base.naturePreserve)
    }

    roll = this._rollingService.roll(2);
    if (planet.settlement.starport == "A" && roll >= 4) {
      if (roll <= 6) {
        bases.push(Base.TASHostel);
      } else if (roll <= 9) {
        bases.push(Base.TASFirstClassHostel);
      } else if (roll >= 10) {
        bases.push((Base.TASChapterHouse));
      }
    } else if (planet.settlement.starport == "B" && roll >= 6) {
      if (roll <= 8) {
        bases.push(Base.TASHostel);
      } else if (roll >= 9) {
        bases.push(Base.TASFirstClassHostel);
      }
    } else if (planet.settlement.starport == "C" && roll >= 10) {
      bases.push(Base.TASHostel);
    }

    if (planet.terraformed) {
      bases.push(Base.terraformingFacility);
    }

    return bases;
  }

  private getTechLevel(planet: Planet): number {
    if (planet.settlement.population == 0) {
      return 0;
    }

    let techLevel = this._rollingService.roll(1);

    if (planet.settlement.starport == "A") {
      techLevel += 6;
    } else if (planet.settlement.starport == "B") {
      techLevel += 4;
    } else if (planet.settlement.starport == "C") {
      techLevel += 2;
    } else if (planet.settlement.starport == "X") {
      techLevel -= 4;
    }

    if (0 <= planet.size && planet.size <= 1) {
      techLevel += 2;
    } else if (2 <= planet.size && planet.size <= 4) {
      techLevel += 1;
    }

    if (0 <= planet.atmosphere && planet.atmosphere <= 3) {
      techLevel += 1
    } else if (planet.atmosphere >= 10) {
      techLevel += 1;
    }

    if (planet.hydrosphere == 0) {
      techLevel += 1;
    } else if (planet.hydrosphere == 9) {
      techLevel += 1;
    } else if (planet.hydrosphere == 10) {
      techLevel += 2;
    }

    if ((1 <= planet.settlement.population && planet.settlement.population <= 5)
      || planet.settlement.population == 8) {
      techLevel += 1;
    } else if (10 <= planet.settlement.population && planet.settlement.population <= 12) {
      techLevel += 2;
    } else if (planet.settlement.population >= 13) {
      techLevel += 4;
    }

    if (planet.settlement.government == 0 || planet.settlement.government == 5) {
      techLevel += 1;
    } else if (planet.settlement.government == 7) {
      techLevel += 2;
    } else if (planet.settlement.government == 13 || planet.settlement.government == 14) {
      techLevel -= 2;
    }

    return Math.min(Math.max(techLevel, 0), techLevel);
  }

  getImportance(planet: Planet) {
    let importance = 0;

    if (planet.settlement.starport == "A" || planet.settlement.starport == "B") {
      importance++;
    } else if (planet.settlement.starport == "D" || planet.settlement.starport == "E" || planet.settlement.starport == "X") {
      importance--;
    }

    if (planet.settlement.techLevel >= 10) {
      importance++;
    } else if (planet.settlement.techLevel <= 8) {
      importance--;
    }

    if (planet.tradeCodes.includes(TradeCode.Agricultural)) {
      importance++;
    }
    if (planet.tradeCodes.includes(TradeCode.HighPopulation)) {
      importance++;
    }
    if (planet.tradeCodes.includes(TradeCode.Industrial)) {
      importance++;
    }
    if (planet.tradeCodes.includes(TradeCode.Rich)) {
      importance++;
    }

    if (planet.settlement.population <= 6) {
      importance--;
    }

    if (planet.settlement.bases.includes(Base.navalBase)
      && (planet.settlement.bases.includes(Base.scoutBase) || planet.settlement.bases.includes(Base.scoutHostel))) {
      importance++;
    }

    if (planet.settlement.isCapitol) {
      importance += 2;
    }

    return Math.min(5, importance);
  }

  getEconomics(planet: Planet) {
    let economics = {} as Economics;

    economics.resources = Math.max(0, this._planetDesirabilityService.getResources(planet));
    economics.labor = Math.max(0, planet.settlement.population - 1);
    economics.infrastructure = Math.max(0, this.getInfrastructure(planet));
    economics.efficiency = this._rollingService.roll(2) - 7;

    if (economics.efficiency == 0) {
      economics.efficiency = 1;
    }

    return economics;
  }

  getCulture(planet: Planet) {
    let culture = {} as Culture;

    culture.homogeneity = Math.max(1, planet.settlement.population + this._rollingService.roll(2) - 7);
    culture.acceptance = Math.max(1, planet.settlement.population + planet.importance);
    culture.strangeness = Math.min(15, Math.max(1, this._rollingService.roll(2) - 2));
    culture.symbols = Math.min(15, Math.max(1, this._rollingService.roll(2) - 7 + planet.settlement.techLevel));

    return culture;
  }

  private getPlanetHabitation(planet: Planet): Planet {
    let raceList = [] as CloseRace[];

    for (let race of Races.AllRaces) {
      let coordinates = {row: planet.star.system.hex.coordinates.row, column: planet.star.system.hex.coordinates.column} as Coordinates;
      let calculatedDistance = this.calculateDistance(coordinates, race.homeworldCoordinates);
      raceList.push({race: race, distance: calculatedDistance} as CloseRace);
    }
    raceList.sort((a, b) => (a.distance < b.distance) ? -1 : 1);

    for (let closestRace of raceList) {
      let desirability = this._planetDesirabilityService.getDesirabilityFor(closestRace.race, planet);
      let expansionLevel = closestRace.race.expansionLevel;

      let expansionRate = Math.min(desirability, expansionLevel) - closestRace.distance;
      if (expansionRate >= this._rollingService.roll(2)) {
        return this.getColonyHabitation(planet, desirability, closestRace.race);
      } else if (expansionRate >= this._rollingService.roll(1)) {
        return this.getOutpostHabitation(planet, desirability, closestRace.race);
      }
    }
    let roll = this._rollingService.roll(2);
    if (roll >= 12) {
      let roll = this._rollingService.roll(1);
      if (roll >= 6) {
        return this.getHomeworldHabitation(planet, 5, {allegianceCode: "", techLevel: this._rollingService.roll(2) + 3} as Race);
      } else if (roll >= 4) {
        return this.getColonyHabitation(planet, 5, {allegianceCode: "", techLevel: this._rollingService.roll(2) + 3} as Race);
      } else {
        return this.getOutpostHabitation(planet, 5, {allegianceCode: "", techLevel: this._rollingService.roll(2) + 3} as Race);
      }
    } else {
      planet.settlement.population = 0;
      planet.settlement.government = 0;
      planet.settlement.settlementType = SettlementType.Uninhabited;
      planet.settlement.allegiance = "";
      return planet;
    }
  }

  private getHomeworldHabitation(planet: Planet, desirability: number, race: Race): Planet {
    planet.settlement.settlementType = SettlementType.Homeworld;

    if (planet.chemistry == Chemistry.Water) {
      planet.settlement.population = desirability + Math.ceil(this._rollingService.roll(1) / 2) - Math.ceil(this._rollingService.roll(1) / 2);
    } else {
      planet.settlement.population = this._rollingService.roll(2);
    }

    let roll = this._rollingService.roll(1);
    if (roll <= this.getTechLevel(planet)) {
      planet.settlement.government = 7;
    } else {
      planet.settlement.government = Math.max(0, planet.settlement.population + this._rollingService.roll(2) - 7);
    }

    if (planet.settlement.population >= 1) {
      planet.settlement.allegiance = race.allegianceCode;
    }

    planet.name = this.generateName(race);

    return planet;
  }

  private getColonyHabitation(planet: Planet, desirability: number, race: Race): Planet {
    planet.settlement.settlementType = SettlementType.Colony;

    let settlement = this._rollingService.roll(2);

    planet.settlement.population = Math.max(1, Math.min(
      desirability + Math.ceil(this._rollingService.roll(1) / 2) - Math.ceil(this._rollingService.roll(1) / 2),
      Math.max(4, (Math.max(0, race.techLevel - this._rollingService.roll(1) + 1)) + settlement - 9)));

    if (1 <= planet.size && planet.size <= 11
      && 2 <= planet.atmosphere && planet.atmosphere <= 9
      && planet.biosphere <= 2) {
      planet.biosphere = this._rollingService.roll(1) + 5;
    }

    if (planet.settlement.population >= 1) {
      planet.settlement.allegiance = race.allegianceCode;
    }

    planet.settlement.government = Math.max(0, planet.settlement.population + this._rollingService.roll(2) - 7);

    planet.settlement.techLevel = race.techLevel;

    planet.name = this.generateName(race);

    return planet;
  }

  private getOutpostHabitation(planet: Planet, desirability: number, race: Race): Planet {
    planet.settlement.settlementType = SettlementType.Outpost;

    planet.settlement.population = Math.min(4, Math.max(0, Math.ceil(this._rollingService.roll(1) / 2) + desirability));

    if (planet.settlement.population == 0) {
      planet.settlement.government = 0;
    } else {
      planet.settlement.government = Math.min(6, Math.max(0, planet.settlement.population + this._rollingService.roll(2) - 7));
    }

    if (planet.settlement.population >= 1) {
      planet.settlement.allegiance = race.allegianceCode;
    }

    planet.settlement.techLevel = race.techLevel;

    planet.name = this.generateName(race);

    return planet;
  }

  private getInfrastructure(planet: Planet) {
    if (planet.tradeCodes.includes(TradeCode.Barren) && planet.tradeCodes.includes(TradeCode.Dieback) && planet.tradeCodes.includes(TradeCode.LowPopulation)) {
      return 0;
    }
    if (planet.tradeCodes.includes(TradeCode.LowPopulation)) {
      return 1;
    }
    if (planet.tradeCodes.includes(TradeCode.NonIndustrial)) {
      return this._rollingService.roll(1) + planet.importance;
    }
    return this._rollingService.roll(2) + planet.importance;
  }

  private calculateDistance(coordinates1: Coordinates, coordinates2: Coordinates): number {
    let distance = 0;
    let currentHex = coordinates1;

    if (currentHex.row == coordinates2.row) {
      distance += Math.abs(currentHex.column - coordinates2.column);
      return distance;
    } else if (currentHex.column == coordinates2.column) {
      distance += Math.abs(currentHex.row - coordinates2.row);
      return distance;
    } else if (currentHex.row < coordinates2.row
      && currentHex.column < coordinates2.column) {
      if (currentHex.column % 2 == 0) {
        currentHex.column++;
        currentHex.row++;
      } else {
        currentHex.column++;
      }
      distance++;
    } else if (currentHex.row < coordinates2.row
      && currentHex.column > coordinates2.column) {
      if (currentHex.column % 2 == 0) {
        currentHex.column--;
        currentHex.row++;
      } else {
        currentHex.column--;
      }
      distance++;
    } else if (currentHex.row > coordinates2.row
      && currentHex.column < coordinates2.column) {
      if (currentHex.column % 2 == 0) {
        currentHex.column++;
      } else {
        currentHex.row--;
        currentHex.column++;
      }
      distance++;
    } else if (currentHex.row > coordinates2.row
      && currentHex.column > coordinates2.column) {
      if (currentHex.column % 2 == 0) {
        currentHex.column--;
      } else {
        currentHex.row--;
        currentHex.column--;
      }
      distance++;
    }
    if (currentHex.row == coordinates2.row && currentHex.column == coordinates2.column) {
      return distance;
    }
    return distance + this.calculateDistance(currentHex, coordinates2);
  }

  private generateName(race: Race): string {
    return this._randomNameService.getLargosianName();
  }
}
