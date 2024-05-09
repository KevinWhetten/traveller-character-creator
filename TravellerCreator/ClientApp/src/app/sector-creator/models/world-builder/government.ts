import { Centralization } from "./Enums/centralization";
import {GovernmentAuthority} from "./governmentAuthority";
import {FactionStrength} from "./Enums/factionStrength";
import {JusticeSystem} from "./Enums/justiceSystem";
import {Uniformity} from "./Enums/uniformity";

export class Government {
  code: number;
  relationships: number[];
  centralization: Centralization;
  authorities: GovernmentAuthority[];
  factionStrength: FactionStrength;
  lawLevel: number;
  justiceSystem: JusticeSystem;
  uniformityOfLaw: Uniformity;
  presumptionOfInnocence: boolean;
  deathPenalty: boolean;
  economicLawLevel: number;
  criminalLawLevel: number;
  privateLawLevel: number;
  personalRightsLevel: number;
  techLevel: number;
  lowCommonTechLevel: number;
  id: number;
  enforcement: number;
  militia: number;
  army: number;
  wetNavy: number;
  airForce: number;
  systemDefense: number;
  navy: number;
  marine: number;
  profile: string;
  factionProfile: string;
}
