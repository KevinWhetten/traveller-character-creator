import {Injectable} from '@angular/core';
import {Training} from "../../models/career";
import {SkillService} from "./skill.service";

@Injectable({
  providedIn: 'root'
})
export class TrainingService {
  dummyTraining = {
    BenefitName: ``,
  } as Training;

  // ----- CHARACTERISTIC TRAINING -----
  strengthTraining = {
    BenefitName: `STR +1`,
    Type: 'characteristic',
    SkillNames: ['STR']
  } as Training;
  dexterityTraining = {
    BenefitName: `DEX +1`,
    Type: 'characteristic',
    SkillNames: ['DEX']
  } as Training;
  enduranceTraining = {
    BenefitName: `END +1`,
    Type: 'characteristic',
    SkillNames: ['END']
  } as Training;
  intelligenceTraining = {
    BenefitName: `INT +1`,
    Type: 'characteristic',
    SkillNames: ['INT']
  } as Training;
  educationTraining = {
    BenefitName: `EDU +1`,
    Type: 'characteristic',
    SkillNames: ['EDU']
  } as Training;
  socialStatusTraining = {
    BenefitName: `SOC +1`,
    Type: 'characteristic',
    SkillNames: ['SOC']
  } as Training;
  psiTraining = {
    BenefitName: `PSI +1`,
    Type: 'characteristic',
    SkillNames: ['PSI']
  } as Training;

  // ----- SKILL TRAINING -----
  adminTraining = {
    BenefitName: `${this._skillService.SkillNames.Admin}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Admin]
  } as Training;
  advocateTraining = {
    BenefitName: `${this._skillService.SkillNames.Advocate}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Advocate]
  } as Training;
  animalsTraining = {
    BenefitName: `${this._skillService.SkillNames.Animals}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.AnimalsHandling,
      this._skillService.SkillNames.AnimalsTraining,
      this._skillService.SkillNames.AnimalsVeterinary,
    ]
  } as Training;
  artTraining = {
    BenefitName: `${this._skillService.SkillNames.Art}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.ArtPerformer,
      this._skillService.SkillNames.ArtHolography,
      this._skillService.SkillNames.ArtInstrument,
      this._skillService.SkillNames.ArtVisualMedia,
      this._skillService.SkillNames.ArtWrite,
    ]
  } as Training;
  astrogationTraining = {
    BenefitName: `${this._skillService.SkillNames.Astrogation}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Astrogation]
  } as Training;
  athleticsTraining = {
    BenefitName: `${this._skillService.SkillNames.Athletics}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.AthleticsStrength,
      this._skillService.SkillNames.AthleticsDexterity,
      this._skillService.SkillNames.AthleticsEndurance,
    ]
  } as Training;
  athleticsStrengthTraining = {
    BenefitName: `${this._skillService.SkillNames.Athletics} (${this._skillService.SkillNames.AthleticsStrength})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.AthleticsStrength]
  } as Training;
  brokerTraining = {
    BenefitName: `${this._skillService.SkillNames.Broker}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Broker]
  } as Training;
  carouseTraining = {
    BenefitName: `${this._skillService.SkillNames.Carouse}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Carouse]
  } as Training;
  deceptionTraining = {
    BenefitName: `${this._skillService.SkillNames.Deception}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Deception]
  } as Training;
  diplomatTraining = {
    BenefitName: `${this._skillService.SkillNames.Diplomat}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Diplomat]
  } as Training;
  driveTraining = {
    BenefitName: `${this._skillService.SkillNames.Drive}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.DriveHovercraft,
      this._skillService.SkillNames.DriveMole,
      this._skillService.SkillNames.DriveTrack,
      this._skillService.SkillNames.DriveWalker,
      this._skillService.SkillNames.DriveWheel,
    ]
  } as Training;
  electronicsTraining = {
    BenefitName: `${this._skillService.SkillNames.Electronics}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.ElectronicsComms,
      this._skillService.SkillNames.ElectronicsComputers,
      this._skillService.SkillNames.ElectronicsRemoteOps,
      this._skillService.SkillNames.ElectronicsSensors,
    ]
  } as Training;
  commsTraining = {
    BenefitName: `${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsComms})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.ElectronicsComms]
  } as Training;
  computersTraining = {
    BenefitName: `${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsComputers})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.ElectronicsComputers]
  } as Training;
  sensorsTraining = {
    BenefitName: `${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsSensors})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.ElectronicsSensors]
  } as Training;
  engineerTraining = {
    BenefitName: `${this._skillService.SkillNames.Engineer}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.EngineerMDrive,
      this._skillService.SkillNames.EngineerJDrive,
      this._skillService.SkillNames.EngineerLifeSupport,
      this._skillService.SkillNames.EngineerPower
    ]
  } as Training;
  explosivesTraining = {
    BenefitName: `${this._skillService.SkillNames.Explosives}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Explosives]
  } as Training;
  flyerTraining = {
    BenefitName: `${this._skillService.SkillNames.Flyer}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.FlyerAirship,
      this._skillService.SkillNames.FlyerGrav,
      this._skillService.SkillNames.FlyerOrnithopter,
      this._skillService.SkillNames.FlyerRotor,
      this._skillService.SkillNames.FlyerWing,
    ]
  } as Training;
  gamblerTraining = {
    BenefitName: `${this._skillService.SkillNames.Gambler}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Gambler]
  } as Training;
  gunnerTraining = {
    BenefitName: `${this._skillService.SkillNames.Gunner}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Gunner]
  } as Training;
  gunCombatTraining = {
    BenefitName: `${this._skillService.SkillNames.GunCombat}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.GunCombatArchaic,
      this._skillService.SkillNames.GunCombatEnergy,
      this._skillService.SkillNames.GunCombatSlug,
    ]
  } as Training;
  heavyWeaponsTraining = {
    BenefitName: `${this._skillService.SkillNames.HeavyWeapons}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.HeavyWeaponsArtillery,
      this._skillService.SkillNames.HeavyWeaponsManPortable,
      this._skillService.SkillNames.HeavyWeaponsVehicle,
    ]
  } as Training;
  vehicleTraining = {
    BenefitName: `${this._skillService.SkillNames.HeavyWeapons} (${this._skillService.SkillNames.HeavyWeaponsVehicle})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.HeavyWeaponsVehicle]
  } as Training;
  investigateTraining = {
    BenefitName: `${this._skillService.SkillNames.Investigate}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Investigate]
  } as Training;
  jackOfAllTradesTraining = {
    BenefitName: `${this._skillService.SkillNames.JackOfAllTrades}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.JackOfAllTrades]
  } as Training;
  languageTraining = {
    BenefitName: `${this._skillService.SkillNames.Language}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.Language1,
      this._skillService.SkillNames.Language2,
      this._skillService.SkillNames.Language3,
      this._skillService.SkillNames.Language4,
      this._skillService.SkillNames.Language5,
    ]
  } as Training;
  leadershipTraining = {
    BenefitName: `${this._skillService.SkillNames.Leadership}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Leadership]
  } as Training;
  mechanicTraining = {
    BenefitName: `${this._skillService.SkillNames.Mechanic}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Mechanic]
  } as Training;
  medicTraining = {
    BenefitName: `${this._skillService.SkillNames.Medic}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Medic]
  } as Training;
  meleeTraining = {
    BenefitName: `${this._skillService.SkillNames.Melee}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.MeleeBlade,
      this._skillService.SkillNames.MeleeBludgeon,
      this._skillService.SkillNames.MeleeUnarmed,
    ]
  } as Training;
  unarmedTraining = {
    BenefitName: `${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeUnarmed})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.MeleeUnarmed]
  } as Training;
  bladeTraining = {
    BenefitName: `${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeBlade})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.MeleeBlade]
  } as Training;
  navigationTraining = {
    BenefitName: `${this._skillService.SkillNames.Navigation}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Navigation]
  } as Training;
  persuadeTraining = {
    BenefitName: `${this._skillService.SkillNames.Persuade}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Persuade]
  } as Training;
  pilotTraining = {
    BenefitName: `${this._skillService.SkillNames.Pilot}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.PilotSmallCraft,
      this._skillService.SkillNames.PilotSpacecraft,
      this._skillService.SkillNames.PilotCapitalShips,
    ]
  } as Training;
  smallCraftTraining = {
    BenefitName: `${this._skillService.SkillNames.Pilot} (${this._skillService.SkillNames.PilotSmallCraft})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.PilotSmallCraft]
  } as Training;
  spacecraftTraining = {
    BenefitName: `${this._skillService.SkillNames.Pilot} (${this._skillService.SkillNames.PilotSpacecraft})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.PilotSpacecraft]
  } as Training;
  professionTraining = {
    BenefitName: `${this._skillService.SkillNames.Profession}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.Profession1,
      this._skillService.SkillNames.Profession2,
      this._skillService.SkillNames.Profession3,
      this._skillService.SkillNames.Profession4,
      this._skillService.SkillNames.Profession5,
    ]
  } as Training;
  reconTraining = {
    BenefitName: `${this._skillService.SkillNames.Recon}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Recon]
  } as Training;
  scienceTraining = {
    BenefitName: `${this._skillService.SkillNames.Science}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.ScienceArchaeology,
      this._skillService.SkillNames.ScienceAstronomy,
      this._skillService.SkillNames.ScienceBiology,
      this._skillService.SkillNames.ScienceChemistry,
      this._skillService.SkillNames.ScienceCosmology,
      this._skillService.SkillNames.ScienceCybernetics,
      this._skillService.SkillNames.ScienceEconomics,
      this._skillService.SkillNames.ScienceGenetics,
      this._skillService.SkillNames.ScienceHistory,
      this._skillService.SkillNames.ScienceLinguistics,
      this._skillService.SkillNames.SciencePhilosophy,
      this._skillService.SkillNames.SciencePhysics,
      this._skillService.SkillNames.SciencePlanetology,
      this._skillService.SkillNames.SciencePsionicology,
      this._skillService.SkillNames.SciencePsychology,
      this._skillService.SkillNames.ScienceRobotics,
      this._skillService.SkillNames.ScienceSophontology,
      this._skillService.SkillNames.ScienceXenology,
    ]
  } as Training;
  seafarerTraining = {
    BenefitName: `${this._skillService.SkillNames.Seafarer}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.SeafarerOceanShips,
      this._skillService.SkillNames.SeafarerPersonal,
      this._skillService.SkillNames.SeafarerSail,
      this._skillService.SkillNames.SeafarerSubmarine,
    ]
  } as Training;
  stealthTraining = {
    BenefitName: `${this._skillService.SkillNames.Stealth}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Stealth]
  } as Training;
  stewardTraining = {
    BenefitName: `${this._skillService.SkillNames.Steward}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Steward]
  } as Training;
  streetwiseTraining = {
    BenefitName: `${this._skillService.SkillNames.Streetwise}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Streetwise]
  } as Training;
  survivalTraining = {
    BenefitName: `${this._skillService.SkillNames.Survival}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.Survival]
  } as Training;
  tacticsTraining = {
    BenefitName: `${this._skillService.SkillNames.Tactics}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillNames.TacticsMilitary,
      this._skillService.SkillNames.TacticsNaval,
    ]
  } as Training;
  militaryTraining = {
    BenefitName: `${this._skillService.SkillNames.Tactics} (${this._skillService.SkillNames.TacticsMilitary})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.TacticsMilitary]
  } as Training;
  navalTraining = {
    BenefitName: `${this._skillService.SkillNames.Tactics} (${this._skillService.SkillNames.TacticsNaval})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.TacticsNaval]
  } as Training;
  vaccSuitTraining = {
    BenefitName: `${this._skillService.SkillNames.VaccSuit}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillNames.VaccSuit]
  } as Training;

  // ----- PSIONIC TRAINING -----
  awarenessTraining = {
    BenefitName: `Awareness`,
  } as Training;
  clairvoyanceTraining = {
    BenefitName: `Clairvoyance`,
  } as Training;
  telekinesisTraining = {
    BenefitName: `Telekinesis`,
  } as Training;
  telepathyTraining = {
    BenefitName: `Telepathy`,
  } as Training;
  teleportationTraining = {
    BenefitName: `Teleportation`,
  } as Training;

  constructor(private _skillService: SkillService) {
  }
}
