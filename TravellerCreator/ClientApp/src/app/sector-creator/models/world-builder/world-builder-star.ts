import {SpectralType} from "./Enums/spectral-type";
import {LuminosityClass} from "./Enums/luminosity-class";
import {StarSpecialType} from "./Enums/star-special-type";
import {StarType} from "./Enums/star-type";

export class WorldBuilderStar {
  component: string;
  spectralType: SpectralType;
  spectralSubclass: number;
  luminosityClass: LuminosityClass;
  specialType: StarSpecialType;
  starType: StarType;
  mass: number;
  temperature: number;
  diameter: number;
  luminosity: number;
  age: number;
  companionStar: WorldBuilderStar;
  orbitNumber: number;
  eccentricity: number;
  orbitDistance: number;
  minimumSeparation: number;
  maximumSeparation: number;
  mao: number;
  hzco: number;
  name: string;
  class: string;
  period: number;
}
