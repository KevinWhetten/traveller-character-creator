import {Injectable} from '@angular/core';
import {IHex} from "../models/interfaces/hex";
import {TravelCode} from "../models/enums/travel-code";
import {StarSystemType} from "../models/enums/star-system-type";
import {IPlanet} from "../models/interfaces/planet";
import {Base} from "../models/enums/base";
import {MongooseStarSystem} from "../models/mongoose/mongoose-star-system";
import {TradeCode} from "../models/enums/trade-code";
import {RollingService} from "../../services/rolling.service";

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

   GetFileFormat(hex: IHex): string {
    let starSystem = hex.starSystems[0];
    let planet = {} as IPlanet;
    if(starSystem.type == StarSystemType.Mongoose)
    {
      planet = (starSystem as MongooseStarSystem).planets[0];
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

   getCoordinates(hex: IHex): string {
    return `${hex.coordinates.x.toString().padStart(2, '0')}${hex.coordinates.y.toString().padStart(2, '0')}`
  }

   GetUWP(planet: IPlanet): string {
    if (planet != null) {
      return `${planet.starport}${this.GetHexadecimal(planet.size, false)}${this.GetHexadecimal(planet.atmosphere, false)}${this.GetHexadecimal(planet.hydrographics, false)}${this.GetHexadecimal(planet.population, false)}${this.GetHexadecimal(planet.government, false)}${this.GetHexadecimal(planet.lawLevel, false)}-${this.GetHexadecimal(planet.techLevel, false)}`;
    }
    return '';
  }

   getTradeCodes(planet: IPlanet): string {
    if (planet != null) {
      let tradeClassifications = '';

      planet.tradeCodes.forEach(tradeCode => {
        tradeClassifications += `${this.getTradeCode(tradeCode.toString())} `
      });
      return tradeClassifications.trim();
    }
    return '';
  }

  private  getTradeCode(tradeCode: string): string {
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

   getBases(planet: IPlanet): string {
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

   GetIx(planet: IPlanet): number {
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

   GetEx(planet: IPlanet): string {
    return "";
  }

   GetCx(planet: IPlanet): string {
    return "";
  }

   GetN(planet: IPlanet): string {
    return "";
  }

   GetZ(planet: IPlanet): string {
    return "";
  }

   GetPBG(hex: IHex): string {
    return `00${hex != null && hex.starSystems.length > 0 && hex.starSystems[0].gasGiant ? '1' : '0'}`
  }

   GetAllegiance(planet: IPlanet): string {
    return "";
  }

   GetStellar(planet: IPlanet): string {
    return "";
  }
}
