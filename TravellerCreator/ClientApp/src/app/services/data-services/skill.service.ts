import {Injectable} from '@angular/core';
import {BaseSkill, Skill, Subskill} from 'src/app/models/skill';
import skills from '../../../assets/json/skills/skills.json';


@Injectable({
  providedIn: 'root'
})
export class SkillService {
  SkillName = {
    Admin: 'Admin',
    Advocate: 'Advocate',
    Animals: 'Animals',
    AnimalsHandling: 'Handling',
    AnimalsTraining: 'Training',
    AnimalsVeterinary: 'Veterinary',
    Art: 'Art',
    ArtPerformer: 'Performer',
    ArtHolography: 'Holography',
    ArtInstrument: 'Instrument',
    ArtVisualMedia: 'Visual Media',
    ArtWrite: 'Write',
    Astrogation: 'Astrogation',
    Athletics: 'Athletics',
    AthleticsStrength: 'Strength',
    AthleticsDexterity: 'Dexterity',
    AthleticsEndurance: 'Endurance',
    Broker: 'Broker',
    Carouse: 'Carouse',
    Deception: 'Deception',
    Diplomat: 'Diplomat',
    Drive: 'Drive',
    DriveHovercraft: 'Hovercraft',
    DriveMole: 'Mole',
    DriveTrack: 'Track',
    DriveWalker: 'Walker',
    DriveWheel: 'Wheel',
    Electronics: 'Electronics',
    ElectronicsComms: 'Comms',
    ElectronicsComputers: 'Computers',
    ElectronicsRemoteOps: 'Remote Ops',
    ElectronicsSensors: 'Sensors',
    Engineer: 'Engineer',
    EngineerMDrive: 'M-Drive',
    EngineerJDrive: 'J-Drive',
    EngineerLifeSupport: 'Life Support',
    EngineerPower: 'Power',
    Explosives: 'Explosives',
    Flyer: 'Flyer',
    FlyerAirship: 'Airship',
    FlyerGrav: 'Grav',
    FlyerOrnithopter: 'Ornithopter',
    FlyerRotor: 'Rotor',
    FlyerWing: 'Wing',
    Gambler: 'Gambler',
    Gunner: 'Gunner',
    GunnerTurret: 'Turret',
    GunnerOrtillery: 'Ortillery',
    GunnerScreen: 'Screen',
    GunnerCapital: 'Capital',
    GunCombat: 'Gun Combat',
    GunCombatArchaic: 'Archaic',
    GunCombatEnergy: 'Energy',
    GunCombatSlug: 'Slug',
    HeavyWeapons: 'Heavy Weapons',
    HeavyWeaponsArtillery: 'Artillery',
    HeavyWeaponsManPortable: 'Man Portable',
    HeavyWeaponsVehicle: 'Vehicle',
    Investigate: 'Investigate',
    JackOfAllTrades: 'Jack-of-All-Trades',
    Language: 'Language',
    Language1: 'Language1',
    Language2: 'Language2',
    Language3: 'Language3',
    Language4: 'Language4',
    Language5: 'Language5',
    Leadership: 'Leadership',
    Mechanic: 'Mechanic',
    Medic: 'Medic',
    Melee: 'Melee',
    MeleeUnarmed: 'Unarmed',
    MeleeBlade: 'Blade',
    MeleeBludgeon: 'Bludgeon',
    Navigation: 'Navigation',
    Persuade: 'Persuade',
    Pilot: 'Pilot',
    PilotSmallCraft: 'Small Craft',
    PilotSpacecraft: 'Spacecraft',
    PilotCapitalShips: 'Capital Ships',
    Profession: 'Profession',
    Profession1: 'Profession1',
    Profession2: 'Profession2',
    Profession3: 'Profession3',
    Profession4: 'Profession4',
    Profession5: 'Profession5',
    Recon: 'Recon',
    Science: 'Science',
    ScienceArchaeology: 'Archaeology',
    ScienceAstronomy: 'Astronomy',
    ScienceBiology: 'Biology',
    ScienceChemistry: 'Chemistry',
    ScienceCosmology: 'Cosmology',
    ScienceCybernetics: 'Cybernetics',
    ScienceEconomics: 'Economics',
    ScienceGenetics: 'Genetics',
    ScienceHistory: 'History',
    ScienceLinguistics: 'Linguistics',
    SciencePhilosophy: 'Philosophy',
    SciencePhysics: 'Physics',
    SciencePlanetology: 'Planetology',
    SciencePsionicology: 'Psionicology',
    SciencePsychology: 'Psychology',
    ScienceRobotics: 'Robotics',
    ScienceSophontology: 'Sophontology',
    ScienceXenology: 'Xenology',
    Seafarer: 'Seafarer',
    SeafarerOceanShips: 'Ocean Ships',
    SeafarerPersonal: 'Personal',
    SeafarerSail: 'Sail',
    SeafarerSubmarine: 'Submarine',
    Stealth: 'Stealth',
    Steward: 'Steward',
    Streetwise: 'Streetwise',
    Survival: 'Survival',
    Tactics: 'Tactics',
    TacticsMilitary: 'Military',
    TacticsNaval: 'Naval',
    VaccSuit: 'Vacc-Suit'
  };
  skills: Skill[] = skills;
  SkillNames = ['Admin', 'Advocate', 'Animals', 'Handling', 'Training', 'Veterinary', 'Art', 'Performer', 'Holography',
    'Instrument', 'Visual Media', 'Write', 'Astrogation', 'Athletics', 'Strength', 'Dexterity', 'Endurance', 'Broker',
    'Carouse', 'Deception', 'Diplomat', 'Drive', 'Hovercraft', 'Mole', 'Track', 'Walker', 'Wheel', 'Electronics', 'Comms',
    'Computers', 'Remote Ops', 'Sensors', 'Engineer', 'M-Drive', 'J-Drive', 'Life Support', 'Power', 'Explosives',
    'Flyer', 'Airship', 'Grav', 'Ornithopter', 'Rotor', 'Wing', 'Gambler', 'Gunner', 'Turret', 'Ortillery', 'Screen',
    'Capital', 'Gun Combat', 'Archaic', 'Energy', 'Slug', 'Heavy Weapons', 'Artillery', 'Man Portable', 'Vehicle',
    'Investigate', 'Jack-of-All-Trades', 'Language', 'Language1', 'Language2', 'Language3', 'Language4', 'Language5',
    'Leadership', 'Mechanic', 'Medic', 'Melee', 'Unarmed', 'Blade', 'Bludgeon', 'Navigation', 'Persuade', 'Pilot',
    'Small Craft', 'Spacecraft', 'Capital Ships', 'Profession', 'Profession1', 'Profession2', 'Profession3', 'Profession4',
    'Profession5', 'Recon', 'Science', 'Archaeology', 'Astronomy', 'Biology', 'Chemistry', 'Cosmology', 'Cybernetics',
    'Economics', 'Genetics', 'History', 'Linguistics', 'Philosophy', 'Physics', 'Planetology', 'Psionicology', 'Psychology',
    'Robotics', 'Sophontology', 'Xenology', 'Seafarer', 'Ocean Ships', 'Personal', 'Sail', 'Submarine', 'Stealth',
    'Steward', 'Streetwise', 'Survival', 'Tactics', 'Military', 'Naval', 'Vacc Suit'];

  constructor() {
  }

  getSkill(skillName: string): Skill {
    let skill = this.skills.find(x => x.Name == skillName);
    return skill ? skill : {} as Skill;
  }

  getDescription(skillName: string): string {
    let skill = this.skills.find(x => x.Name == skillName);
    let Description = '';
    if (skill) {
      Description = skill.Description;
    }
    return Description ? Description : '';
  }

  getGroups(skills: string[]): Record<string, string[]> {
    let groups = {} as Record<string, string[]>;
    for (let skillName of skills) {
      let skill = this.getSkill(skillName);
      if ((skill as Subskill).ParentSkill) {
        let subskill = skill as Subskill;
        if (groups[subskill.ParentSkill]) {
          groups[subskill.ParentSkill].push(subskill.Name);
        } else {
          groups[subskill.ParentSkill] = [subskill.Name];
        }
      } else if ((skill as BaseSkill).Subskills) {
        let baseSkill = skill as BaseSkill;
        if (!groups[baseSkill.Name]) {
          groups[baseSkill.Name] = [];
        }
      } else {
        groups[skill.Name] = [skill.Name];
      }
    }
    return groups;
  }

  getGroupNames(skills: string[]): string[] {
    let groupNames = [] as string[];

    for (let skillName of skills) {
      let skill = this.getSkill(skillName);
      if ((skill as Subskill).ParentSkill) {
        let subskill = skill as Subskill;
        if (groupNames.indexOf(subskill.ParentSkill) < 0) {
          groupNames.push(subskill.ParentSkill);
        }
      } else if ((skill as BaseSkill).Subskills) {
        let baseSkill = skill as BaseSkill;
        if (groupNames.indexOf(baseSkill.Name) < 0) {
          groupNames.push(skill.Name);
        }
      } else {
        if (groupNames.indexOf(skill.Name) < 0) {
          groupNames.push(skill.Name);
        }
      }
    }
    return groupNames;
  }

  getBasicGroups(skills: string[]): Record<string, string[]> {
    let groups = {} as Record<string, string[]>;
    groups['Basic Skills'] = [];

    for (let skillName of skills) {
      let skill = this.getSkill(skillName);

      if ((skill as Subskill).ParentSkill) {
        // Do nothing
      } else {
        groups['Basic Skills'].push(skill.Name);
      }
    }

    return groups;
  }
}
