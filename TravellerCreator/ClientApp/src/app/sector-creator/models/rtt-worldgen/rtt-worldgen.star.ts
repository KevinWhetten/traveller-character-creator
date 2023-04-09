import {StarType} from "../enums/star-type";
import {CompanionOrbit} from "../enums/companion-orbit";
import {Star} from "../basic/star";
import {Luminosity} from "../enums/luminosity";

export class RTTWorldgenStar extends Star {
  id: string;
  rttWorldgenStarType: StarType;
  luminosity: Luminosity;
  companionOrbit: CompanionOrbit;
  expansionSize: number;
  age: number;
}
