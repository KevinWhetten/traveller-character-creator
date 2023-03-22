import {Coordinates} from "../hex/coordinates";

export interface Race {
  name: string;
  allegianceCode: string;
  techLevel: number;
  expansionLevel: number;
  homeworldCoordinates: Coordinates;
}
