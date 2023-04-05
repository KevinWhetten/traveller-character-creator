import {IStar} from "../interfaces/star";
import {Luminosity} from "../enums/luminosity";
import {RTTWorldgenStarType} from "../enums/rttworldgen-star-type";
import {SpectralType} from "../enums/spectral-type";
import {CompanionOrbit} from "../enums/companion-orbit";

export class RTTWorldgenStar implements IStar {
  id: string;
  rttWorldgenStarType: RTTWorldgenStarType;
  spectralType: SpectralType;
  luminosity: Luminosity;
  spectralSubclass: number;
  companionOrbit: CompanionOrbit;
  expansionSize: number;
  age: number;
}
