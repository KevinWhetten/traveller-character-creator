import {IStar} from "../interfaces/star";
import {Luminosity} from "../enums/luminosity";
import {RttWorldgenStarType} from "../enums/rttworldgen-star-type";
import {SpectralType} from "../enums/spectral-type";
import {CompanionOrbit} from "../enums/companion-orbit";

export class RttWorldgenStar implements IStar {
  id: string;
  rttWorldgenStarType: RttWorldgenStarType;
  spectralType: SpectralType;
  luminosity: Luminosity;
  spectralSubclass: number;
  companionOrbit: CompanionOrbit;
  expansionSize: number;
  age: number;
}
