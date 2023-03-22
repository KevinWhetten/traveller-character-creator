import {Star} from "./star/star";
import {Planet} from "./planet/planet";
import {Hex} from "../hex";

export interface System {
  hex: Hex;

  stars: Star[];
  planets: Planet[];
}
