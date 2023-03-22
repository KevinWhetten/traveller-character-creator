import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {MatDialog} from '@angular/material/dialog';
import { Hex } from '../models/sector-creator/hex/hex';
import {Star} from "../models/sector-creator/hex/system/star/star";
import {Coordinates} from "../models/sector-creator/hex/coordinates";
import {PlanetType} from "../models/sector-creator/hex/system/planet/planet-type";
import {SpectralType} from "../models/sector-creator/hex/system/star/spectral-type";
import {Planet} from "../models/sector-creator/hex/system/planet/planet";
import {Base} from "../models/sector-creator/hex/system/planet/settlement/base";
import {HexService} from "../services/sector-creator/hex-services/hex.service";
import { TradeCode } from '../models/sector-creator/hex/system/planet/trade-code';
import {SystemInfoDialog} from "./rtt-worldgen/system-info/system-info-dialog";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-sector-creator',
  templateUrl: './sector-creator.component.html',
  styleUrls: ['./sector-creator.component.scss']
})
export class SectorCreatorComponent implements OnInit {
  sector: Hex[][] = [];

  constructor(private _router: Router,
              private _httpClient: HttpClient,
              private _hexService: HexService,
              public dialog: MatDialog) {
  }

  ngOnInit() {
    for (let i = 0; i < 16; i++) {
      this.sector.push([] as Hex[])
    }
  }

  generateBasicSector() {
    let sector = this._httpClient.get("http://localhost:5000/CreateSector/BasicSector");
  }

  generateSpaceOperaSector() {

  }

  generateHardScienceSector() {

  }

  generateSecondSurveySector() {

  }

  generateRttSector() {
    this.generateSubsector(1);
    this.generateSubsector(2);
    this.generateSubsector(3);
    this.generateSubsector(4);
    this.generateSubsector(5);
    this.generateSubsector(6);
    this.generateSubsector(7);
    this.generateSubsector(8);
    this.generateSubsector(9);
    this.generateSubsector(10);
    this.generateSubsector(11);
    this.generateSubsector(12);
    this.generateSubsector(13);
    this.generateSubsector(14);
    this.generateSubsector(15);
    this.generateSubsector(16);
  }

  private generateSubsector(subsectorNum: number) {
    let hexColumn = ((subsectorNum - 1) % 4) * 8 + 1;
    let hexRow = Math.floor((subsectorNum - 1) / 4) * 10 + 1;

    for (let i = 0; i < 80; i++) {
      if(i % 10 == 0 && i !== 0){
        hexColumn++;
        hexRow = Math.floor((subsectorNum - 1) / 4) * 10 + 1;
      }
      this.addHex(8, subsectorNum, hexRow, hexColumn);
      hexRow++;
    }
  }

  addHex(techLevel: number, sectorNumber: number, hexRow: number, hexColumn: number) {
    let newHex = this._hexService.createNewHex({row: hexRow, column: hexColumn} as Coordinates);
    this.sector[sectorNumber - 1].push(newHex);
  }

  getInfo(hex: Hex): string {
    let stars = 0;
    let planets = 0;

    for (let system of hex.systems) {
      stars += system.stars.length;
      planets += system.planets.length;
    }

    return `Systems: ${hex.systems.length}, Stars: ${stars}, Planets: ${planets}`;
  }

  openDialog(hex: Hex) {
    this.dialog.open(SystemInfoDialog, {
      width: '500px',
      data: hex,
    });
  }

  DecToHex(number: number): string {
    if (number <= 9) {
      return number.toString();
    } else {
      switch (number) {
        case 10:
          return "A";
        case 11:
          return "B";
        case 12:
          return "C";
        case 13:
          return "D";
        case 14:
          return "E";
        case 15:
          return "F";
      }
    }
    return " ";
  }

  download() {
    let text = this.getText();

    let file = new Blob([text], {type: '.txt'});
    let a = document.createElement("a"),
      url = URL.createObjectURL(file);
    a.href = url;
    a.download = 'sector';
    document.body.appendChild(a);
    a.click();
    setTimeout(function () {
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    }, 0);
  }

  private getText(): string {
    let text =
      `Hex  Name                 UWP       Remarks                           {Ix}   (Ex)    [Cx]   N B   Z PBG W  A  Stellar
---- -------------------- --------- --------------------------------- ------ ------- ------ - --- - --- -- -- ----------------------
`;
    let bestPlanet = {importance: -5} as Planet;
    let worldNum = 0;
    let belts = 0
    let giants = 0;
    let star = {} as Star;
    let subsectorNum = 0;

    for (let subsector of this.sector) {
      subsectorNum++;
      text += '\n';
      for (let hex of subsector) {
        for (let system of hex.systems) {
          worldNum = 0;
          belts = 0;
          giants = 0;
          for (let planet of system.planets) {
            worldNum++;
            if (bestPlanet.importance < planet.importance) {
              bestPlanet = planet;
              star = planet.star;
            }
            if (planet.type == PlanetType.AsteroidBelt) {
              belts++;
            } else if (planet.type == PlanetType.Jovian) {
              giants++;
            }
          }
        }
        if (bestPlanet.settlement.population >= 1 && bestPlanet.importance != -5) {
          text += `${hex.coordinates.column.toString().padStart(2, '0')}${hex.coordinates.row.toString().padStart(2, '0')} `;
          text += `${bestPlanet.name.padEnd(20, ' ')} `;
          text += `${bestPlanet.settlement.starport}${this.DecToHex(bestPlanet.size)}${this.DecToHex(bestPlanet.atmosphere)}${this.DecToHex(Math.min(10, bestPlanet.hydrosphere))}${this.DecToHex(bestPlanet.settlement.population)}${this.DecToHex(bestPlanet.settlement.government)}${this.DecToHex(bestPlanet.settlement.lawLevel)}-${this.DecToHex(bestPlanet.settlement.techLevel)} `;
          text += `${this.getRemarks(bestPlanet.tradeCodes)} `
          text += `{ ${bestPlanet.importance.toString().padEnd(2, ' ')} } `;
          text += `(${this.DecToHex(bestPlanet.settlement.economics.resources)}${this.DecToHex(bestPlanet.settlement.economics.labor)}${this.DecToHex(bestPlanet.settlement.economics.infrastructure)}${bestPlanet.settlement.economics.efficiency >= 0 ? '+' + bestPlanet.settlement.economics.efficiency : bestPlanet.settlement.economics.efficiency}) `;
          text += `[${this.DecToHex(bestPlanet.settlement.culture.homogeneity)}${this.DecToHex(bestPlanet.settlement.culture.acceptance)}${this.DecToHex(bestPlanet.settlement.culture.strangeness)}${this.DecToHex(bestPlanet.settlement.culture.symbols)}] `;
          text += `- `;
          text += `${this.getBases(bestPlanet.settlement.bases)} `;
          text += `- `;
          text += `${bestPlanet.pbg.population}${this.DecToHex(belts)}${this.DecToHex(giants)} `;
          text += `${worldNum.toString().padEnd(2, ' ')} `;
          text += `${bestPlanet.settlement.allegiance.padEnd(2, ' ')} `;
          text += `${this.getStars(hex).padEnd(17, ' ')}`
          text += '\n';
        }
        bestPlanet = {importance: -5} as Planet;
      }
    }

    return text;
  }

  private getStars(hex: Hex) {
    let text = "";
    for (let system of hex.systems) {
      for (let star of system.stars) {
        if (star.spectralType != SpectralType.WhiteDwarf && star.spectralType != SpectralType.BrownDwarf) {
          text += `${star.spectralType}${star.spectrum} ${star.luminosity} `;
        } else {
          text += `${SpectralType.WhiteDwarf} `;
        }
      }
    }
    return text;
  }

  getBases(bases: Base[]): string {
    return `${bases.includes(Base.navalDepot) ? 'D' : ''}${bases.includes(Base.navalBase) ? 'N' : ''}${bases.includes(Base.scoutBase) ? 'S' : ''}${bases.includes(Base.explorationBase) ? 'V' : ''}${bases.includes(Base.wayStation) ? 'W' : ''}`.padEnd(3, ' ');
  }

  private getRemarks(tradeCodes: string[]) {
    let codes = "";
    let validCodes = [TradeCode.AsteroidBelt, TradeCode.Desert, TradeCode.FluidOceans, TradeCode.Garden, TradeCode.Hellworld, TradeCode.IceCapped, TradeCode.Ocean, TradeCode.Vacuum, TradeCode.WaterWorld, TradeCode.Dieback, TradeCode.Barren, TradeCode.LowPopulation, TradeCode.NonIndustrial, TradeCode.HighPopulation, TradeCode.Agricultural, TradeCode.NonAgricultural, TradeCode.Industrial, TradeCode.Poor, TradeCode.Rich, TradeCode.Frozen, TradeCode.Hot, TradeCode.Cold, TradeCode.TidallyLocked, TradeCode.Tropical, TradeCode.Tundra, TradeCode.TwilightZone, TradeCode.Capitol, TradeCode.Satellite, TradeCode.DataRepository, TradeCode.AncientSite, TradeCode.ResearchStation, TradeCode.LowTech, TradeCode.HighTech, TradeCode.PrisonCamp, TradeCode.MilitaryRule, TradeCode.Reserve] as string[];

    codes = tradeCodes.filter(x => {
      return validCodes.includes(x);
    }).join(' ');

    return codes.padEnd(33, ' ');
  }
}
