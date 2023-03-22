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
    BenefitName: `${this._skillService.SkillName.Admin}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Admin]
  } as Training;
  advocateTraining = {
    BenefitName: `${this._skillService.SkillName.Advocate}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Advocate]
  } as Training;
  animalsTraining = {
    BenefitName: `${this._skillService.SkillName.Animals}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.AnimalsHandling,
      this._skillService.SkillName.AnimalsTraining,
      this._skillService.SkillName.AnimalsVeterinary,
    ]
  } as Training;
  artTraining = {
    BenefitName: `${this._skillService.SkillName.Art}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.ArtPerformer,
      this._skillService.SkillName.ArtHolography,
      this._skillService.SkillName.ArtInstrument,
      this._skillService.SkillName.ArtVisualMedia,
      this._skillService.SkillName.ArtWrite,
    ]
  } as Training;
  astrogationTraining = {
    BenefitName: `${this._skillService.SkillName.Astrogation}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Astrogation]
  } as Training;
  athleticsTraining = {
    BenefitName: `${this._skillService.SkillName.Athletics}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.AthleticsStrength,
      this._skillService.SkillName.AthleticsDexterity,
      this._skillService.SkillName.AthleticsEndurance,
    ]
  } as Training;
  athleticsStrengthTraining = {
    BenefitName: `${this._skillService.SkillName.Athletics} (${this._skillService.SkillName.AthleticsStrength})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.AthleticsStrength]
  } as Training;
  brokerTraining = {
    BenefitName: `${this._skillService.SkillName.Broker}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Broker]
  } as Training;
  carouseTraining = {
    BenefitName: `${this._skillService.SkillName.Carouse}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Carouse]
  } as Training;
  deceptionTraining = {
    BenefitName: `${this._skillService.SkillName.Deception}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Deception]
  } as Training;
  diplomatTraining = {
    BenefitName: `${this._skillService.SkillName.Diplomat}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Diplomat]
  } as Training;
  driveTraining = {
    BenefitName: `${this._skillService.SkillName.Drive}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.DriveHovercraft,
      this._skillService.SkillName.DriveMole,
      this._skillService.SkillName.DriveTrack,
      this._skillService.SkillName.DriveWalker,
      this._skillService.SkillName.DriveWheel,
    ]
  } as Training;
  electronicsTraining = {
    BenefitName: `${this._skillService.SkillName.Electronics}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.ElectronicsComms,
      this._skillService.SkillName.ElectronicsComputers,
      this._skillService.SkillName.ElectronicsRemoteOps,
      this._skillService.SkillName.ElectronicsSensors,
    ]
  } as Training;
  commsTraining = {
    BenefitName: `${this._skillService.SkillName.Electronics} (${this._skillService.SkillName.ElectronicsComms})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.ElectronicsComms]
  } as Training;
  computersTraining = {
    BenefitName: `${this._skillService.SkillName.Electronics} (${this._skillService.SkillName.ElectronicsComputers})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.ElectronicsComputers]
  } as Training;
  sensorsTraining = {
    BenefitName: `${this._skillService.SkillName.Electronics} (${this._skillService.SkillName.ElectronicsSensors})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.ElectronicsSensors]
  } as Training;
  engineerTraining = {
    BenefitName: `${this._skillService.SkillName.Engineer}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.EngineerMDrive,
      this._skillService.SkillName.EngineerJDrive,
      this._skillService.SkillName.EngineerLifeSupport,
      this._skillService.SkillName.EngineerPower
    ]
  } as Training;
  explosivesTraining = {
    BenefitName: `${this._skillService.SkillName.Explosives}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Explosives]
  } as Training;
  flyerTraining = {
    BenefitName: `${this._skillService.SkillName.Flyer}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.FlyerAirship,
      this._skillService.SkillName.FlyerGrav,
      this._skillService.SkillName.FlyerOrnithopter,
      this._skillService.SkillName.FlyerRotor,
      this._skillService.SkillName.FlyerWing,
    ]
  } as Training;
  gamblerTraining = {
    BenefitName: `${this._skillService.SkillName.Gambler}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Gambler]
  } as Training;
  gunnerTraining = {
    BenefitName: `${this._skillService.SkillName.Gunner}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Gunner]
  } as Training;
  gunCombatTraining = {
    BenefitName: `${this._skillService.SkillName.GunCombat}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.GunCombatArchaic,
      this._skillService.SkillName.GunCombatEnergy,
      this._skillService.SkillName.GunCombatSlug,
    ]
  } as Training;
  heavyWeaponsTraining = {
    BenefitName: `${this._skillService.SkillName.HeavyWeapons}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.HeavyWeaponsArtillery,
      this._skillService.SkillName.HeavyWeaponsManPortable,
      this._skillService.SkillName.HeavyWeaponsVehicle,
    ]
  } as Training;
  vehicleTraining = {
    BenefitName: `${this._skillService.SkillName.HeavyWeapons} (${this._skillService.SkillName.HeavyWeaponsVehicle})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.HeavyWeaponsVehicle]
  } as Training;
  investigateTraining = {
    BenefitName: `${this._skillService.SkillName.Investigate}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Investigate]
  } as Training;
  jackOfAllTradesTraining = {
    BenefitName: `${this._skillService.SkillName.JackOfAllTrades}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.JackOfAllTrades]
  } as Training;
  languageTraining = {
    BenefitName: `${this._skillService.SkillName.Language}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.Language1,
      this._skillService.SkillName.Language2,
      this._skillService.SkillName.Language3,
      this._skillService.SkillName.Language4,
      this._skillService.SkillName.Language5,
    ]
  } as Training;
  leadershipTraining = {
    BenefitName: `${this._skillService.SkillName.Leadership}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Leadership]
  } as Training;
  mechanicTraining = {
    BenefitName: `${this._skillService.SkillName.Mechanic}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Mechanic]
  } as Training;
  medicTraining = {
    BenefitName: `${this._skillService.SkillName.Medic}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Medic]
  } as Training;
  meleeTraining = {
    BenefitName: `${this._skillService.SkillName.Melee}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.MeleeBlade,
      this._skillService.SkillName.MeleeBludgeon,
      this._skillService.SkillName.MeleeUnarmed,
    ]
  } as Training;
  unarmedTraining = {
    BenefitName: `${this._skillService.SkillName.Melee} (${this._skillService.SkillName.MeleeUnarmed})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.MeleeUnarmed]
  } as Training;
  bladeTraining = {
    BenefitName: `${this._skillService.SkillName.Melee} (${this._skillService.SkillName.MeleeBlade})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.MeleeBlade]
  } as Training;
  navigationTraining = {
    BenefitName: `${this._skillService.SkillName.Navigation}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Navigation]
  } as Training;
  persuadeTraining = {
    BenefitName: `${this._skillService.SkillName.Persuade}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Persuade]
  } as Training;
  pilotTraining = {
    BenefitName: `${this._skillService.SkillName.Pilot}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.PilotSmallCraft,
      this._skillService.SkillName.PilotSpacecraft,
      this._skillService.SkillName.PilotCapitalShips,
    ]
  } as Training;
  smallCraftTraining = {
    BenefitName: `${this._skillService.SkillName.Pilot} (${this._skillService.SkillName.PilotSmallCraft})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.PilotSmallCraft]
  } as Training;
  spacecraftTraining = {
    BenefitName: `${this._skillService.SkillName.Pilot} (${this._skillService.SkillName.PilotSpacecraft})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.PilotSpacecraft]
  } as Training;
  professionTraining = {
    BenefitName: `${this._skillService.SkillName.Profession}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.Profession1,
      this._skillService.SkillName.Profession2,
      this._skillService.SkillName.Profession3,
      this._skillService.SkillName.Profession4,
      this._skillService.SkillName.Profession5,
    ]
  } as Training;
  reconTraining = {
    BenefitName: `${this._skillService.SkillName.Recon}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Recon]
  } as Training;
  scienceTraining = {
    BenefitName: `${this._skillService.SkillName.Science}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.ScienceArchaeology,
      this._skillService.SkillName.ScienceAstronomy,
      this._skillService.SkillName.ScienceBiology,
      this._skillService.SkillName.ScienceChemistry,
      this._skillService.SkillName.ScienceCosmology,
      this._skillService.SkillName.ScienceCybernetics,
      this._skillService.SkillName.ScienceEconomics,
      this._skillService.SkillName.ScienceGenetics,
      this._skillService.SkillName.ScienceHistory,
      this._skillService.SkillName.ScienceLinguistics,
      this._skillService.SkillName.SciencePhilosophy,
      this._skillService.SkillName.SciencePhysics,
      this._skillService.SkillName.SciencePlanetology,
      this._skillService.SkillName.SciencePsionicology,
      this._skillService.SkillName.SciencePsychology,
      this._skillService.SkillName.ScienceRobotics,
      this._skillService.SkillName.ScienceSophontology,
      this._skillService.SkillName.ScienceXenology,
    ]
  } as Training;
  seafarerTraining = {
    BenefitName: `${this._skillService.SkillName.Seafarer}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.SeafarerOceanShips,
      this._skillService.SkillName.SeafarerPersonal,
      this._skillService.SkillName.SeafarerSail,
      this._skillService.SkillName.SeafarerSubmarine,
    ]
  } as Training;
  stealthTraining = {
    BenefitName: `${this._skillService.SkillName.Stealth}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Stealth]
  } as Training;
  stewardTraining = {
    BenefitName: `${this._skillService.SkillName.Steward}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Steward]
  } as Training;
  streetwiseTraining = {
    BenefitName: `${this._skillService.SkillName.Streetwise}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Streetwise]
  } as Training;
  survivalTraining = {
    BenefitName: `${this._skillService.SkillName.Survival}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.Survival]
  } as Training;
  tacticsTraining = {
    BenefitName: `${this._skillService.SkillName.Tactics}`,
    Type: 'skill',
    SkillNames: [
      this._skillService.SkillName.TacticsMilitary,
      this._skillService.SkillName.TacticsNaval,
    ]
  } as Training;
  militaryTraining = {
    BenefitName: `${this._skillService.SkillName.Tactics} (${this._skillService.SkillName.TacticsMilitary})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.TacticsMilitary]
  } as Training;
  navalTraining = {
    BenefitName: `${this._skillService.SkillName.Tactics} (${this._skillService.SkillName.TacticsNaval})`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.TacticsNaval]
  } as Training;
  vaccSuitTraining = {
    BenefitName: `${this._skillService.SkillName.VaccSuit}`,
    Type: 'skill',
    SkillNames: [this._skillService.SkillName.VaccSuit]
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
