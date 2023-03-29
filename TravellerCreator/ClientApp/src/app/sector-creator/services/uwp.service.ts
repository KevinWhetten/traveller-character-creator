import {Injectable} from '@angular/core';
import {IHex} from "../models/interfaces/hex";
import {TravelCode} from "../models/enums/travel-code";
import {StarSystemType} from "../models/enums/star-system-type";
import {IPlanet} from "../models/interfaces/planet";
import {Base} from "../models/enums/base";
import {BasicStarSystem} from "../models/basic/basic-star-system";

@Injectable({
  providedIn: 'root'
})
export class UWPService {

  constructor() {
  }

  static GetFileFormatHeader(): string {
    return `Hex  Name UWP       Remarks              {Ix}  (Ex)    [Cx]   N      B Z PBG W  A  Stellar
---- ---- --------- -------------------- ----- ------- ------ ------ - - --- -- -- -------\n`
  }

  static GetFileFormat(hex: IHex): string {
    let starSystem = hex.starSystems[0];
    let planet = {} as IPlanet;
    if(starSystem.type == StarSystemType.Basic)
    {
      planet = (starSystem as BasicStarSystem).planet;
    }

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

  static GetHexadecimal(num: number): string {
    if (num <= 9) {
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
      default:
        return ' ';
    }
  }

  private static GetTravelCode(travelCode: TravelCode): string {
    switch (travelCode) {
      case TravelCode.None:
        return '';
      case TravelCode.Amber:
        return 'A';
      case TravelCode.Red:
        return 'R';
    }
  }

  static getCoordinates(hex: IHex): string {
    return `${hex.coordinates.x.toString().padStart(2, '0')}${hex.coordinates.y.toString().padStart(2, '0')}`
  }

  static GetUWP(planet: IPlanet): string {
    if (planet != null) {
      return `${planet.starport}${this.GetHexadecimal(planet.size)}${this.GetHexadecimal(planet.atmosphere)}${this.GetHexadecimal(planet.hydrographics)}${this.GetHexadecimal(planet.population)}${this.GetHexadecimal(planet.government)}${this.GetHexadecimal(planet.lawLevel)}-${this.GetHexadecimal(planet.techLevel)}`;
    }
    return '';
  }

  static getTradeCodes(planet: IPlanet): string {
    if (planet != null) {
      let tradeClassifications = '';

      planet.tradeCodes.forEach(tradeCode => {
        tradeClassifications += `${this.getTradeCode(tradeCode.toString())} `
      });
      return tradeClassifications.trim();
    }
    return '';
  }

  private static getTradeCode(tradeCode: string): string {
    switch (Number.parseInt(tradeCode)) {
      case 0:
        return 'Ag';
      case 1:
        return 'As';
      case 2:
        return 'Ba';
      case 3:
        return 'De';
      case 4:
        return 'Fl';
      case 5:
        return 'Ga';
      case 6:
        return 'Hi';
      case 7:
        return 'Ht';
      case 8:
        return 'Ic';
      case 9:
        return 'In';
      case 10:
        return 'Lo';
      case 11:
        return 'Lt';
      case 12:
        return 'Na';
      case 13:
        return 'Ni';
      case 14:
        return 'Po';
      case 15:
        return 'Ri';
      case 16:
        return 'Wa';
      case 17:
        return 'Va';
      default:
        return '';
    }
  }

  static getBases(planet: IPlanet): string {
    if (planet != null) {
      let bases = '';

      planet.bases.forEach(base => {
        bases += UWPService.getBase(base);
      });
      return bases;
    }
    return '';
  }

  private static getBase(base: Base): string {
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

  static GetIx(planet: IPlanet): string {
    return "";
  }

  static GetEx(planet: IPlanet): string {
    return "";
  }

  static GetCx(planet: IPlanet): string {
    return "";
  }

  static GetN(planet: IPlanet): string {
    return "";
  }

  static GetZ(planet: IPlanet): string {
    return "";
  }

  static GetPBG(hex: IHex): string {
    return `00${hex != null && hex.starSystems.length > 0 && hex.starSystems[0].gasGiant ? '1' : '0'}`
  }

  static GetAllegiance(planet: IPlanet): string {
    return "";
  }

  static GetStellar(planet: IPlanet): string {
    return "";
  }
}
