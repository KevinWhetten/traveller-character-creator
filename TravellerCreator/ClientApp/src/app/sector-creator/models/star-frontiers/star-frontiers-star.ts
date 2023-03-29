import {IStar} from "../interfaces/star";
import {Luminosity} from "../enums/luminosity";

export class StarFrontiersStar implements IStar {
  luminosity: Luminosity;
  spectralSubclass: number;
}
