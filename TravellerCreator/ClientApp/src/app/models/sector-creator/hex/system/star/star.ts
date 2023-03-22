import {StarType} from "./star-type";
import {SpectralType} from "./spectral-type";
import {Luminosity} from "./luminosity";
import {Orbit} from "./orbit";
import {System} from "../system";

export interface Star {
  system: System;

  type: StarType;
  spectralType: SpectralType;
  spectrum: number;
  luminosity: Luminosity;
  orbit: Orbit;
  expansionSize: number;
  age: number;
}
