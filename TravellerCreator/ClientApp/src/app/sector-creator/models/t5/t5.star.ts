import {IStar} from "../interfaces/star";
import {Luminosity} from "../enums/luminosity";

export class T5Star implements IStar{
  luminosity: Luminosity;
  spectralSubclass: number;
}
