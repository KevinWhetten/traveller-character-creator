import {Coordinates} from "./coordinates";
import {AtmosphereTaint} from "./atmosphere-taint";
import {PlanetElement} from "./planet-element";
import {Government} from "./government";
import {City} from "./city";
import {WorldBuilderStarport} from "./world-builder-starport";
import {WorldBuilderMoon} from "./world-builder-moon";
import {PlanetType} from "./Enums/planet-type";
import {PlanetAnomaly} from "./Enums/planet-anomaly";
import {TravelZone} from "./Enums/travel-zone";
import {Taint} from "./Enums/taint";
import {UnusualSubtype} from "./Enums/unusual-subtype";
import {Geography} from "./Enums/geography";
import {SurfaceDistribution} from "./Enums/surface-distribution";
import {TariffRate} from "./Enums/tariff-rate";

export class WorldBuilderPlanet {
  // Basic
  name: string;
  coordinates: Coordinates;
  pbg: string;
  isMoon: boolean;
  planetType: PlanetType;
  primary: string;
  object: string;
  anomaly: PlanetAnomaly;
  belts: number;
  gasGiants: number;
  sah: string;
  modifiedSAH: string;
  uwp: string;
  details: string;
  stellarData: string;
  allegiance: string;
  worlds: number;
  bases: string[];
  nobility: string;
  notes: string;

  // Travel Zone
  travelZone: TravelZone;

  // Starport
  starport: WorldBuilderStarport;

  // Size
  size: number;
  diameter: number;
  density: number;
  mass: number;
  gravity: number;
  escapeVelocity: number;
  orbitalVelocity: number;
  sizeProfile: string;

  // Atmosphere
  atmosphere: number;
  atmosphereSub: number;
  bar: number;
  oxygenFraction: number;
  partialPressureOfOxygen: number;
  atmosphereTaints: AtmosphereTaint[];
  atmosphereHazards: Taint[];
  scaleHeight: number;
  unusualSubtypes: UnusualSubtype[];
  atmosphereChemicals: PlanetElement[];
  runawayGreenhouse: boolean;

  // Temperature
  temperature: number;
  highTemperature: number;
  lowTemperature: number;
  albedo: number;
  greenhouseFactor: number;

  // Hydrographics
  hydrographics: number;
  hydroPercent: number;
  fundamentalGeography: Geography;
  liquidChemicals: PlanetElement[];
  surfaceDistribution: SurfaceDistribution;
  majorContinents: number;
  minorContinents: number;

  // Population
  population: number;
  pValue: number;
  populationConcentrationRating: number;
  urbanizationPercentage: number;
  totalUrbanPopulation: number;
  majorCities: City[];
  majorCityPopulation: number;
  totalWorldPopulation: number;

  // Government
  government: number;
  governments: Government[];
  relationships: string;

  // Law Level
  lawLevel: number;

  // TechLevel
  techLevel: number;
  lowCommonTechLevel: number;
  energyTechLevel: number;
  electronicsTechLevel: number;
  manufacturingTechLevel: number;
  medicalTechLevel: number;
  environmentalTechLevel: number;
  landTransportTechLevel: number;
  waterTransportTechLevel: number;
  airTransportTechLevel: number;
  personalMilitaryTechLevel: number;
  heavyMilitaryTechLevel: number;

  // Orbit
  orbitNumber: number;
  orbitDistance: number;
  eccentricity: number;
  period: number;
  nearAU: number;
  farAU: number;

  // Rotation
  siderealDay: number;
  tidalForce: number;
  axialTilt: number;
  isTidallyLocked: boolean;
  isTidallyLockedWithMoon: boolean;
  starTidalEffect: number;
  moonTidalEffect: number;

  // Semiology
  residualSeismicStress: number;
  tidalStressFactor: number;
  tidalHeatingFactor: number;
  totalSeismicStress: number;
  majorTectonicPlates: number;

  // Moons
  hillSphere: number;
  hillSphereDistance: number;
  hillSphereMoonLimit: number;
  moonOrbitRange: number;
  moons: WorldBuilderMoon[];
  sub: number;

  // Biology
  biomassRating: number;
  biocomplexityRating: number;
  currentSophontExists: boolean;
  extinctSophontExists: boolean;
  biodiversityRating: number;
  compatibilityRating: number;
  habitabilityRating: number;

  // Culture
  diversity: number;
  xenophilia: number;
  uniqueness: number;
  symbology: number;
  cohesion: number;
  progressiveness: number;
  expansionism: number;
  militancy: number;
  culturalExtension: string;

  // Economic
  resourceRating: number;
  tradeCodes: string[];
  importance: number;
  importanceExtension: string[];
  resourceFactor: number;
  laborFactor: number;
  infrastructureFactor: number;
  efficiencyFactor: number;
  resourceUnits: number;
  gwpPerCapita: number;
  totalGWP: number;
  wtnStarportModifier: number;
  worldTradeNumber: number;
  inequalityRating: number;
  developmentScore: number;
  tariffRates: TariffRate;
  tariffPercentage: number;
  economicExtension: string;

  // Military
  militaryBudgetPercent: number;
  militaryBudget: number;
}
