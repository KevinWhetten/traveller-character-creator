import { Injectable } from '@angular/core';
import {Star} from "../../../../../models/sector-creator/hex/system/star/star";
import {StarType} from "../../../../../models/sector-creator/hex/system/star/star-type";
import {SpectralType} from "../../../../../models/sector-creator/hex/system/star/spectral-type";
import {Luminosity} from "../../../../../models/sector-creator/hex/system/star/luminosity";
import {Orbit} from "../../../../../models/sector-creator/hex/system/star/orbit";
import {RollingService} from "../../../../data-services/rolling.service";

@Injectable({
  providedIn: 'root'
})
export class StarService {

  constructor(private _rollingService: RollingService) { }

  getNewBrownDwarfStar(): Star {
    let star = {} as Star;

    star.type = StarType.BrownDwarf;
    star.orbit = this.GetStarOrbit(star.type);
    star.age = this._rollingService.roll(3) - 3;
    star.spectralType = SpectralType.BrownDwarf;
    star.luminosity = Luminosity.BD;
    star.expansionSize = this._rollingService.roll(1);

    return star;
  }

  GetNewPrimaryStar(): Star {
    let roll = this._rollingService.roll(3) - 2;

    let star = this.GetStar(roll, StarType.primary);
    star.orbit = Orbit.primary;
    return star;
  }

  GetNewCompanionStar(): Star {
    let roll = this._rollingService.roll(3) + this._rollingService.roll(1) - 1;

    let star = this.GetStar(roll, StarType.companion);
    star.orbit = this.GetStarOrbit(StarType.companion);
    return star;
  }

  private GetStar(roll: number, type: StarType): Star {
    let star;
    if (roll <= 1) {
      star = this.GetLuminosity(SpectralType.Blue);
    } else if (2 <= roll && roll <= 3) {
      star = this.GetLuminosity(SpectralType.White);
    } else if (4 <= roll && roll <= 5) {
      star = this.GetLuminosity(SpectralType.WhiteDwarf);
    } else if (6 <= roll && roll <= 7) {
      star = this.GetLuminosity(SpectralType.Orange);
    } else if (8 <= roll && roll <= 10) {
      star = this.GetLuminosity(SpectralType.Red);
    } else if (11 <= roll && roll <= 12) {
      star = this.GetLuminosity(SpectralType.Yellow);
    } else if (13 <= roll && roll <= 14) {
      star = this.GetLuminosity(SpectralType.YellowWhite);
    } else {
      star = this.GetLuminosity(SpectralType.BlueWhite);
    }

    star.expansionSize = this._rollingService.roll(1);
    star.type = type;
    return star;
  }

  private GetLuminosity(spectralType: SpectralType): Star {
    let age = this._rollingService.roll(3) - 3;
    let spectrum = this._rollingService.rollBenford();

    switch (spectralType) {
      case SpectralType.Blue:
        let blueRoll = this._rollingService.roll(2) + age;
        if (blueRoll <= 2) {
          return {spectralType: SpectralType.Blue, luminosity: Luminosity.Ia, age: age, spectrum: spectrum} as Star;
        } else if (blueRoll <= 3) {
          return {spectralType: SpectralType.Blue, luminosity: Luminosity.Ib, age: age, spectrum: spectrum} as Star;
        } else if (blueRoll <= 5) {
          return {spectralType: SpectralType.Blue, luminosity: Luminosity.II, age: age, spectrum: spectrum} as Star;
        } else if (blueRoll <= 8) {
          return {spectralType: SpectralType.Blue, luminosity: Luminosity.III, age: age, spectrum: spectrum} as Star;
        } else if (blueRoll <= 12) {
          return {spectralType: SpectralType.Blue, luminosity: Luminosity.IV, age: age, spectrum: spectrum} as Star;
        } else if (blueRoll <= 17) {
          return {spectralType: SpectralType.Blue, luminosity: Luminosity.V, age: age, spectrum: spectrum} as Star;
        } else {
          return {spectralType: SpectralType.Blue, luminosity: Luminosity.VI, age: age, spectrum: spectrum} as Star;
        }
      case SpectralType.BlueWhite:
        let blueWhiteRoll = this._rollingService.roll(2) + age;
        if (blueWhiteRoll <= 3) {
          return {
            spectralType: SpectralType.BlueWhite,
            luminosity: Luminosity.Ia,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (blueWhiteRoll <= 4) {
          return {
            spectralType: SpectralType.BlueWhite,
            luminosity: Luminosity.Ib,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (blueWhiteRoll <= 6) {
          return {
            spectralType: SpectralType.BlueWhite,
            luminosity: Luminosity.II,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (blueWhiteRoll <= 9) {
          return {
            spectralType: SpectralType.BlueWhite,
            luminosity: Luminosity.III,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (blueWhiteRoll <= 13) {
          return {
            spectralType: SpectralType.BlueWhite,
            luminosity: Luminosity.IV,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (blueWhiteRoll <= 18) {
          return {spectralType: SpectralType.BlueWhite, luminosity: Luminosity.V, age: age, spectrum: spectrum} as Star;
        } else {
          return {
            spectralType: SpectralType.BlueWhite,
            luminosity: Luminosity.VI,
            age: age,
            spectrum: spectrum
          } as Star;
        }
      case SpectralType.White:
        let whiteRoll = this._rollingService.roll(2) + age;
        if (whiteRoll <= 4) {
          return {spectralType: SpectralType.White, luminosity: Luminosity.Ia, age: age, spectrum: spectrum} as Star;
        } else if (whiteRoll <= 5) {
          return {spectralType: SpectralType.White, luminosity: Luminosity.Ib, age: age, spectrum: spectrum} as Star;
        } else if (whiteRoll <= 7) {
          return {spectralType: SpectralType.White, luminosity: Luminosity.II, age: age, spectrum: spectrum} as Star;
        } else if (whiteRoll <= 10) {
          return {spectralType: SpectralType.White, luminosity: Luminosity.III, age: age, spectrum: spectrum} as Star;
        } else if (whiteRoll <= 14) {
          return {spectralType: SpectralType.White, luminosity: Luminosity.IV, age: age, spectrum: spectrum} as Star;
        } else if (whiteRoll <= 19) {
          return {spectralType: SpectralType.White, luminosity: Luminosity.V, age: age, spectrum: spectrum} as Star;
        } else {
          return {spectralType: SpectralType.White, luminosity: Luminosity.VI, age: age, spectrum: spectrum} as Star;
        }
      case SpectralType.YellowWhite:
        let yellowWhiteRoll = this._rollingService.roll(2) + age;
        if (yellowWhiteRoll <= 5) {
          return {
            spectralType: SpectralType.YellowWhite,
            luminosity: Luminosity.Ia,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (yellowWhiteRoll <= 6) {
          return {
            spectralType: SpectralType.YellowWhite,
            luminosity: Luminosity.Ib,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (yellowWhiteRoll <= 8) {
          return {
            spectralType: SpectralType.YellowWhite,
            luminosity: Luminosity.II,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (yellowWhiteRoll <= 11) {
          return {
            spectralType: SpectralType.YellowWhite,
            luminosity: Luminosity.III,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (yellowWhiteRoll <= 15) {
          return {
            spectralType: SpectralType.YellowWhite,
            luminosity: Luminosity.IV,
            age: age,
            spectrum: spectrum
          } as Star;
        } else if (yellowWhiteRoll <= 20) {
          return {
            spectralType: SpectralType.YellowWhite,
            luminosity: Luminosity.V,
            age: age,
            spectrum: spectrum
          } as Star;
        } else {
          return {
            spectralType: SpectralType.YellowWhite,
            luminosity: Luminosity.VI,
            age: age,
            spectrum: spectrum
          } as Star;
        }
      case SpectralType.Yellow:
        let yellowRoll = this._rollingService.roll(2) + age;
        if (yellowRoll <= 6) {
          return {spectralType: SpectralType.Yellow, luminosity: Luminosity.Ia, age: age, spectrum: spectrum} as Star;
        } else if (yellowRoll <= 7) {
          return {spectralType: SpectralType.Yellow, luminosity: Luminosity.Ib, age: age, spectrum: spectrum} as Star;
        } else if (yellowRoll <= 9) {
          return {spectralType: SpectralType.Yellow, luminosity: Luminosity.II, age: age, spectrum: spectrum} as Star;
        } else if (yellowRoll <= 12) {
          return {spectralType: SpectralType.Yellow, luminosity: Luminosity.III, age: age, spectrum: spectrum} as Star;
        } else if (yellowRoll <= 16) {
          return {spectralType: SpectralType.Yellow, luminosity: Luminosity.IV, age: age, spectrum: spectrum} as Star;
        } else if (yellowRoll <= 21) {
          return {spectralType: SpectralType.Yellow, luminosity: Luminosity.V, age: age, spectrum: spectrum} as Star;
        } else {
          return {spectralType: SpectralType.Yellow, luminosity: Luminosity.VI, age: age, spectrum: spectrum} as Star;
        }
      case SpectralType.Orange:
        let orangeRoll = this._rollingService.roll(2) + age;
        if (orangeRoll <= 7) {
          return {spectralType: SpectralType.Orange, luminosity: Luminosity.Ia, age: age, spectrum: spectrum} as Star;
        } else if (orangeRoll <= 8) {
          return {spectralType: SpectralType.Orange, luminosity: Luminosity.Ib, age: age, spectrum: spectrum} as Star;
        } else if (orangeRoll <= 10) {
          return {spectralType: SpectralType.Orange, luminosity: Luminosity.II, age: age, spectrum: spectrum} as Star;
        } else if (orangeRoll <= 13) {
          return {spectralType: SpectralType.Orange, luminosity: Luminosity.III, age: age, spectrum: spectrum} as Star;
        } else if (orangeRoll <= 17) {
          return {spectralType: SpectralType.Orange, luminosity: Luminosity.IV, age: age, spectrum: spectrum} as Star;
        } else if (orangeRoll <= 22) {
          return {spectralType: SpectralType.Orange, luminosity: Luminosity.V, age: age, spectrum: spectrum} as Star;
        } else {
          return {spectralType: SpectralType.Orange, luminosity: Luminosity.VI, age: age, spectrum: spectrum} as Star;
        }
      case SpectralType.Red:
        let redRoll = this._rollingService.roll(2) + age;
        if (redRoll <= 8) {
          return {spectralType: SpectralType.Red, luminosity: Luminosity.Ia, age: age, spectrum: spectrum} as Star;
        } else if (redRoll <= 9) {
          return {spectralType: SpectralType.Red, luminosity: Luminosity.Ib, age: age, spectrum: spectrum} as Star;
        } else if (redRoll <= 11) {
          return {spectralType: SpectralType.Red, luminosity: Luminosity.II, age: age, spectrum: spectrum} as Star;
        } else if (redRoll <= 14) {
          return {spectralType: SpectralType.Red, luminosity: Luminosity.III, age: age, spectrum: spectrum} as Star;
        } else if (redRoll <= 18) {
          return {spectralType: SpectralType.Red, luminosity: Luminosity.IV, age: age, spectrum: spectrum} as Star;
        } else if (redRoll <= 23) {
          return {spectralType: SpectralType.Red, luminosity: Luminosity.V, age: age, spectrum: spectrum} as Star;
        } else {
          return {spectralType: SpectralType.Red, luminosity: Luminosity.VI, age: age, spectrum: spectrum} as Star;
        }
      default:
        return {spectralType: SpectralType.WhiteDwarf, luminosity: Luminosity.D, age: age, spectrum: spectrum} as Star;
    }
  }

  private GetStarOrbit(type: StarType) {
    if (type == StarType.companion) {
      let roll = this._rollingService.roll(1);
      if (roll <= 2) {
        return Orbit.tight;
      } else if (3 <= roll && roll <= 4) {
        return Orbit.close;
      } else if (roll == 5) {
        return Orbit.moderate;
      } else {
        return Orbit.distant;
      }
    } else {
      return Orbit.primary;
    }
  }
}
