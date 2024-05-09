import {StarportClass} from "./Enums/starport-class";

export class WorldBuilderStarport {
  class: StarportClass;
  highport: boolean;
  dailyTraffic: number;
  dailyMaximum: number;
  expectedWeekly: number;
  highportTotalDockingCapacity: number;
  downportTotalDockingCapacity: number;
  shipyardBuildCapacity: number;
  annualShipyardOutput: number;
}
