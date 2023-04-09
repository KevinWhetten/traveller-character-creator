import {Injectable} from '@angular/core';
import {TravelCode} from "../models/enums/travel-code";
import {Base} from "../models/enums/base";
import {TradeCode} from "../models/enums/trade-code";
import {RollingService} from "../../services/rolling.service";
import {Planet} from "../models/basic/planet";
import {Hex} from "../models/basic/hex";
import {StarSystem} from "../models/basic/star-system";

@Injectable({
  providedIn: 'root'
})
export class UWPService {

  constructor(protected _rollingService: RollingService) {
  }

   GetFileFormatHeader(): string {
    return `Hex  Name UWP       Remarks              {Ix}  (Ex)    [Cx]   N      B Z PBG W  A  Stellar
---- ---- --------- -------------------- ----- ------- ------ ------ - - --- -- -- -------\n`
  }

   GetFileFormat(hex: Hex): string {
    let starSystem = hex.starSystems[0];
    let planet = {} as Planet;
      planet = (starSystem as StarSystem).planets[0];

    let hexNum = this.getCoordinates(hex);
    if (planet == null) {
      return '';
    }
    let uwp = this.GetUWP(planet);

    let b = this.getBases(planet);

    let tradeClassifications = this.getTradeCodes(planet);


    let a = '  ';

    let z = ' ';
    if (planet.travelCode !== TravelCode.None) {
      z = this.GetTravelCode(planet.travelCode);
    }

    let pbg = hex.starSystems[0].gasGiant ? '001' : '000';
    return `${hexNum} ${hexNum} ${uwp} ${tradeClassifications} { 0 } (A46+2) [1716]        ${b} ${z} ${pbg} 1 ${a}        \n`;
  }

   GetHexadecimal(num: number, allowNegative: boolean = true): string {
    if (!allowNegative && num <= 0) {
      return '0';
    } else if (num <= 9) {
      return num.toString();
    }
    switch (num) {
      case 10:
        return 'A';
      case 11:
        return 'B';
      case 12:
        return 'C';
      case 13:
        return 'D';
      case 14:
        return 'E';
      case 15:
        return 'F';
      case 16:
        return 'G';
      case 17:
        return 'H';
      case 18:
        return 'J';
      case 19:
        return 'K';
      case 20:
        return 'L';
      case 21:
        return 'M';
      case 22:
        return 'N';
      case 23:
        return 'P';
      case 24:
        return 'Q';
      case 25:
        return 'R';
      case 26:
        return 'S';
      case 27:
        return 'T';
      case 28:
        return 'U';
      case 29:
        return 'V';
      case 30:
        return 'W';
      default:
        return ' ';
    }
  }

  private  GetTravelCode(travelCode: TravelCode): string {
    switch (travelCode) {
      case TravelCode.None:
        return '';
      case TravelCode.Amber:
        return 'A';
      case TravelCode.Red:
        return 'R';
    }
  }

   getCoordinates(hex: Hex): string {
    return `${hex.coordinates.x.toString().padStart(2, '0')}${hex.coordinates.y.toString().padStart(2, '0')}`
  }

   GetUWP(planet: Planet): string {
    if (planet != null) {
      return `${planet.starport}${this.GetHexadecimal(planet.size, false)}${this.GetHexadecimal(planet.atmosphere, false)}${this.GetHexadecimal(planet.hydrographics, false)}${this.GetHexadecimal(planet.population, false)}${this.GetHexadecimal(planet.government, false)}${this.GetHexadecimal(planet.lawLevel, false)}-${this.GetHexadecimal(planet.techLevel, false)}`;
    }
    return '';
  }

   getTradeCodes(planet: Planet): string {
    if (planet != null) {
      let tradeClassifications = '';

      planet.tradeCodes.forEach(tradeCode => {
        tradeClassifications += `${this.getTradeCode(tradeCode)} `
      });
      return tradeClassifications.trim();
    }
    return '';
  }

  private  getTradeCode(tradeCode: TradeCode): string {
    switch (tradeCode) {
      // Planetary
      case TradeCode.Asteroid:
        return "As";
      case TradeCode.Desert:
        return "De";
      case TradeCode.FluidOceans:
        return "Fl";
      case TradeCode.Garden:
        return "Ga";
      case TradeCode.Hellworld:
        return "He";
      case TradeCode.IceCapped:
        return "Ic";
      case TradeCode.OceanWorld:
        return "Oc";
      case TradeCode.Vacuum:
        return "Va";
      case TradeCode.WaterWorld:
        return "Wa";
        // Population
      case TradeCode.Dieback:
        return "Di";
      case TradeCode.Barren:
        return "Ba";
      case TradeCode.LowPopulation:
        return "Lo";
      case TradeCode.NonIndustrial:
        return "Ni";
      case TradeCode.PreHighPopulation:
        return "Ph";
      case TradeCode.HighPopulation:
        return "Hi";
        // Economic
      case TradeCode.PreAgricultural:
        return "Pa";
      case TradeCode.Agricultural:
        return "Ag";
      case TradeCode.NonAgricultural:
        return "Na";
      case TradeCode.PreIndustrial:
        return "Pi";
      case TradeCode.Industrial:
        return "In";
      case TradeCode.Poor:
        return "Po";
      case TradeCode.PreRich:
        return "Pr";
      case TradeCode.Rich:
        return "Ri";
        // Climate
      case TradeCode.Frozen:
        return "Fr";
      case TradeCode.Hot:
        return "Ho";
      case TradeCode.Cold:
        return "Co";
      case TradeCode.Locked:
        return "Lk";
      case TradeCode.Tropic:
        return "Tr";
      case TradeCode.Tundra:
        return "Tu";
      case TradeCode.TwilightZone:
        return "Tz";
        // Secondary
      case TradeCode.Farming:
        return "Fa";
      case TradeCode.Mining:
        return "Mi";
      case TradeCode.MilitaryRule:
        return "Mr";
      case TradeCode.PrisonCamp:
        return "Px";
      case TradeCode.PenalColony:
        return "Pe";
      case TradeCode.Reserve:
        return "Re";
        // Political
      case TradeCode.SubsectorCapital:
        return "Cp";
      case TradeCode.SectorCapital:
        return "Cs";
      case TradeCode.Capital:
        return "Cx";
      case TradeCode.Colony:
        return "Cy";
        // Special
      case TradeCode.Satellite:
        return "Sa";
      case TradeCode.Forbidden:
        return "Fo";
      case TradeCode.Puzzle:
        return "Pz";
      case TradeCode.Danger:
        return "Da";
      case TradeCode.DataRepository:
        return "Ab";
      case TradeCode.AncientSite:
        return "An";
      case TradeCode.ResearchStation:
        return "Rs";

        // Other
      case TradeCode.HighTechnology:
        return "Ht";
      case TradeCode.LowTechnology:
        return "Lt";
    }
  }

   getBases(planet: Planet): string {
    if (planet != null) {
      let bases = '';

      planet.bases.forEach(base => {
        bases += this.getBase(base);
      });
      return bases;
    }
    return '';
  }

  private  getBase(base: Base): string {
    switch (base) {
      case Base.Naval:
        return 'N';
      case Base.Scout:
        return 'S';
      case Base.Research:
        return 'R';
      default:
        return ' ';
    }
  }

   GetIx(planet: Planet): number {
    let importance = 0;

    if(planet.starport == 'A' || planet.starport == 'B'){
      importance++;
    } else if (planet.starport == 'D' || planet.starport == 'X'){
      importance--;
    }

    if(planet.techLevel >= 16){
      importance++;
    }
    if(planet.techLevel >= 10) {
      importance++;
    }
    if(planet.techLevel <= 8){
      importance--;
    }

    if(planet.tradeCodes.includes(TradeCode.Agricultural)){
      importance++;
    }
    if(planet.tradeCodes.includes(TradeCode.HighPopulation)){
      importance++;
    }
    if(planet.tradeCodes.includes(TradeCode.Industrial)){
      importance++;
    }
    if(planet.tradeCodes.includes(TradeCode.Rich)){
      importance++;
    }

    if(planet.population <= 6){
      importance--;
    }

    if(planet.bases.includes(Base.Naval) && planet.bases.includes(Base.Scout)){
      importance++;
    }

    if(planet.bases.includes(Base.WayStation)){
      importance++;
    }

    return importance;
  }

   GetEx(planet: Planet): string {
    return "";
  }

   GetCx(planet: Planet): string {
    return "";
  }

   GetN(planet: Planet): string {
    return "";
  }

   GetZ(planet: Planet): string {
    return "";
  }

   GetPBG(hex: Hex): string {
    return `00${hex != null && hex.starSystems.length > 0 && hex.starSystems[0].gasGiant ? '1' : '0'}`
  }

   GetAllegiance(planet: Planet): string {
    return "";
  }

   GetStellar(planet: Planet): string {
    return "";
  }
}
