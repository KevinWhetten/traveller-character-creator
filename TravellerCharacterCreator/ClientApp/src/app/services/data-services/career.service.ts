import {Injectable} from '@angular/core';
import {
  Assignment,
  Benefit,
  Career,
  CareerEvent,
  Mishap,
  Qualification, Rank,
  RanksTable, Training,
  TrainingTable
} from "../../models/career";
import {SkillService} from "./skill.service";
import {TrainingService} from "./training.service";

@Injectable({
  providedIn: `root`
})
export class CareerService {
  private careers: Record<string, Career> = {
    'Agent': {
      Name: `Agent`,
      Designation: '1',
      Description: `Law enforcement agencies, corporate operatives, spies, and others who work in the shadows.`,
      Qualification: {characteristic: `INT`, target: 6} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Law Enforcement`,
          Description: `You are a police officer or detective.`,
          Survival: {characteristic: `END`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Intelligence`,
          Description: `You work as a spy or saboteur.`,
          Survival: {characteristic: `INT`, target: 7} as Qualification,
          Advancement: {characteristic: `INT`, target: 5} as Qualification,
        } as Assignment,
        {
          Name: `Corporate`,
          Description: `You work for a corporation, spying on rival organizations.`,
          Survival: {characteristic: `INT`, target: 5} as Qualification,
          Advancement: {characteristic: `INT`, target: 7} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 1000,
          BenefitName: `Scientific Equipment`,
        } as Benefit,
        2: {
          Cash: 2000,
          BenefitName: `INT +1`,
        } as Benefit,
        3: {
          Cash: 5000,
          BenefitName: `Ship Share`,
        } as Benefit,
        4: {
          Cash: 7500,
          BenefitName: `Weapon`,
        } as Benefit,
        5: {
          Cash: 10000,
          BenefitName: `Combat Implant`,
        } as Benefit,
        6: {
          Cash: 25000,
          BenefitName: `SOC +1 or Combat Implant`,
        } as Benefit,
        7: {
          Cash: 50000,
          BenefitName: `TAS Membership`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Law Enforcement', 'Intelligence', 'Corporate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.gunCombatTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.meleeTraining,
            5: this._trainingService.intelligenceTraining,
            6: this._trainingService.athleticsTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Law Enforcement', 'Intelligence', 'Corporate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.streetwiseTraining,
            2: this._trainingService.driveTraining,
            3: this._trainingService.investigateTraining,
            4: this._trainingService.flyerTraining,
            5: this._trainingService.reconTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min EDU 8)`,
          Assignments: ['Law Enforcement', 'Intelligence', 'Corporate'],
          MinEDU: 8,
          Trainings: {
            1: this._trainingService.advocateTraining,
            2: this._trainingService.languageTraining,
            3: this._trainingService.explosivesTraining,
            4: this._trainingService.medicTraining,
            5: this._trainingService.vaccSuitTraining,
            6: this._trainingService.electronicsTraining
          }
        } as TrainingTable,
        {
          Name: `Law Enforcement`,
          Assignments: ['Law Enforcement'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.investigateTraining,
            2: this._trainingService.reconTraining,
            3: this._trainingService.streetwiseTraining,
            4: this._trainingService.stealthTraining,
            5: this._trainingService.meleeTraining,
            6: this._trainingService.advocateTraining
          }
        } as TrainingTable,
        {
          Name: `Intelligence`,
          Assignments: ['Intelligence'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.investigateTraining,
            2: this._trainingService.reconTraining,
            3: this._trainingService.commsTraining,
            4: this._trainingService.stealthTraining,
            5: this._trainingService.persuadeTraining,
            6: this._trainingService.deceptionTraining
          }
        } as TrainingTable,
        {
          Name: `Corporate`,
          Assignments: ['Corporate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.investigateTraining,
            2: this._trainingService.computersTraining,
            3: this._trainingService.stealthTraining,
            4: this._trainingService.carouseTraining,
            5: this._trainingService.deceptionTraining,
            6: this._trainingService.streetwiseTraining
          }
        } as TrainingTable,
      ],
      RankTables: [
        {
          Name: `Law Enforcement`,
          Assignments: [`Law Enforcement`],
          Ranks: {
            0: {
              Name: `Rookie`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Corporal`,
              Bonus: `Streetwise 1`,
            } as Rank,
            2: {
              Name: `Sergeant`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Detective`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Lieutenant`,
              Bonus: `Investigate 1`,
            } as Rank,
            5: {
              Name: `Chief`,
              Bonus: `Admin 1`,
            } as Rank,
            6: {
              Name: `Commissioner`,
              Bonus: `SOC +1`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Intelligence/Corporate`,
          Assignments: [`Intelligence`, `Corporate`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Agent`,
              Bonus: `Deception 1`,
            } as Rank,
            2: {
              Name: `Field Agent`,
              Bonus: `Investigate 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Special Agent`,
              Bonus: `Gun Combat 1`,
            } as Rank,
            5: {
              Name: `Assistant Director`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Director`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `A criminal or other figure under investigation offers you a deal. Accept, and you leave this career without further penalty (although you lose the Benefit roll as normal). Refuse, and you must roll twice on the Injury Table and take the lower result. You gain an [Enemy] and one level in a skill you choose.`,
        } as Mishap,
        3: {
          Description: `An investigation goes critically wrong or leads to the top, ruining your career. Roll ${this._skillService.SkillNames.Advocate} 8+. If you succeed, you may keep the Benefit roll from this term. If you roll 2, you must take the Prisoner career in your next term.`,
        } as Mishap,
        4: {
          Description: `You learn something you should not know, and people want to kill you for it. Gain an [Enemy] and [${this._skillService.SkillNames.Deception} 1].`,
        } as Mishap,
        5: {
          Description: `Your work ends up coming home with you, and someone gets hurt. Choose one of your Contacts, Allies, or family members, and roll twice on the Injury Table for them, taking the lower result.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `An investigation takes on a dangerous turn. Roll ${this._skillService.SkillNames.Investigate} 8+ or ${this._skillService.SkillNames.Streetwise} 8+. If you fail, roll on the Mishap table. If you succeed, increase one of these skills by one level: [${this._skillService.SkillNames.Deception}], [${this._skillService.SkillNames.JackOfAllTrades}], [${this._skillService.SkillNames.Persuade}], or [${this._skillService.SkillNames.Tactics}]`,
        } as CareerEvent,
        4: {
          Description: `You complete a mission for your superiors and are suitably rewarded. Gain DM+1 to any one Benefit roll from this career.`,
        } as CareerEvent,
        5: {
          Description: `You establish a network of contacts. Gain D3 [Contacts].`,
        } as CareerEvent,
        6: {
          Description: `You are given advanced training in a specialist field. Roll EDU 8+ to increase any one skill you already have by one level.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events table.`,
        } as CareerEvent,
        8: {
          Description: `You go undercover to investigate an enemy. Roll ${this._skillService.SkillNames.Deception} 8+. If you succeed, roll immediately on the Rogue or Citizen Events Table and make one roll on any Specialist skill table for that career. If you fail, roll immediately on the Rogue or Citizen Mishap Table.`,
        } as CareerEvent,
        9: {
          Description: `You go above and beyond the call of duty. Gain DM+2 to your next Advancement check.`,
        } as CareerEvent,
        10: {
          Description: `You are given specialist training in vehicles. Gain one of [${this._skillService.SkillNames.Drive} 1], [${this._skillService.SkillNames.Flyer} 1], [${this._skillService.SkillNames.Pilot} 1], or [${this._skillService.SkillNames.Gunner} 1].`,
        } as CareerEvent,
        11: {
          Description: `You are befriended by a senior agent. Either increase [${this._skillService.SkillNames.Investigate}] by one level or DM+4 to an Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `Your efforts undercover a major conspiracy against your employers. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Army': {
      Name: `Army`,
      Designation: '2',
      Description: `Members of the planetary armed fighting forces. Soldiers deal with planetary surface actions, battles, and campaigns. Such individuals may also be mercenaries for hire.`,
      Qualification: {characteristic: `END`, target: 5} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Support`,
          Description: `You are an engineer, cook, or some other role behind the front lines.`,
          Survival: {characteristic: `END`, target: 5} as Qualification,
          Advancement: {characteristic: `EDU`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: `Infantry`,
          Description: `You are one of the Poor Bloody Infantry on the ground.`,
          Survival: {characteristic: `STR`, target: 6} as Qualification,
          Advancement: {characteristic: `EDU`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Cavalry`,
          Description: `You are one of the crew of a gunship or tank.`,
          Survival: {characteristic: `INT`, target: 7} as Qualification,
          Advancement: {characteristic: `INT`, target: 5} as Qualification,
        } as Assignment,
      ],
      BenefitTable: {
        1: {
          Cash: 2000,
          BenefitName: `Combat Implant`,
        } as Benefit,
        2: {
          Cash: 5000,
          BenefitName: `INT +1`,
        } as Benefit,
        3: {
          Cash: 10000,
          BenefitName: `EDU +1`,
        } as Benefit,
        4: {
          Cash: 10000,
          BenefitName: `Weapon`,
        } as Benefit,
        5: {
          Cash: 10000,
          BenefitName: `Armour`,
        } as Benefit,
        6: {
          Cash: 20000,
          BenefitName: `END +1 or Combat Implant`,
        } as Benefit,
        7: {
          Cash: 30000,
          BenefitName: `SOC +1`,
        } as Benefit,
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Support', 'Infantry', 'Cavalry'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.gamblerTraining,
            5: this._trainingService.medicTraining,
            6: this._trainingService.meleeTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Support', 'Infantry', 'Cavalry'],
          MinEDU: 0,
          Trainings: {
            1: {
              BenefitName: `${this._skillService.SkillNames.Drive} or ${this._skillService.SkillNames.VaccSuit}`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.DriveHovercraft,
                this._skillService.SkillNames.DriveMole,
                this._skillService.SkillNames.DriveTrack,
                this._skillService.SkillNames.DriveWalker,
                this._skillService.SkillNames.DriveWheel,
                this._skillService.SkillNames.VaccSuit,
              ]
            } as Training,
            2: this._trainingService.athleticsTraining,
            3: this._trainingService.gunCombatTraining,
            4: this._trainingService.reconTraining,
            5: this._trainingService.meleeTraining,
            6: this._trainingService.heavyWeaponsTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 8)`,
          Assignments: ['Support', 'Infantry', 'Cavalry'],
          MinEDU: 8,
          Trainings: {
            1: this._trainingService.militaryTraining,
            2: this._trainingService.electronicsTraining,
            3: this._trainingService.navigationTraining,
            4: this._trainingService.explosivesTraining,
            5: this._trainingService.engineerTraining,
            6: this._trainingService.survivalTraining
          }
        } as TrainingTable,
        {
          Name: `Officer (Commissioned Only)`,
          Assignments: ['Support Officer', 'Infantry Officer', 'Cavalry Officer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.militaryTraining,
            2: this._trainingService.leadershipTraining,
            3: this._trainingService.advocateTraining,
            4: this._trainingService.diplomatTraining,
            5: this._trainingService.electronicsTraining,
            6: this._trainingService.adminTraining
          }
        } as TrainingTable,
        {
          Name: `Support`,
          Assignments: ['Support'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.mechanicTraining,
            2: {
              BenefitName: `${this._skillService.SkillNames.Drive} or ${this._skillService.SkillNames.Flyer}`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.DriveHovercraft,
                this._skillService.SkillNames.DriveMole,
                this._skillService.SkillNames.DriveTrack,
                this._skillService.SkillNames.DriveWalker,
                this._skillService.SkillNames.DriveWheel,
                this._skillService.SkillNames.FlyerAirship,
                this._skillService.SkillNames.FlyerGrav,
                this._skillService.SkillNames.FlyerOrnithopter,
                this._skillService.SkillNames.FlyerRotor,
                this._skillService.SkillNames.FlyerWing,
              ]
            } as Training,
            3: this._trainingService.professionTraining,
            4: this._trainingService.explosivesTraining,
            5: this._trainingService.commsTraining,
            6: this._trainingService.medicTraining
          }
        } as TrainingTable,
        {
          Name: `Infantry`,
          Assignments: ['Infantry'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.gunCombatTraining,
            2: this._trainingService.meleeTraining,
            3: this._trainingService.heavyWeaponsTraining,
            4: this._trainingService.stealthTraining,
            5: this._trainingService.athleticsTraining,
            6: this._trainingService.reconTraining
          }
        } as TrainingTable,
        {
          Name: `Cavalry`,
          Assignments: ['Cavalry'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.mechanicTraining,
            2: this._trainingService.driveTraining,
            3: this._trainingService.flyerTraining,
            4: this._trainingService.reconTraining,
            5: this._trainingService.vehicleTraining,
            6: this._trainingService.sensorsTraining
          }
        } as TrainingTable,
      ],
      RankTables: [
        {
          Name: `Enlisted`,
          Assignments: [`Support`, 'Infantry', 'Cavalry'],
          Ranks: {
            0: {
              Name: `Private`,
              Bonus: `${this._skillService.SkillNames.GunCombat} 1`,
            } as Rank,
            1: {
              Name: `Lance Corporal`,
              Bonus: `${this._skillService.SkillNames.Recon} 1`,
            } as Rank,
            2: {
              Name: `Corporal`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Lance Sergeant`,
              Bonus: `${this._skillService.SkillNames.Leadership} 1`,
            } as Rank,
            4: {
              Name: `Sergeant`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Gunnery Sergeant`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Sergeant Major`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Officer`,
          Assignments: [`Support Officer`, 'Infantry Officer', 'Cavalry Officer'],
          Ranks: {
            1: {
              Name: `Lieutenant`,
              Bonus: `${this._skillService.SkillNames.Leadership} 1`,
            } as Rank,
            2: {
              Name: `Captain`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Major`,
              Bonus: `${this._skillService.SkillNames.Tactics} (${this._skillService.SkillNames.TacticsMilitary}) 1`,
            } as Rank,
            4: {
              Name: `Lieutenant Colonel`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Colonel`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `General`,
              Bonus: `SOC 10 or SOC +1, whichever is higher`,
            }
          }
        } as RanksTable,
      ],
      Mishaps: {
        1: {
          Description: `Severely injured in action (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `Your unit is slaughtered in a disastrous battle, for which you blame your commander. Gain them as an [Enemy] as they have you removed from the service.`,
        } as Mishap,
        3: {
          Description: `You are sent to a very unpleasant region (jungle, swamp, desert, icecap, urban) to battle against geurilla fighters and rebels. You are discharged because of stress, injury, or because the government wishes to bury the whole incident. Increase [${this._skillService.SkillNames.Recon}] or [${this._skillService.SkillNames.Survival}] by one level, but also gain the rebels as an [Enemy].`,
        } as Mishap,
        4: {
          Description: `You discover that your commanding officer is engaged in some illegal activity, such as weapon smuggling. You can join their ring and gain them as an [Ally] before the inevitable investigation gets you discharged, or you can cooperate with the military police - the official whitewash gets you discharged anyway, but you may keep your Benefit roll from this term of service.`,
        } as Mishap,
        5: {
          Description: `You are tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival as they drive you out of the service.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You are assigned to a planet with a hostile or wild environment. Gain one of [${this._skillService.SkillNames.VaccSuit} 1], [${this._skillService.SkillNames.Engineer} 1], [${this._skillService.SkillNames.Animals} (${this._skillService.SkillNames.AnimalsHandling} or ${this._skillService.SkillNames.AnimalsTraining}) 1], or [${this._skillService.SkillNames.Recon} 1].`,
        } as CareerEvent,
        4: {
          Description: `You are assigned to an urbanised planet torn by war. Gain one of [${this._skillService.SkillNames.Stealth} 1], [${this._skillService.SkillNames.Streetwise} 1], [${this._skillService.SkillNames.Persuade} 1], or [${this._skillService.SkillNames.Recon} 1].`,
        } as CareerEvent,
        5: {
          Description: `You are given a special assignment or duty in your unit. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        6: {
          Description: `You are thrown into a brutal ground war. Roll EDU 8+ to avoid injury; if you succeed, you gain one level in [${this._skillService.SkillNames.GunCombat}] or [${this._skillService.SkillNames.Leadership}].`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You are given advanced training in a specialist field. Roll EDU 8+ to increase any one skill you already have by one level.`,
        } as CareerEvent,
        9: {
          Description: `Surrounded and outnumbered by the enemy, you hold out until relief arrives. Gain DM+2 to your next Advancement check.`,
        } as CareerEvent,
        10: {
          Description: `You are assigned to a peacekeeping role. Gain one of [${this._skillService.SkillNames.Admin} 1], [${this._skillService.SkillNames.Investigate} 1], [${this._skillService.SkillNames.Deception} 1], or [${this._skillService.SkillNames.Recon} 1].`,
        } as CareerEvent,
        11: {
          Description: `Your commanding officer takes an interest in your career. Either gain [${this._skillService.SkillNames.Tactics} (${this._skillService.SkillNames.TacticsMilitary}) 1] or DM+4 to your next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `You display heroism in battle. You may gain a promotion or a commission automatically.`,
        } as CareerEvent,
      }
    } as Career,
    'Citizen': {
      Name: `Citizen`,
      Designation: '3',
      Description: `Individuals serving in a corporation, bureaucracy, or industry, or who are making a new life on an
      untamed planet.`,
      Qualification: {characteristic: `EDU`, target: 5} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Corporate`,
          Description: `You are an executive or manager in a large corporation.`,
          Survival: {characteristic: `SOC`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Worker`,
          Description: `You are a blue collar worker on an industrial world.`,
          Survival: {characteristic: `END`, target: 4} as Qualification,
          Advancement: {characteristic: `EDU`, target: 8} as Qualification,
        } as Assignment,
        {
          Name: `Colonist`,
          Description: `You are building a new life on a recently settled world that still needs taming.`,
          Survival: {characteristic: `INT`, target: 7} as Qualification,
          Advancement: {characteristic: `END`, target: 5} as Qualification,
        } as Assignment,
      ],
      BenefitTable: {
        1: {
          Cash: 2000,
          BenefitName: `Ship Share`,
        } as Benefit,
        2: {
          Cash: 5000,
          BenefitName: `Ally`,
        } as Benefit,
        3: {
          Cash: 10000,
          BenefitName: `INT +1`,
        } as Benefit,
        4: {
          Cash: 10000,
          BenefitName: `Gun`,
        } as Benefit,
        5: {
          Cash: 10000,
          BenefitName: `EDU +1`,
        } as Benefit,
        6: {
          Cash: 50000,
          BenefitName: `Two Ship shares`,
        } as Benefit,
        7: {
          Cash: 100000,
          BenefitName: `TAS Membership`,
        } as Benefit,
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Corporate', 'Worker', 'Colonist'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.educationTraining,
            2: this._trainingService.intelligenceTraining,
            3: this._trainingService.carouseTraining,
            4: this._trainingService.gamblerTraining,
            5: this._trainingService.driveTraining,
            6: this._trainingService.jackOfAllTradesTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Corporate', 'Worker', 'Colonist'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.driveTraining,
            2: this._trainingService.flyerTraining,
            3: this._trainingService.streetwiseTraining,
            4: this._trainingService.meleeTraining,
            5: this._trainingService.stewardTraining,
            6: this._trainingService.professionTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min EDU 10)`,
          Assignments: ['Corporate', 'Worker', 'Colonist'],
          MinEDU: 10,
          Trainings: {
            1: this._trainingService.artTraining,
            2: this._trainingService.advocateTraining,
            3: this._trainingService.diplomatTraining,
            4: this._trainingService.languageTraining,
            5: this._trainingService.computersTraining,
            6: this._trainingService.medicTraining
          }
        } as TrainingTable,
        {
          Name: `Corporate`,
          Assignments: ['Corporate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.advocateTraining,
            2: this._trainingService.adminTraining,
            3: this._trainingService.brokerTraining,
            4: this._trainingService.computersTraining,
            5: this._trainingService.diplomatTraining,
            6: this._trainingService.leadershipTraining
          }
        } as TrainingTable,
        {
          Name: `Worker`,
          Assignments: ['Worker'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.driveTraining,
            2: this._trainingService.mechanicTraining,
            3: this._trainingService.electronicsTraining,
            4: this._trainingService.engineerTraining,
            5: this._trainingService.professionTraining,
            6: this._trainingService.scienceTraining
          }
        } as TrainingTable,
        {
          Name: `Colonist`,
          Assignments: ['Colonist'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.animalsTraining,
            2: this._trainingService.athleticsTraining,
            3: this._trainingService.jackOfAllTradesTraining,
            4: this._trainingService.driveTraining,
            5: this._trainingService.survivalTraining,
            6: this._trainingService.reconTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Corporate`,
          Assignments: [`Corporate`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            2: {
              Name: `Manager`,
              Bonus: `${this._skillService.SkillNames.Admin} 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Senior Manager`,
              Bonus: `${this._skillService.SkillNames.Advocate} 1`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Director`,
              Bonus: `SOC +1`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Worker`,
          Assignments: [`Worker`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            2: {
              Name: `Technician`,
              Bonus: `${this._skillService.SkillNames.Profession} 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Craftsman`,
              Bonus: `${this._skillService.SkillNames.Mechanic} 1`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Master Technician`,
              Bonus: `${this._skillService.SkillNames.Engineer} 1`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Colonist`,
          Assignments: [`Colonist`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            2: {
              Name: `Settler`,
              Bonus: `${this._skillService.SkillNames.Survival} 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Explorer`,
              Bonus: `${this._skillService.SkillNames.Navigation} 1`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.GunCombat} 1`,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `You are harassed and your life ruined by a criminal gang. Gain the gang as an [Enemy]`,
        } as Mishap,
        3: {
          Description: `Hard times caused by a lack of interstellar trade costs you your job. Lose one SOC.`,
        } as Mishap,
        4: {
          Description: `Your business is investigated by the planetary authorities (or your colony ship suffers interference from interests offworld). Cooperate, and the business or colony is shut down, but you gain DM+2 to the Qualification check for your next career as a reward for your aid. Refuse, and gain an [Ally].`,
        } as Mishap,
        5: {
          Description: `A revolution, attack, or other unusual event throws your life into chaos, forcing you to leave the planet. Roll ${this._skillService.SkillNames.Streetwise} 8+. If you succeed, increase any skill you have by one level.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `Political upheaval strikes your homeworld, and you are caught up in the revolution.
          Gain either [${this._skillService.SkillNames.Advocate} 1], [${this._skillService.SkillNames.Persuade} 1],
          [${this._skillService.SkillNames.Explosives} 1], or [${this._skillService.SkillNames.Streetwise} 1].
          Roll whichever skill you chose 8+. If you succeed, you come out on the winning side, and gain DM+2 to your
          next Advancement roll. Fail, and you suffer DM-2 to your next Survival roll.`,
        } as CareerEvent,
        4: {
          Description: `You spend time maintaining and using heavy vehicles, either as part of your job or as a hobby.
          Increase [${this._skillService.SkillNames.Mechanic}], [${this._skillService.SkillNames.Drive}],
          [${this._skillService.SkillNames.Electronics}], [${this._skillService.SkillNames.Flyer}], or
          [${this._skillService.SkillNames.Engineer}] by one level.`,
        } as CareerEvent,
        5: {
          Description: `Your business expands, your corporation grows, or the colony thrives. Gain DM+1 to any one
          Benefit roll.`,
        } as CareerEvent,
        6: {
          Description: `You are given advanced training in a specialist field. Roll EDU 10+ to gain any one skill of
          your choice at level 1`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You learn something you should not have - a corporate secret, a political scandal - which you
          can profit from illegally. If you choose to do so, then you gain DM+1 to a Benefit roll from this career and
          gain [${this._skillService.SkillNames.Streetwise} 1], [${this._skillService.SkillNames.Deception} 1],
          or a criminal [Contact]. If you refuse, you gain nothing.`,
        } as CareerEvent,
        9: {
          Description: `You are rewarded for you diligence or cunning. Gain DM+2 to your next Advancement check.`,
        } as CareerEvent,
        10: {
          Description: `You gain experience in a technical field as a computer operator or surveyor. Increase
          [${this._skillService.SkillNames.Electronics}] or [${this._skillService.SkillNames.Engineer}] by one level.`,
        } as CareerEvent,
        11: {
          Description: `You befriend a superior in the corporation or the colony. Gain an [Ally]. Either gain
          [${this._skillService.SkillNames.Diplomat} 1] or DM+4 to your next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `You rise to a position of power in your colony or corporation. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Drifter': {
      Name: `Drifter`,
      Designation: '4',
      Description: `Wanderers, hitchhikers, and travellers, drifters are those who roam the stars without obvious
      purpose or direction.`,
      Qualification: {characteristic: `Automatic`, target: 0} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Barbarian`,
          Description: `You live on a primitive world without the benefits of technology.`,
          Survival: {characteristic: `END`, target: 7} as Qualification,
          Advancement: {characteristic: `STR`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: `Wanderer`,
          Description: `You are a space bum, living hand-to-mouth in slums and spaceports across the galaxy.`,
          Survival: {characteristic: `END`, target: 7} as Qualification,
          Advancement: {characteristic: `INT`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: `Scavenger`,
          Description: `You work as a belter (asteroid miner) or on a salvage crew.`,
          Survival: {characteristic: `DEX`, target: 7} as Qualification,
          Advancement: {characteristic: `END`, target: 7} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 0,
          BenefitName: `[Contact]`,
        } as Benefit,
        2: {
          Cash: 0,
          BenefitName: `Weapon`,
        } as Benefit,
        3: {
          Cash: 1000,
          BenefitName: `[Ally]`,
        } as Benefit,
        4: {
          Cash: 2000,
          BenefitName: `Weapon`,
        } as Benefit,
        5: {
          Cash: 3000,
          BenefitName: `EDU +1`,
        } as Benefit,
        6: {
          Cash: 4000,
          BenefitName: `Ship Share`,
        } as Benefit,
        7: {
          Cash: 8000,
          BenefitName: `Two Ship Shares`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Barbarian', 'Wanderer', 'Scavenger'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.enduranceTraining,
            3: this._trainingService.dexterityTraining,
            4: this._trainingService.languageTraining,
            5: this._trainingService.professionTraining,
            6: this._trainingService.jackOfAllTradesTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Barbarian', 'Wanderer', 'Scavenger'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.athleticsTraining,
            2: this._trainingService.unarmedTraining,
            3: this._trainingService.reconTraining,
            4: this._trainingService.streetwiseTraining,
            5: this._trainingService.stealthTraining,
            6: this._trainingService.survivalTraining
          }
        } as TrainingTable,
        {
          Name: ``,
          Assignments: [],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.dummyTraining,
            2: this._trainingService.dummyTraining,
            3: this._trainingService.dummyTraining,
            4: this._trainingService.dummyTraining,
            5: this._trainingService.dummyTraining,
            6: this._trainingService.dummyTraining
          }
        } as TrainingTable,
        {
          Name: `Barbarian`,
          Assignments: ['Barbarian'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.animalsTraining,
            2: this._trainingService.carouseTraining,
            3: this._trainingService.bladeTraining,
            4: this._trainingService.stealthTraining,
            5: {
              BenefitName: `${this._skillService.SkillNames.Seafarer} (${this._skillService.SkillNames.SeafarerPersonal}
               or ${this._skillService.SkillNames.SeafarerSail})`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.SeafarerPersonal,
                this._skillService.SkillNames.SeafarerSail,
              ]
            } as Training,
            6: this._trainingService.survivalTraining
          }
        } as TrainingTable,
        {
          Name: `Wanderer`,
          Assignments: ['Wanderer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.driveTraining,
            2: this._trainingService.deceptionTraining,
            3: this._trainingService.reconTraining,
            4: this._trainingService.stealthTraining,
            5: this._trainingService.streetwiseTraining,
            6: this._trainingService.survivalTraining
          }
        } as TrainingTable,
        {
          Name: `Scavenger`,
          Assignments: ['Scavenger'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.smallCraftTraining,
            2: this._trainingService.mechanicTraining,
            3: this._trainingService.astrogationTraining,
            4: this._trainingService.vaccSuitTraining,
            5: this._trainingService.professionTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
      ],
      RankTables: [
        {
          Name: `Barbarian`,
          Assignments: [`Barbarian`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Survival} 1`,
            } as Rank,
            2: {
              Name: `Warrior`,
              Bonus: `${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeBlade}) 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Chieftain`,
              Bonus: `${this._skillService.SkillNames.Leadership} 1`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Warlord`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Wanderer`,
          Assignments: [`Wanderer`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Streetwise} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Deception} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Scavenger`,
          Assignments: [`Scavenger`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.VaccSuit} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Profession} (Belter) 1 or ${this._skillService.SkillNames.Mechanic} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll
          twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
        3: {
          Description: `You run afoul of a criminal gang, corrupt bureaucrat, or other foe. Gain an [Enemy]`,
        } as Mishap,
        4: {
          Description: `You suffer from a life-threatening illness. Reduce your END by 1.`,
        } as Mishap,
        5: {
          Description: `Betrayed by a friend. One of your Contacts or Allies betrays you, ending your career.
          That [Contact] or [Ally] becomes a [Rival] or [Enemy]. If you have no Contacts or Allies, then you are
          betrayed by someone you never saw coming and still gain a [Rival] or [Enemy].`,
        } as Mishap,
        6: {
          Description: `You do not know what happened to you. There is a gap in your memory.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `A patron offers you a chance at a job. If you accept, you gain DM+4 to your next Qualification
           roll, but you owe that patron a favour.`,
        } as CareerEvent,
        4: {
          Description: `You pick up a few useful skills here and there. Gain one level of ${this._skillService.SkillNames.JackOfAllTrades}
          ${this._skillService.SkillNames.Survival}, ${this._skillService.SkillNames.Streetwise}, or
          ${this._skillService.SkillNames.Melee}.`,
        } as CareerEvent,
        5: {
          Description: `You manage to scavenge something of use. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        6: {
          Description: `You encounter something unusual. Go to the Life Events Table and have an Unusual Event.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You are attacked by enemies. Gain an [Enemy] if you do not have one already, and roll either
          ${this._skillService.SkillNames.Melee} 8+, ${this._skillService.SkillNames.GunCombat} 8+, or
          ${this._skillService.SkillNames.Stealth} 8+ to avoid a roll on the Injury Table.`,
        } as CareerEvent,
        9: {
          Description: `You are offered a chance to take part in a risky but rewarding adventure. If you accept, roll 1D:<br>
          On a 1-2, you are injured or arrested; either roll on the Injury Table or take the Prisoner Career in your next term.<br>
          On a 3-4, you survive, but gain nothing.<br>
          On a 5-6, you succeed. Gain DM+4 to one Benefit roll.`,
        } as CareerEvent,
        10: {
          Description: `Life on the edge hones your abilities. Increase any skill you already have by one level.`,
        } as CareerEvent,
        11: {
          Description: `You are forcibly drafted. Roll for the Draft next term.`,
        } as CareerEvent,
        12: {
          Description: `You thrive on adversity. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Entertainer': {
      Name: `Entertainer`,
      Designation: '5',
      Description: `Individuals who are involved with the media, whether as reporters, artists, or celebrities.`,
      Qualification: {
        characteristic: `DEX or INT`,
        target: 5,
        special: 'DM-1 for every previous career'
      } as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Artist`,
          Description: `You are a writer, holographer, or other creative.`,
          Survival: {characteristic: `SOC`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Journalist`,
          Description: `You report on local or galactic events for a news feed, the TAS, or other organization.`,
          Survival: {characteristic: `EDU`, target: 7} as Qualification,
          Advancement: {characteristic: `INT`, target: 5} as Qualification,
        } as Assignment,
        {
          Name: `Performer`,
          Description: `You are an actor, dancer, acrobat, professional athlete, or other public performer.`,
          Survival: {characteristic: `INT`, target: 5} as Qualification,
          Advancement: {characteristic: `DEX`, target: 7} as Qualification,
        } as Assignment,
      ],
      BenefitTable: {
        1: {
          Cash: 0,
          BenefitName: `[Contact]`,
        } as Benefit,
        2: {
          Cash: 0,
          BenefitName: `SOC +1`,
        } as Benefit,
        3: {
          Cash: 10000,
          BenefitName: `[Contact]`,
        } as Benefit,
        4: {
          Cash: 10000,
          BenefitName: `SOC +1`,
        } as Benefit,
        5: {
          Cash: 40000,
          BenefitName: `INT +1`,
        } as Benefit,
        6: {
          Cash: 40000,
          BenefitName: `Two Ship Shares`,
        } as Benefit,
        7: {
          Cash: 80000,
          BenefitName: `SOC +1 and EDU +1`,
        } as Benefit,
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Artist', 'Journalist', 'Performer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.dexterityTraining,
            2: this._trainingService.intelligenceTraining,
            3: this._trainingService.socialStatusTraining,
            4: this._trainingService.languageTraining,
            5: this._trainingService.carouseTraining,
            6: this._trainingService.jackOfAllTradesTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Artist', 'Journalist', 'Performer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.artTraining,
            2: this._trainingService.carouseTraining,
            3: this._trainingService.deceptionTraining,
            4: this._trainingService.driveTraining,
            5: this._trainingService.persuadeTraining,
            6: this._trainingService.stewardTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 10)`,
          Assignments: ['Artist', 'Journalist', 'Performer'],
          MinEDU: 10,
          Trainings: {
            1: this._trainingService.advocateTraining,
            2: this._trainingService.brokerTraining,
            3: this._trainingService.deceptionTraining,
            4: this._trainingService.scienceTraining,
            5: this._trainingService.streetwiseTraining,
            6: this._trainingService.diplomatTraining
          }
        } as TrainingTable,
        {
          Name: `Artist`,
          Assignments: ['Artist'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.artTraining,
            2: this._trainingService.carouseTraining,
            3: this._trainingService.computersTraining,
            4: this._trainingService.gamblerTraining,
            5: this._trainingService.persuadeTraining,
            6: this._trainingService.professionTraining
          }
        } as TrainingTable,
        {
          Name: `Journalist`,
          Assignments: ['Journalist'],
          MinEDU: 0,
          Trainings: {
            1: {
              BenefitName: `${this._skillService.SkillNames.Art} (${this._skillService.SkillNames.ArtHolography} or
              ${this._skillService.SkillNames.ArtWrite})`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.ArtHolography,
                this._skillService.SkillNames.ArtWrite,
              ]
            } as Training,
            2: this._trainingService.electronicsTraining,
            3: this._trainingService.driveTraining,
            4: this._trainingService.investigateTraining,
            5: this._trainingService.reconTraining,
            6: this._trainingService.streetwiseTraining
          }
        } as TrainingTable,
        {
          Name: `Performer`,
          Assignments: ['Performer'],
          MinEDU: 0,
          Trainings: {
            1: {
              BenefitName: `${this._skillService.SkillNames.Art} (${this._skillService.SkillNames.ArtPerformer} or ${this._skillService.SkillNames.ArtInstrument})`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.ArtPerformer,
                this._skillService.SkillNames.ArtInstrument,
              ]
            } as Training,
            2: this._trainingService.athleticsTraining,
            3: this._trainingService.carouseTraining,
            4: this._trainingService.deceptionTraining,
            5: this._trainingService.stealthTraining,
            6: this._trainingService.streetwiseTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Artist`,
          Assignments: [`Artist`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Art} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Investigate} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Famous Artist`,
              Bonus: `SOC +1`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Journalist`,
          Assignments: [`Journalist`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Freelancer`,
              Bonus: `${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsComms}) 1`,
            } as Rank,
            2: {
              Name: `Staff Writer`,
              Bonus: `${this._skillService.SkillNames.Investigate} 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Correspondent`,
              Bonus: `${this._skillService.SkillNames.Persuade} 1`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Senior Correspondent`,
              Bonus: `SOC +1`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Performer`,
          Assignments: [`Performer`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `DEX +1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `STR +1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Famous Performer`,
              Bonus: `SOC +1`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `You expose or are involved in a scandal of some sort.`,
        } as Mishap,
        3: {
          Description: `Public opinion turns on you. Reduce your SOC by 1.`,
        } as Mishap,
        4: {
          Description: `You are betrayed by a peer. One of your Contacts or Allies betrays you, ending your career. That Contact or Ally becomes a [Rival] or [Enemy].
          If you have no Contacts or Allies, then you are betrayed by someone you never say coming and still gain a [Rival] or [Enemy].`,
        } as Mishap,
        5: {
          Description: `An investigation, tour, project, or expedition goes wrong, stranding you far from home. Gain one of
          [${this._skillService.SkillNames.Survival} 1], [${this._skillService.SkillNames.Pilot} 1],
           [${this._skillService.SkillNames.Persuade} 1], or [${this._skillService.SkillNames.Streetwise} 1]`,
        } as Mishap,
        6: {
          Description: `You are forced out because of censorship or controversy. What truth did you get too close to?
           You gain DM+2 to the Qualification roll for your next career.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You are invited to take part in a controversial event or exhibition. Roll ${this._skillService.SkillNames.Art}
           8+ or ${this._skillService.SkillNames.Investigate} 8+. If you succeed, gain one SOC. If you fail, lose one SOC.`,
        } as CareerEvent,
        4: {
          Description: `You are a part of your homeworld's celebrity circles. Gain one of [${this._skillService.SkillNames.Carouse} 1],
          [${this._skillService.SkillNames.Persuade} 1], [${this._skillService.SkillNames.Steward} 1], or a [Contact].`,
        } as CareerEvent,
        5: {
          Description: `One of your works is especially well received and popular, making you a minor celebrity. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        6: {
          Description: `You gain a patron in the arts. Gain DM+2 to your next Advancement check and an [Ally]`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You have the opportunity to criticize or even bring down a questionable political leader on
          your homeworld. If you refuse and support the leader, you gain nothing. If you accept, gain an [Enemy] and
          roll ${this._skillService.SkillNames.Art} or ${this._skillService.SkillNames.Persuade} 8+. If you succeed,
          gain one level in any skill you already have. If you fail, increase a skill anyway and roll on the Mishap
          table.`,
        } as CareerEvent,
        9: {
          Description: `You go on a tour of the sector, visiting several worlds. Gain D3 [Contacts]`,
        } as CareerEvent,
        10: {
          Description: `One of your pieces of art is stolen, and the investigation brings you into the criminal underworld.
          Gain one of [${this._skillService.SkillNames.Streetwise} 1], [${this._skillService.SkillNames.Investigate} 1],
          [${this._skillService.SkillNames.Recon} 1], or [${this._skillService.SkillNames.Stealth} 1]`,
        } as CareerEvent,
        11: {
          Description: `As an artist, you lead a strange and charmed life. Go to the Life Events Table and have an Unusual Event.`,
        } as CareerEvent,
        12: {
          Description: `You win a prestigious prize. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Marine': {
      Name: `Marine`,
      Designation: '6',
      Description: `Members of the armed fighting forces carried aboard starships, marines deal with piracy and boarding
       actions in space, defend the starports and bases belonging to the navy, and supplement ground forces such as the army.`,
      Qualification: {
        characteristic: `END`,
        target: 6,
        special: 'DM-1 for every previous career<br>DM-2 if you are aged 30 or more'
      } as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Support`,
          Description: `You are a quartermaster, engineer, or battlefield medic in the marines.`,
          Survival: {characteristic: `END`, target: 5} as Qualification,
          Advancement: {characteristic: `EDU`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: `Star Marine`,
          Description: `You are trained to fight boarding actions and capture enemy vessels.`,
          Survival: {characteristic: `END`, target: 6} as Qualification,
          Advancement: {characteristic: `EDU`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Ground Assault`,
          Description: `You are kicked out of a spacecraft in high orbit and told to 'capture that planet'.`,
          Survival: {characteristic: `END`, target: 7} as Qualification,
          Advancement: {characteristic: `EDU`, target: 5} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 2000,
          BenefitName: `Armour`,
        } as Benefit,
        2: {
          Cash: 5000,
          BenefitName: `INT +1`,
        } as Benefit,
        3: {
          Cash: 5000,
          BenefitName: `EDU +1`,
        } as Benefit,
        4: {
          Cash: 10000,
          BenefitName: `Weapon`,
        } as Benefit,
        5: {
          Cash: 20000,
          BenefitName: `TAS Membership`,
        } as Benefit,
        6: {
          Cash: 30000,
          BenefitName: `Armour or END +1`,
        } as Benefit,
        7: {
          Cash: 40000,
          BenefitName: `SOC +2`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Support', 'Star Marine', 'Ground Assault'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.gamblerTraining,
            5: this._trainingService.unarmedTraining,
            6: this._trainingService.bladeTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Support', 'Star Marine', 'Ground Assault'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.athleticsTraining,
            2: this._trainingService.vaccSuitTraining,
            3: this._trainingService.tacticsTraining,
            4: this._trainingService.heavyWeaponsTraining,
            5: this._trainingService.gunCombatTraining,
            6: this._trainingService.stealthTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 8)`,
          Assignments: ['Support', 'Star Marine', 'Ground Assault'],
          MinEDU: 8,
          Trainings: {
            1: this._trainingService.medicTraining,
            2: this._trainingService.survivalTraining,
            3: this._trainingService.explosivesTraining,
            4: this._trainingService.engineerTraining,
            5: this._trainingService.pilotTraining,
            6: this._trainingService.navigationTraining
          }
        } as TrainingTable,
        {
          Name: `Support`,
          Assignments: ['Support'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.mechanicTraining,
            3: {
              BenefitName: `${this._skillService.SkillNames.Drive} or ${this._skillService.SkillNames.Flyer}`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.DriveHovercraft,
                this._skillService.SkillNames.DriveMole,
                this._skillService.SkillNames.DriveTrack,
                this._skillService.SkillNames.DriveWalker,
                this._skillService.SkillNames.DriveWheel,
                this._skillService.SkillNames.FlyerAirship,
                this._skillService.SkillNames.FlyerGrav,
                this._skillService.SkillNames.FlyerOrnithopter,
                this._skillService.SkillNames.FlyerRotor,
                this._skillService.SkillNames.FlyerWing,
              ]
            } as Training,
            4: this._trainingService.medicTraining,
            5: this._trainingService.heavyWeaponsTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
        {
          Name: `Star Marine`,
          Assignments: ['Star Marine'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.vaccSuitTraining,
            2: this._trainingService.athleticsTraining,
            3: this._trainingService.gunnerTraining,
            4: this._trainingService.bladeTraining,
            5: this._trainingService.electronicsTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
        {
          Name: `Ground Assault`,
          Assignments: ['Ground Assault'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.vaccSuitTraining,
            2: this._trainingService.heavyWeaponsTraining,
            3: this._trainingService.reconTraining,
            4: this._trainingService.bladeTraining,
            5: this._trainingService.militaryTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
        {
          Name: `Officer (Commissioned Only)`,
          Assignments: ['Support Officer', 'Star Marine Officer', 'Ground Assault Officer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.tacticsTraining,
            3: this._trainingService.adminTraining,
            4: this._trainingService.advocateTraining,
            5: this._trainingService.vaccSuitTraining,
            6: this._trainingService.leadershipTraining
          }
        } as TrainingTable,
        {
          Name: ``,
          Assignments: [],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.dummyTraining,
            2: this._trainingService.dummyTraining,
            3: this._trainingService.dummyTraining,
            4: this._trainingService.dummyTraining,
            5: this._trainingService.dummyTraining,
            6: this._trainingService.dummyTraining
          }
        } as TrainingTable,
        {
          Name: ``,
          Assignments: [],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.dummyTraining,
            2: this._trainingService.dummyTraining,
            3: this._trainingService.dummyTraining,
            4: this._trainingService.dummyTraining,
            5: this._trainingService.dummyTraining,
            6: this._trainingService.dummyTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Enlisted`,
          Assignments: [`Support`, 'Star Marine', 'Ground Assault'],
          Ranks: {
            0: {
              Name: `Marine`,
              Bonus: `${this._skillService.SkillNames.GunCombat} 1 or ${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeBlade}) 1`,
            } as Rank,
            1: {
              Name: `Lance Corporal`,
              Bonus: `${this._skillService.SkillNames.GunCombat} 1`,
            } as Rank,
            2: {
              Name: `Corporal`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Lance Sergeant`,
              Bonus: `${this._skillService.SkillNames.Leadership}`,
            } as Rank,
            4: {
              Name: `Sergeant`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Gunnery Sergeant`,
              Bonus: `END +1`,
            } as Rank,
            6: {
              Name: `Sergeant Major`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Officer`,
          Assignments: [`Support Officer`, `Star Marine Officer`, `Ground Assault Officer`],
          Ranks: {
            0: {
              Name: `-----Not a Rank-----`,
              Bonus: `----------`,
            } as Rank,
            1: {
              Name: `Lieutenant`,
              Bonus: `${this._skillService.SkillNames.Leadership} 1`,
            } as Rank,
            2: {
              Name: `Captain`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Force Commander`,
              Bonus: `${this._skillService.SkillNames.Tactics} 1`,
            } as Rank,
            4: {
              Name: `Lieutenant Colonel`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Colonel`,
              Bonus: `SOC 10 or SOC +1, whichever is higher`,
            } as Rank,
            6: {
              Name: `Brigadier`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll
          twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `A mission goes wrong; you and several others are captured and mistreated by the enemy. Due to
          your injuries, you are discharged early. Gain your jailer as an [Enemy] and reduce your STR and DEX by one
          because of your injuries.`,
        } as Mishap,
        3: {
          Description: `A mission goes wrong and you are stranded behind enemy lines. Increase
          [${this._skillService.SkillNames.Stealth}] or [${this._skillService.SkillNames.Survival}] by one level, but
          due to the mission's failure, you are ejected from the service.`,
        } as Mishap,
        4: {
          Description: `You are ordered to take part in a black ops mission that goes against your conscience.
          If you refuse, you are ejected from the service. If you accept, you may stay with the marines, but gain the
          lone survivor as an [Enemy].`,
        } as Mishap,
        5: {
          Description: `You are tormented by or quarrel with an officer or fellow marine. Gain that character as a [Rival] as he drives you out of the service.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `Trapped behind enemy lines, you have to survive on your own. Gain one of ${this._skillService.SkillNames.Survival} 1,
          ${this._skillService.SkillNames.Stealth} 1, ${this._skillService.SkillNames.Deception} 1, or ${this._skillService.SkillNames.Streetwise} 1`,
        } as CareerEvent,
        4: {
          Description: `You are assigned to the security staff of a space station. Increase ${this._skillService.SkillNames.VaccSuit}
          or ${this._skillService.SkillNames.Athletics} (${this._skillService.SkillNames.AthleticsDexterity}) by one level.`,
        } as CareerEvent,
        5: {
          Description: `You are given advanced training in a specialist field. Roll EDU 8+ to gain any one skill of your choice at level 1.`,
        } as CareerEvent,
        6: {
          Description: `You are assigned to an assault on an enemy fortress. Roll ${this._skillService.SkillNames.Melee}
           or ${this._skillService.SkillNames.GunCombat} 8+ and gain a ${this._skillService.SkillNames.Tactics}
            (${this._skillService.SkillNames.TacticsMilitary}) or ${this._skillService.SkillNames.Leadership} if you succeed.
            If you fail, you are injured and lose 1 point from any physical characteristic.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You are on the front lines of a planetary assault and occupation. Gain one of
          ${this._skillService.SkillNames.Recon} 1, ${this._skillService.SkillNames.GunCombat} 1,
          ${this._skillService.SkillNames.Leadership} 1, or ${this._skillService.SkillNames.Electronics}
          (${this._skillService.SkillNames.ElectronicsComms}) 1.`,
        } as CareerEvent,
        9: {
          Description: `A mission goes disastrously wrong due to your commander's error or incompetence, but you survive.
          If you report your commanding officer for their failure, then you gain DM+2 to your next Advancement roll and
          gain the officer as an Enemy. If you say nothing and protect them, gain them as an Ally.`,
        } as CareerEvent,
        10: {
          Description: `You are assigned to a black ops mission. Gain DM+2 to your next Advancement roll.`,
        } as CareerEvent,
        11: {
          Description: `Your commanding officer takes an interest in your career. Either gain ${this._skillService.SkillNames.Tactics} 1
          or DM+4 to your next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `You display heroism in battle. You may gain a promotion or a commission automatically.`,
        } as CareerEvent,
      }
    } as Career,
    'Merchant': {
      Name: `Merchant`,
      Designation: '7',
      Description: `Members of a commercial enterprise. Merchants may  crew the ships of the huge trading corporations,
      or they may work for independent free traders who carry chance cargoes and passengers between worlds.`,
      Qualification: {characteristic: `INT`, target: 4, special: 'DM-1 for every previous career'} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Merchant Marine`,
          Description: `You work on one of the massive cargo haulers run by the Imperium or the megacorporations.`,
          Survival: {characteristic: `EDU`, target: 5} as Qualification,
          Advancement: {characteristic: `INT`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: 'Free Trader',
          Description: `You are part of the crew of a tramp trader.`,
          Survival: {characteristic: `DEX`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Broker`,
          Description: `You work in a planetside brokerage or starport.`,
          Survival: {characteristic: `EDU`, target: 5} as Qualification,
          Advancement: {characteristic: `INT`, target: 7} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 1000,
          BenefitName: `Blade`,
        } as Benefit,
        2: {
          Cash: 5000,
          BenefitName: `INT +1`,
        } as Benefit,
        3: {
          Cash: 10000,
          BenefitName: `EDU +1`,
        } as Benefit,
        4: {
          Cash: 20000,
          BenefitName: `Gun`,
        } as Benefit,
        5: {
          Cash: 20000,
          BenefitName: `Ship Share`,
        } as Benefit,
        6: {
          Cash: 40000,
          BenefitName: `Free Trader`,
        } as Benefit,
        7: {
          Cash: 40000,
          BenefitName: `Free Trader`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Merchant Marine', 'Free Trader', 'Broker'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.intelligenceTraining,
            5: this._trainingService.languageTraining,
            6: this._trainingService.streetwiseTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Merchant Marine', 'Free Trader', 'Broker'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.driveTraining,
            2: this._trainingService.vaccSuitTraining,
            3: this._trainingService.brokerTraining,
            4: this._trainingService.stewardTraining,
            5: this._trainingService.electronicsTraining,
            6: this._trainingService.persuadeTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 10)`,
          Assignments: ['Merchant Marine', 'Free Trader', 'Broker'],
          MinEDU: 10,
          Trainings: {
            1: this._trainingService.engineerTraining,
            2: this._trainingService.astrogationTraining,
            3: this._trainingService.electronicsTraining,
            4: this._trainingService.pilotTraining,
            5: this._trainingService.adminTraining,
            6: this._trainingService.advocateTraining
          }
        } as TrainingTable,
        {
          Name: `Merchant Marine`,
          Assignments: ['Merchant Marine'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.pilotTraining,
            2: this._trainingService.vaccSuitTraining,
            3: this._trainingService.athleticsTraining,
            4: this._trainingService.mechanicTraining,
            5: this._trainingService.engineerTraining,
            6: this._trainingService.electronicsTraining
          }
        } as TrainingTable,
        {
          Name: `Free Trader`,
          Assignments: ['Free Trader'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.spacecraftTraining,
            2: this._trainingService.vaccSuitTraining,
            3: this._trainingService.deceptionTraining,
            4: this._trainingService.mechanicTraining,
            5: this._trainingService.streetwiseTraining,
            6: this._trainingService.gunnerTraining
          }
        } as TrainingTable,
        {
          Name: `Broker`,
          Assignments: ['Broker'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.adminTraining,
            2: this._trainingService.advocateTraining,
            3: this._trainingService.brokerTraining,
            4: this._trainingService.streetwiseTraining,
            5: this._trainingService.deceptionTraining,
            6: this._trainingService.persuadeTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Merchant Marine`,
          Assignments: [`Merchant Marine`],
          Ranks: {
            0: {
              Name: `Crewman`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Senior Crewman`,
              Bonus: `${this._skillService.SkillNames.Mechanic} 1`,
            } as Rank,
            2: {
              Name: `4th Officer`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `3rd Officer`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `2nd Officer`,
              Bonus: `${this._skillService.SkillNames.Pilot} +1`,
            } as Rank,
            5: {
              Name: `1st Officer`,
              Bonus: `SOC +1`,
            } as Rank,
            6: {
              Name: `Captain`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Free Trader`,
          Assignments: [`Free Trader`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Persuade}`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Experienced Trader`,
              Bonus: `${this._skillService.SkillNames.JackOfAllTrades}`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Broker`,
          Assignments: [`Broker`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Broker} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Experienced Broker`,
              Bonus: `${this._skillService.SkillNames.Streetwise}`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: ``,
          Assignments: [``],
          Ranks: {
            0: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            1: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            2: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            3: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            4: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            5: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            6: {
              Name: ``,
              Bonus: ``,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll
          twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `You are bankrupted by a rival. You lose all Benefits from this career, and gain the other
          trader as a Rival.`,
        } as Mishap,
        3: {
          Description: `A sudden war destroys your trade routes and contacts, forcing you fo flee that region of space.
          Gain ${this._skillService.SkillNames.GunCombat} 1 or ${this._skillService.SkillNames.Pilot} 1`,
        } as Mishap,
        4: {
          Description: `Your ship or starport is destroyed by criminals. Gain them as an Enemy.`,
        } as Mishap,
        5: {
          Description: `Imperial trade restrictions force you out of business. You may take the Rogue career for your
           next term without needing  to roll for qualification.`,
        } as Mishap,
        6: {
          Description: `A series of bad deals and decisions force you into bankruptcy. You salvage what you can.
          You may take a Benefit roll for this term as well as any others you are entitled to.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You are offered the opportunity to smuggle illegal items onto a planet. If you accept, roll
          ${this._skillService.SkillNames.Deception} 8+ or ${this._skillService.SkillNames.Persuade} 8+ to gain
          ${this._skillService.SkillNames.Streetwise} 1 and an extra Benefit roll. If you refuse, you gain an Enemy
          in the criminal sphere.`,
        } as CareerEvent,
        4: {
          Description: `Gain any one of these skills, reflecting you time spent dealing with suppliers and spacers:
          ${this._skillService.SkillNames.Profession} 1, ${this._skillService.SkillNames.Electronics} 1,
          ${this._skillService.SkillNames.Engineer} 1, ${this._skillService.SkillNames.Animals} 1,
          or ${this._skillService.SkillNames.Science} 1.`,
        } as CareerEvent,
        5: {
          Description: `You have a chance to risk your fortune on a possible lucrative deal. You may gamble a number
          of Benefit rolls and roll ${this._skillService.SkillNames.Gambler} 8+ or ${this._skillService.SkillNames.Broker} 8+.
          If you succeed, you gain half as many Benefit rolls as you risked, rounding up. If you fail, lose all the rolls
          risked. Either way, gain one level in whichever skill you used.`,
        } as CareerEvent,
        6: {
          Description: `You make an unexpected connection outside your normal circles. Gain a Contact.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You are embroiled in legal trouble. Gain one of ${this._skillService.SkillNames.Advocate} 1,
          ${this._skillService.SkillNames.Admin} 1, ${this._skillService.SkillNames.Diplomat} 1, or
          ${this._skillService.SkillNames.Investigate} 1. Then roll 2D. If you roll a 2, you must take the Prisoner
          career in your next term.`,
        } as CareerEvent,
        9: {
          Description: `You are given advanced training in a specialist field. Roll EDU 8+ to increase any one skill
          you already have by one level.`,
        } as CareerEvent,
        10: {
          Description: `A good deal ensures you are living the high life for a few years. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        11: {
          Description: `You befriend a useful ally in one sphere. Gain an Ally and either gain a level in
          ${this._skillService.SkillNames.Carouse} or DM+4 to your next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `Your business or ship thrives. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Navy': {
      Name: `Navy`,
      Designation: '8',
      Description: `Members of the interstellar navy which patrols space between the stars. The navy has the
      responsibility for the protection of society from foreign powers and lawless elements in the interstellar trade
      channels.`,
      Qualification: {
        characteristic: `INT`,
        target: 6,
        special: 'DM-1 for every previous career<br>DM-2 if you are aged 34 or more'
      } as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Line/Crew`,
          Description: `You serve as a general crewman or officer on a ship of the line.`,
          Survival: {characteristic: `INT`, target: 5} as Qualification,
          Advancement: {characteristic: `EDU`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: `Engineer/Gunner`,
          Description: `You serve as a specialist technician on a starship.`,
          Survival: {characteristic: `INT`, target: 6} as Qualification,
          Advancement: {characteristic: `EDU`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Flight`,
          Description: `You are the pilot of a shuttle, fighter, or other light craft.`,
          Survival: {characteristic: `DEX`, target: 7} as Qualification,
          Advancement: {characteristic: `EDU`, target: 5} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 1000,
          BenefitName: `Personal Vehicle or Ship Share`,
        } as Benefit,
        2: {
          Cash: 5000,
          BenefitName: `INT +1`,
        } as Benefit,
        3: {
          Cash: 5000,
          BenefitName: `EDU +1 or two Ship Shares`,
        } as Benefit,
        4: {
          Cash: 10000,
          BenefitName: `Weapon`,
        } as Benefit,
        5: {
          Cash: 20000,
          BenefitName: `TAS Membership`,
        } as Benefit,
        6: {
          Cash: 50000,
          BenefitName: `Ship's Boat or two Ship Shares`,
        } as Benefit,
        7: {
          Cash: 50000,
          BenefitName: `SOC +2`,
        } as Benefit,
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Line/Crew', 'Engineer/Gunner', 'Flight'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.intelligenceTraining,
            5: this._trainingService.educationTraining,
            6: this._trainingService.socialStatusTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Line/Crew', 'Engineer/Gunner', 'Flight'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.pilotTraining,
            2: this._trainingService.vaccSuitTraining,
            3: this._trainingService.athleticsTraining,
            4: this._trainingService.gunnerTraining,
            5: this._trainingService.mechanicTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 10)`,
          Assignments: ['Line/Crew', 'Engineer/Gunner', 'Flight'],
          MinEDU: 10,
          Trainings: {
            1: this._trainingService.leadershipTraining,
            2: this._trainingService.electronicsTraining,
            3: this._trainingService.pilotTraining,
            4: this._trainingService.bladeTraining,
            5: this._trainingService.adminTraining,
            6: this._trainingService.navalTraining
          }
        } as TrainingTable,
        {
          Name: `Line/Crew`,
          Assignments: ['Line/Crew'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.mechanicTraining,
            3: this._trainingService.gunCombatTraining,
            4: this._trainingService.flyerTraining,
            5: this._trainingService.meleeTraining,
            6: this._trainingService.vaccSuitTraining
          }
        } as TrainingTable,
        {
          Name: `Engineer/Gunner`,
          Assignments: ['Engineer/Gunner'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.engineerTraining,
            2: this._trainingService.mechanicTraining,
            3: this._trainingService.electronicsTraining,
            4: this._trainingService.engineerTraining,
            5: this._trainingService.gunnerTraining,
            6: this._trainingService.flyerTraining
          }
        } as TrainingTable,
        {
          Name: `Flight`,
          Assignments: ['Flight'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.pilotTraining,
            2: this._trainingService.flyerTraining,
            3: this._trainingService.gunnerTraining,
            4: this._trainingService.smallCraftTraining,
            5: this._trainingService.astrogationTraining,
            6: this._trainingService.electronicsTraining
          }
        } as TrainingTable,
        {
          Name: `Officer (Commissioned Only)`,
          Assignments: ['Line/Crew Officer', 'Engineer/Gunner Officer', 'Flight Officer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.leadershipTraining,
            2: this._trainingService.electronicsTraining,
            3: this._trainingService.pilotTraining,
            4: this._trainingService.bladeTraining,
            5: this._trainingService.adminTraining,
            6: this._trainingService.navalTraining
          }
        } as TrainingTable,
        {
          Name: ``,
          Assignments: [],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.dummyTraining,
            2: this._trainingService.dummyTraining,
            3: this._trainingService.dummyTraining,
            4: this._trainingService.dummyTraining,
            5: this._trainingService.dummyTraining,
            6: this._trainingService.dummyTraining
          }
        } as TrainingTable,
        {
          Name: ``,
          Assignments: [],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.dummyTraining,
            2: this._trainingService.dummyTraining,
            3: this._trainingService.dummyTraining,
            4: this._trainingService.dummyTraining,
            5: this._trainingService.dummyTraining,
            6: this._trainingService.dummyTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Enlisted`,
          Assignments: [`Line/Crew`, `Engineer/Gunner`, `Flight`],
          Ranks: {
            0: {
              Name: `Crewman`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Able Spacehand`,
              Bonus: `${this._skillService.SkillNames.Mechanic} 1`,
            } as Rank,
            2: {
              Name: `Petty Officer, 3rd class`,
              Bonus: `${this._skillService.SkillNames.VaccSuit}`,
            } as Rank,
            3: {
              Name: `Petty Officer, 2nd class`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Petty Officer, 1st class`,
              Bonus: `END +1`,
            } as Rank,
            5: {
              Name: `Chief Petty Officer`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Master Chief`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Officer`,
          Assignments: [`Line/Crew Officer`, `Engineer/Gunner Officer`, `Flight Officer`],
          Ranks: {
            0: {
              Name: `-----Not a Rank-----`,
              Bonus: `----------`,
            } as Rank,
            1: {
              Name: `Ensign`,
              Bonus: `${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeBlade})`,
            } as Rank,
            2: {
              Name: `Sublieutenant`,
              Bonus: `${this._skillService.SkillNames.Leadership}`,
            } as Rank,
            3: {
              Name: `Lieutenant`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Commander`,
              Bonus: `${this._skillService.SkillNames.Tactics} (${this._skillService.SkillNames.TacticsNaval}) 1`,
            } as Rank,
            5: {
              Name: `Captain`,
              Bonus: `SOC 10 or SOC +1, whichever is higher`,
            } as Rank,
            6: {
              Name: `Admiral`,
              Bonus: `SOC 12 or SOC +1, whichever is higher`,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll
          twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `Placed in the frozen watch (cryogenically stored on board ship) and revived improperly. Reduce
          STR, DEX, or END by 1 due to muscle wastage. You are not ejected from this career.`,
        } as Mishap,
        3: {
          Description: `During a battle, defeat or victory depends on your actions. You must make an 8+ roll using a
          skill that depends on your branch:<br><strong>Line/Crew:</strong> ${this._skillService.SkillNames.Electronics}
           (${this._skillService.SkillNames.ElectronicsSensors}) or ${this._skillService.SkillNames.Gunner}
           <br><strong>Engineer/Gunner:</strong> ${this._skillService.SkillNames.Mechanic} or ${this._skillService.SkillNames.VaccSuit}
           <br><strong>Flight:</strong> ${this._skillService.SkillNames.Pilot} (${this._skillService.SkillNames.PilotSmallCraft} or
            ${this._skillService.SkillNames.PilotSpacecraft}) or ${this._skillService.SkillNames.Tactics}
            (${this._skillService.SkillNames.TacticsNaval})<br>If you <strong>fail</strong>, the ship suffers severe damage and you are
             blamed for the disaster. You are court-marshalled and discharged.<br>If you <strong>succeed</strong>, your efforts ensure that
            you are honourably discharged. You still leave th career, but you may keep your Benefit roll from this term.`,
        } as Mishap,
        4: {
          Description: `You are blamed for an accident that causes the death of several crew members. If you were
          responsible, then you gain one free roll on the Skills and Training table before you are ejected from this
          career as your guilt drives you to excel. If you were not, then gain the officer who blamed you as an Enemy,
          but you keep your Benefit roll from this term.`,
        } as Mishap,
        5: {
          Description: `You are tormented by or quarrel with an officer or fellow crewman. Gain that character as a Rival,
          as they force you out of the Navy.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You join a gambling circle on board. Gain ${this._skillService.SkillNames.Gambler} 1 or
          ${this._skillService.SkillNames.Deception} 1. If you wish, throw ${this._skillService.SkillNames.Gambler} +8.
          If you succeed, gain an extra Benefit roll from this career; if you fail, you lose one Benefit roll from this career.`,
        } as CareerEvent,
        4: {
          Description: `You are given a special assignment or duty on board ship. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        5: {
          Description: `You are given advanced training in a specialist field. Roll EDU 8+ to gain one level in any skill you already have.`,
        } as CareerEvent,
        6: {
          Description: `Your vessel participates in a notable military engagement. Gain one of
          ${this._skillService.SkillNames.Electronics} 1, ${this._skillService.SkillNames.Engineer} 1,
          ${this._skillService.SkillNames.Gunner} 1, or ${this._skillService.SkillNames.Pilot} 1`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `Your vessel participates in a diplomatic mission. Gain one of ${this._skillService.SkillNames.Recon} 1,
          ${this._skillService.SkillNames.Diplomat} 1, ${this._skillService.SkillNames.Steward} 1, or a Contact.`,
        } as CareerEvent,
        9: {
          Description: `You foil an attempted crime on board, such as mutiny, sabotage, smuggling, or conspiracy.
          Gain an Enemy, but also gain DM+2 to your next Advancement roll in the Navy.`,
        } as CareerEvent,
        10: {
          Description: `You have the opportunity to abuse your position for profit. If you do so, gain an extra Benefit
           roll from this term. Refuse, and you get DM+2 to your next Advancement roll.`,
        } as CareerEvent,
        11: {
          Description: `Your commanding officer takes an interest in your career. Either gain
           ${this._skillService.SkillNames.Tactics} (${this._skillService.SkillNames.TacticsNaval}) or DM+4 to your
           next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `You display heroism in battle, saving the whole ship. You automatically pass your next promotion
           or commission roll.`,
        } as CareerEvent,
      }
    } as Career,
    'Noble': {
      Name: `Noble`,
      Designation: '9',
      Description: `Individuals of the upper class who perform little consistent function, but often have large amounts of money.`,
      Qualification: {characteristic: `SOC`, target: 10, special: `DM-1 for every previous career<br>Automatic qualification if
                        your SOC is 10 or higher.`} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Administrator`,
          Description: `You serve in the planetary government or even ruled over a fiefdom or other domain.`,
          Survival: {characteristic: `INT`, target: 4} as Qualification,
          Advancement: {characteristic: `EDU`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Diplomat`,
          Description: `You are a diplomat or other state official.`,
          Survival: {characteristic: `INT`, target: 5} as Qualification,
          Advancement: {characteristic: `SOC`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: `Dilettante`,
          Description: `You are known for being known and have absolutely no useful function in society.`,
          Survival: {characteristic: `SOC`, target: 3} as Qualification,
          Advancement: {characteristic: `INT`, target: 8} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 10000,
          BenefitName: `Ship Share`,
        } as Benefit,
        2: {
          Cash: 10000,
          BenefitName: `Two Ship Shares`,
        } as Benefit,
        3: {
          Cash: 50000,
          BenefitName: `Blade`,
        } as Benefit,
        4: {
          Cash: 50000,
          BenefitName: `SOC +1`,
        } as Benefit,
        5: {
          Cash: 100000,
          BenefitName: `TAS Membership`,
        } as Benefit,
        6: {
          Cash: 100000,
          BenefitName: `Yacht`,
        } as Benefit,
        7: {
          Cash: 200000,
          BenefitName: `SOC +1 and Yacht`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Administrator', 'Diplomat', 'Dilettante'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.gamblerTraining,
            5: this._trainingService.gunCombatTraining,
            6: this._trainingService.meleeTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Administrator', 'Diplomat', 'Dilettante'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.adminTraining,
            2: this._trainingService.advocateTraining,
            3: this._trainingService.electronicsTraining,
            4: this._trainingService.diplomatTraining,
            5: this._trainingService.investigateTraining,
            6: this._trainingService.persuadeTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min EDU 8)`,
          Assignments: ['Administrator', 'Diplomat', 'Dilettante'],
          MinEDU: 8,
          Trainings: {
            1: this._trainingService.adminTraining,
            2: this._trainingService.advocateTraining,
            3: this._trainingService.languageTraining,
            4: this._trainingService.leadershipTraining,
            5: this._trainingService.diplomatTraining,
            6: this._trainingService.artTraining
          }
        } as TrainingTable,
        {
          Name: `Administrator`,
          Assignments: ['Administrator'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.adminTraining,
            2: this._trainingService.advocateTraining,
            3: this._trainingService.brokerTraining,
            4: this._trainingService.diplomatTraining,
            5: this._trainingService.leadershipTraining,
            6: this._trainingService.persuadeTraining
          }
        } as TrainingTable,
        {
          Name: `Diplomat`,
          Assignments: ['Diplomat'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.advocateTraining,
            2: this._trainingService.carouseTraining,
            3: this._trainingService.electronicsTraining,
            4: this._trainingService.stewardTraining,
            5: this._trainingService.diplomatTraining,
            6: this._trainingService.deceptionTraining
          }
        } as TrainingTable,
        {
          Name: `Dilettante`,
          Assignments: ['Dilettante'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.carouseTraining,
            2: this._trainingService.deceptionTraining,
            3: this._trainingService.flyerTraining,
            4: this._trainingService.streetwiseTraining,
            5: this._trainingService.gamblerTraining,
            6: this._trainingService.jackOfAllTradesTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Administrator`,
          Assignments: [`Administrator`],
          Ranks: {
            0: {
              Name: `Assistant`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Clerk`,
              Bonus: `${this._skillService.SkillNames.Admin} 1`,
            } as Rank,
            2: {
              Name: `Supervisor`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Manager`,
              Bonus: `${this._skillService.SkillNames.Advocate} 1`,
            } as Rank,
            4: {
              Name: `Chief`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Director`,
              Bonus: `${this._skillService.SkillNames.Leadership} 1`,
            } as Rank,
            6: {
              Name: `Minister`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Diplomat`,
          Assignments: [`Diplomat`],
          Ranks: {
            0: {
              Name: `Intern`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `3rd Secretary`,
              Bonus: `${this._skillService.SkillNames.Admin} 1`,
            } as Rank,
            2: {
              Name: `2nd Secretary`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `1st Secretary`,
              Bonus: `${this._skillService.SkillNames.Advocate} 1`,
            } as Rank,
            4: {
              Name: `Counsellor`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Minister`,
              Bonus: `${this._skillService.SkillNames.Diplomat} 1`,
            } as Rank,
            6: {
              Name: `Ambassador`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Dilettante`,
          Assignments: [`Dilettante`],
          Ranks: {
            0: {
              Name: `Wastrel`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            2: {
              Name: `Ingrate`,
              Bonus: `${this._skillService.SkillNames.Carouse} 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `Black Sheep`,
              Bonus: `${this._skillService.SkillNames.Persuade} 1`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Scoundrel`,
              Bonus: `${this._skillService.SkillNames.JackOfAllTrades} 1`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: ``,
          Assignments: [``],
          Ranks: {
            0: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            1: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            2: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            3: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            4: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            5: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            6: {
              Name: ``,
              Bonus: ``,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `A family scandal forces you out of your position. Lose one SOC.`,
        } as Mishap,
        3: {
          Description: `A disaster or war strikes. Roll ${this._skillService.SkillNames.Stealth} 8+ or
          ${this._skillService.SkillNames.Deception} 8+ to escape unhurt. If you fail, roll on the Injury Table.`,
        } as Mishap,
        4: {
          Description: `Political manoeuvrings usurp your position. Increase ${this._skillService.SkillNames.Diplomat}
          or ${this._skillService.SkillNames.Advocate} by one level and gain a Rival.`,
        } as Mishap,
        5: {
          Description: `An assassin attempts to end your life. Roll END 8+. If you fail, roll on the Injury Table.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You are challenged to a duel for your honour and standing. If you refuse, reduce your SOC by 1.
          If you accept, roll ${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeBlade}) 8+.
          If you succeed, gain one SOC. If you fail, roll on the Injury Table and reduce your SOC by one. Either way,
          gain one level in ${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeBlade}),
          ${this._skillService.SkillNames.Leadership}, ${this._skillService.SkillNames.Tactics}, or
          ${this._skillService.SkillNames.Deception}.`,
        } as CareerEvent,
        4: {
          Description: `Your time as a ruler or playboy gives you a wide range of experiences. Gain one of
          ${this._skillService.SkillNames.Animals} (${this._skillService.SkillNames.AnimalsHandling}) 1,
          ${this._skillService.SkillNames.Art} 1, ${this._skillService.SkillNames.Carouse} 1,
          or ${this._skillService.SkillNames.Streetwise} 1.`,
        } as CareerEvent,
        5: {
          Description: `You inherit a gift from a rich relative. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        6: {
          Description: `You become deeply involved in politics on your world of residence, becoming a player in the
           political intrigues of government. Gain one level in ${this._skillService.SkillNames.Advocate},
           ${this._skillService.SkillNames.Admin}, ${this._skillService.SkillNames.Diplomat}, or
           ${this._skillService.SkillNames.Persuade}, but also gain a Rival.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `A conspiracy of nobles attempts to recruit you. If you refuse, gain the conspiracy as an Enemy.
           If you accept, roll ${this._skillService.SkillNames.Deception} 8+ or ${this._skillService.SkillNames.Persuade}
           8+. If you fail, roll on the Mishap Table as the conspiracy collapses. If you succeed, gain one level of
           ${this._skillService.SkillNames.Deception}, ${this._skillService.SkillNames.Persuade},
           ${this._skillService.SkillNames.Tactics}, or ${this._skillService.SkillNames.Carouse}.`,
        } as CareerEvent,
        9: {
          Description: `Your reign is acclaimed by all as being fair and wise, or in the case of a dilettante, you
          sponge off your family's wealth a while longer. Gain either a jealous relative or an unhappy subject as an
           Enemy. Gain DM+2 to your next Advancement check.`,
        } as CareerEvent,
        10: {
          Description: `You manipulate and charm your way through high society. Gain one level of
           ${this._skillService.SkillNames.Carouse}, ${this._skillService.SkillNames.Diplomat},
           ${this._skillService.SkillNames.Persuade}, or ${this._skillService.SkillNames.Steward}, as well as a Rival
           or Enemy.`,
        } as CareerEvent,
        11: {
          Description: `You make an alliance with a powerful and charismatic noble, who becomes an Ally. Either gain
           one level of ${this._skillService.SkillNames.Leadership} or DM+4 to your next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `Your efforts do not go unnoticed by the Imperium. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Rogue': {
      Name: `Rogue`,
      Designation: '10',
      Description: `Criminal elements familiar with the rougher or more illegal methods of attaining goals.`,
      Qualification: {characteristic: `DEX`, target: 6, special: 'DM-1 for every previous career'} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Thief`,
          Description: `You steal from the rich and give to... well, yourself, actually.`,
          Survival: {characteristic: `INT`, target: 6} as Qualification,
          Advancement: {characteristic: `DEX`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Enforcer`,
          Description: `You are a leg breaker, thug, or assassin for a criminal group.`,
          Survival: {characteristic: `END`, target: 6} as Qualification,
          Advancement: {characteristic: `STR`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Pirate`,
          Description: `You are a space-going corsair.`,
          Survival: {characteristic: `DEX`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 6} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 0,
          BenefitName: `Ship Share`,
        } as Benefit,
        2: {
          Cash: 0,
          BenefitName: `Weapon`,
        } as Benefit,
        3: {
          Cash: 10000,
          BenefitName: `INT +1`,
        } as Benefit,
        4: {
          Cash: 10000,
          BenefitName: `1D Ship Shares`,
        } as Benefit,
        5: {
          Cash: 50000,
          BenefitName: `Armour`,
        } as Benefit,
        6: {
          Cash: 100000,
          BenefitName: `DEX +1`,
        } as Benefit,
        7: {
          Cash: 100000,
          BenefitName: `2D Ship Shares`,
        } as Benefit,
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Thief', 'Enforcer', 'Pirate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.carouseTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.gamblerTraining,
            5: this._trainingService.meleeTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Thief', 'Enforcer', 'Pirate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.deceptionTraining,
            2: this._trainingService.reconTraining,
            3: this._trainingService.athleticsTraining,
            4: this._trainingService.gunCombatTraining,
            5: this._trainingService.stealthTraining,
            6: this._trainingService.streetwiseTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 10)`,
          Assignments: ['Thief', 'Enforcer', 'Pirate'],
          MinEDU: 10,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.navigationTraining,
            3: this._trainingService.medicTraining,
            4: this._trainingService.investigateTraining,
            5: this._trainingService.brokerTraining,
            6: this._trainingService.advocateTraining
          }
        } as TrainingTable,
        {
          Name: `Thief`,
          Assignments: ['Thief'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.stealthTraining,
            2: this._trainingService.electronicsTraining,
            3: this._trainingService.reconTraining,
            4: this._trainingService.streetwiseTraining,
            5: this._trainingService.deceptionTraining,
            6: this._trainingService.athleticsTraining
          }
        } as TrainingTable,
        {
          Name: `Enforcer`,
          Assignments: ['Enforcer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.gunCombatTraining,
            2: this._trainingService.meleeTraining,
            3: this._trainingService.streetwiseTraining,
            4: this._trainingService.persuadeTraining,
            5: this._trainingService.athleticsTraining,
            6: this._trainingService.driveTraining
          }
        } as TrainingTable,
        {
          Name: `Pirate`,
          Assignments: ['Pirate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.pilotTraining,
            2: this._trainingService.astrogationTraining,
            3: this._trainingService.gunnerTraining,
            4: this._trainingService.engineerTraining,
            5: this._trainingService.vaccSuitTraining,
            6: this._trainingService.meleeTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Thief`,
          Assignments: [`Thief`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Stealth} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Streetwise} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Recon}`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Enforcer`,
          Assignments: [`Enforcer`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Persuade} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.GunCombat} 1 or ${this._skillService.SkillNames.Melee} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Streetwise} 1`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Pirate`,
          Assignments: [`Pirate`],
          Ranks: {
            0: {
              Name: `Lackey`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Henchman`,
              Bonus: `${this._skillService.SkillNames.Pilot} 1 or ${this._skillService.SkillNames.Gunner} 1`,
            } as Rank,
            2: {
              Name: `Corporal`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Sergeant`,
              Bonus: `${this._skillService.SkillNames.GunCombat} 1 or ${this._skillService.SkillNames.Melee} 1`,
            } as Rank,
            4: {
              Name: `Lieutenant`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Leader`,
              Bonus: `${this._skillService.SkillNames.Engineer} 1 or ${this._skillService.SkillNames.Navigation} 1`,
            } as Rank,
            6: {
              Name: `Captain`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: ``,
          Assignments: [``],
          Ranks: {
            0: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            1: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            2: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            3: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            4: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            5: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            6: {
              Name: ``,
              Bonus: ``,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `Arrested. You must take the Prisoner career in your next term.`,
        } as Mishap,
        3: {
          Description: `Betrayed by a friend. One of your Contacts or Allies betrays you, ending your career. That
          Contact or Ally becomes a Rival or Enemy. If you have not Contacts or Enemies, then you are betrayed by
          someone you never saw coming and still gain a Rival or Enemy. In addition, roll 2D. If you roll 2, you must
          take the Prisoner career in your next term.`,
        } as Mishap,
        4: {
          Description: `A job goes wrong, forcing you to flee off-planet. Gain one of ${this._skillService.SkillNames.Deception} 1,
          ${this._skillService.SkillNames.Pilot} (${this._skillService.SkillNames.PilotSmallCraft} or
          ${this._skillService.SkillNames.PilotSpacecraft}) 1, ${this._skillService.SkillNames.Athletics}
          (${this._skillService.SkillNames.AthleticsDexterity}) 1, or ${this._skillService.SkillNames.Gunner} 1.`,
        } as Mishap,
        5: {
          Description: `A police detective or rival criminal forces you to flee and vows to hunt you down. Gain an Enemy.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You are arrested and charged. You can choose to defend yourself, or hire a lawyer. If you
          defend yourself, roll ${this._skillService.SkillNames.Advocate} 8+. If you succeed, the charges are dropped.
          If you fail, gain an Enemy and take the Prisoner career in your next term. If you hired a lawyer, gain the
          lawyer as a Contact and lose a Benefit roll.`,
        } as CareerEvent,
        4: {
          Description: `You are involved in the planning of an impressive heist. Gain ${this._skillService.SkillNames.Electronics} 1
          or ${this._skillService.SkillNames.Mechanic} 1.`,
        } as CareerEvent,
        5: {
          Description: `One of your crimes pays off. Gain DM+2 to any one Benefit roll, and gain your victim as an Enemy.`,
        } as CareerEvent,
        6: {
          Description: `You have the opportunity to backstab a fellow rogue for personal gain. If you do so, gain DM+4
          to your next Advancement check. If you refuse, gain them as an Ally.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You spend months in the dangerous criminal underworld. Gain one of ${this._skillService.SkillNames.Streetwise} 1,
          ${this._skillService.SkillNames.Stealth} 1, ${this._skillService.SkillNames.Melee} 1, or
          ${this._skillService.SkillNames.GunCombat} 1.`,
        } as CareerEvent,
        9: {
          Description: `You become involved in a feud with a rival criminal organization. Roll
           ${this._skillService.SkillNames.Stealth} or ${this._skillService.SkillNames.GunCombat} 8+. If you fail,
           roll on the Injury table. If you succeed, gain an extra Benefit roll.`,
        } as CareerEvent,
        10: {
          Description: `You are involved in a gambling ring. Gain ${this._skillService.SkillNames.Gambler} 1. You may
          wager any number of Benefit rolls. Roll ${this._skillService.SkillNames.Gambler} 8+. If you fail, lose all
          the wagered Benefit rolls. If you succeed, gain half as many Benefit rolls as you wagered (round up).`,
        } as CareerEvent,
        11: {
          Description: `A crime lord considers you his protege. Either gain ${this._skillService.SkillNames.Tactics}
          (${this._skillService.SkillNames.TacticsMilitary}) 1 or DM+4 to your next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `You commit a legendary crime. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Scholar': {
      Name: `Scholar`,
      Designation: '11',
      Description: `Individuals trained in technological or research sciences who conduct scientific investigations
      into materials, situations, and phenomena; or who practice medicine.`,
      Qualification: {characteristic: `INT`, target: 6, special: 'DM-1 for every previous career'} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Field Researcher`,
          Description: `You are an explorer or field researcher, equally at home in the laboratory or wilderness.`,
          Survival: {characteristic: `END`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Scientist`,
          Description: `You are a researcher in some corporation or research institution, or a mad scientist in an
          orbiting laboratory.`,
          Survival: {characteristic: `EDU`, target: 4} as Qualification,
          Advancement: {characteristic: `INT`, target: 8} as Qualification,
        } as Assignment,
        {
          Name: `Physician`,
          Description: `You are a doctor, healer, or medical researcher.`,
          Survival: {characteristic: `EDU`, target: 4} as Qualification,
          Advancement: {characteristic: `EDU`, target: 8} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 5000,
          BenefitName: `INT +1`,
        } as Benefit,
        2: {
          Cash: 10000,
          BenefitName: `EDU +1`,
        } as Benefit,
        3: {
          Cash: 20000,
          BenefitName: `Two Ship Shares`,
        } as Benefit,
        4: {
          Cash: 30000,
          BenefitName: `SOC +1`,
        } as Benefit,
        5: {
          Cash: 40000,
          BenefitName: `Scientific Equipment`,
        } as Benefit,
        6: {
          Cash: 60000,
          BenefitName: `Lab Ship`,
        } as Benefit,
        7: {
          Cash: 100000,
          BenefitName: `Lab Ship`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Field Researcher', 'Scientist', 'Physician'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.intelligenceTraining,
            2: this._trainingService.educationTraining,
            3: this._trainingService.socialStatusTraining,
            4: this._trainingService.dexterityTraining,
            5: this._trainingService.enduranceTraining,
            6: this._trainingService.languageTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Field Researcher', 'Scientist', 'Physician'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.driveTraining,
            2: this._trainingService.electronicsTraining,
            3: this._trainingService.diplomatTraining,
            4: this._trainingService.medicTraining,
            5: this._trainingService.investigateTraining,
            6: this._trainingService.scienceTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 10)`,
          Assignments: ['Field Researcher', 'Scientist', 'Physician'],
          MinEDU: 10,
          Trainings: {
            1: this._trainingService.artTraining,
            2: this._trainingService.advocateTraining,
            3: this._trainingService.electronicsTraining,
            4: this._trainingService.languageTraining,
            5: this._trainingService.engineerTraining,
            6: this._trainingService.scienceTraining
          }
        } as TrainingTable,
        {
          Name: `Field Researcher`,
          Assignments: ['Field Researcher'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.vaccSuitTraining,
            3: this._trainingService.navigationTraining,
            4: this._trainingService.survivalTraining,
            5: this._trainingService.investigateTraining,
            6: this._trainingService.scienceTraining
          }
        } as TrainingTable,
        {
          Name: `Scientist`,
          Assignments: ['Scientist'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.adminTraining,
            2: this._trainingService.engineerTraining,
            3: this._trainingService.scienceTraining,
            4: this._trainingService.scienceTraining,
            5: this._trainingService.electronicsTraining,
            6: this._trainingService.scienceTraining
          }
        } as TrainingTable,
        {
          Name: `Physician`,
          Assignments: ['Physician'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.medicTraining,
            2: this._trainingService.electronicsTraining,
            3: this._trainingService.investigateTraining,
            4: this._trainingService.medicTraining,
            5: this._trainingService.persuadeTraining,
            6: this._trainingService.scienceTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Field Researcher`,
          Assignments: [`Field Researcher`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Science}`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsComputers}) 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Investigate} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Science} 2`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Scientist`,
          Assignments: [`Scientist`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Science} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsComputers}) 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Investigate} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Science} 2`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Physician`,
          Assignments: [`Physician`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Medic} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Science} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Science} 2`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: ``,
          Assignments: [``],
          Ranks: {
            0: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            1: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            2: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            3: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            4: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            5: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            6: {
              Name: ``,
              Bonus: ``,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `A disaster leaves several injured, and others blame you, forcing you to leave your career. Roll
          on the Injury Table twice, taking the higher result, and gain a Rival.`,
        } as Mishap,
        3: {
          Description: `The planetary government interferes with your research for political or religious reasons.
          If you continue with your work openly, increase ${this._skillService.SkillNames.Science} by one level and
          gain an Enemy. If you continue with your work secretly, increase science by one level and reduce your SOC by 2.
          This mishap does not cause you to leave this career.`,
        } as Mishap,
        4: {
          Description: `An expedition or voyage goes wrong, leaving you stranded in the wilderness. Gain
          ${this._skillService.SkillNames.Survival} 1 or ${this._skillService.SkillNames.Athletics}
          (${this._skillService.SkillNames.AthleticsDexterity} or ${this._skillService.SkillNames.AthleticsEndurance}) 1.
          By the time you find your way home, your job is gone.`,
        } as Mishap,
        5: {
          Description: `Your work is sabotaged by unknown parties. You may salvage what you can and give up (leave the
          career, but retain this term's Benefit roll) or start again from scratch (lose all Benefit rolls from this
          career, but you do not have to leave).`,
        } as Mishap,
        6: {
          Description: `A rival researcher blackens your name or steals your research. Gain a Rival, but you do not
          have to leave this career.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You are called upon to perform research that goes against your conscience. Accept, and you gain
          an extra Benefit roll, a level in each of any two Science skill specialties, and D3 Enemies.`,
        } as CareerEvent,
        4: {
          Description: `You are assigned to work on a secret project for a patron or organization. Gain one of
          ${this._skillService.SkillNames.Medic} 1, ${this._skillService.SkillNames.Science} 1,
          ${this._skillService.SkillNames.Engineer} 1, ${this._skillService.SkillNames.Electronics} 1, or
          ${this._skillService.SkillNames.Investigate} 1.`,
        } as CareerEvent,
        5: {
          Description: `You win a prestigious prize for your work, garnering both the praise and envy of your peers.
          Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        6: {
          Description: `You are given advanced training in a specialist field. Roll EDU 8+ to gain any one skill of
          your choice at level 1.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You have the opportunity to cheat in some fashion, advancing your career and research by
          stealing another's work, using an alien device, taking a shortcut, and so forth. If you refuse, you gain
          nothing. If you accept, roll ${this._skillService.SkillNames.Deception} 8+ or
          ${this._skillService.SkillNames.Admin} 8+. If you succeed, you gain D<+2 to any one Benefit roll and may
          increase any skill by one level, but also gain an Enemy. If you fail, gain an Enemy and lose one Benefit roll
          from this career.`,
        } as CareerEvent,
        9: {
          Description: `You make a breakthrough in your field. Gain DM+2 to your next Advancement check.`,
        } as CareerEvent,
        10: {
          Description: `You become entangled in a bureaucratic or legal morass that distracts you from your work.
          Gain ${this._skillService.SkillNames.Admin} 1, ${this._skillService.SkillNames.Advocate} 1,
          ${this._skillService.SkillNames.Persuade} 1, or ${this._skillService.SkillNames.Diplomat} 1.`,
        } as CareerEvent,
        11: {
          Description: `You work for an eccentric, but brilliant mentor, who becomes an Ally. Either increase
          ${this._skillService.SkillNames.Science} by one level or DM+4 to your next Advancement roll thanks to their aid.`,
        } as CareerEvent,
        12: {
          Description: `Your work leads to a considerable breakthrough. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Scout': {
      Name: `Scout`,
      Designation: '12',
      Description: `Members of the Exploratory service. Scouts explore new areas, map and survey known or newly discovered
      areas, and maintain communication ships which carry information and messages between the worlds of the galaxy.`,
      Qualification: {characteristic: `INT`, target: 5, special: 'DM-1 for every previous career'} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Courier`,
          Description: `You are responsible for shuttling messages and high value packages around the galaxy.`,
          Survival: {characteristic: `END`, target: 5} as Qualification,
          Advancement: {characteristic: `EDU`, target: 9} as Qualification,
        } as Assignment,
        {
          Name: `Surveyor`,
          Description: `You visit border worlds and assess their worth.`,
          Survival: {characteristic: `END`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 8} as Qualification,
        } as Assignment,
        {
          Name: `Explorer`,
          Description: `You go wherever the map is blank, exploring unknown worlds and uncharted space.`,
          Survival: {characteristic: `END`, target: 7} as Qualification,
          Advancement: {characteristic: `EDU`, target: 7} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 20000,
          BenefitName: `Ship Share`,
        } as Benefit,
        2: {
          Cash: 20000,
          BenefitName: `INT +1`,
        } as Benefit,
        3: {
          Cash: 30000,
          BenefitName: `EDU +1`,
        } as Benefit,
        4: {
          Cash: 30000,
          BenefitName: `Weapon`,
        } as Benefit,
        5: {
          Cash: 50000,
          BenefitName: `Weapon`,
        } as Benefit,
        6: {
          Cash: 50000,
          BenefitName: `Scout Ship`,
        } as Benefit,
        7: {
          Cash: 50000,
          BenefitName: `Scout Ship`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Courier', 'Surveyor', 'Explorer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.dexterityTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.intelligenceTraining,
            5: this._trainingService.educationTraining,
            6: this._trainingService.jackOfAllTradesTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Courier', 'Surveyor', 'Explorer'],
          MinEDU: 0,
          Trainings: {
            1: {
              BenefitName: `${this._skillService.SkillNames.Pilot} (${this._skillService.SkillNames.PilotSmallCraft} or ${this._skillService.SkillNames.PilotSpacecraft})`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.PilotSmallCraft,
                this._skillService.SkillNames.PilotSpacecraft,
              ]
            } as Training,
            2: this._trainingService.survivalTraining,
            3: this._trainingService.mechanicTraining,
            4: this._trainingService.astrogationTraining,
            5: this._trainingService.vaccSuitTraining,
            6: this._trainingService.gunCombatTraining
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 8)`,
          Assignments: ['Courier', 'Surveyor', 'Explorer'],
          MinEDU: 8,
          Trainings: {
            1: this._trainingService.medicTraining,
            2: this._trainingService.navigationTraining,
            3: this._trainingService.seafarerTraining,
            4: this._trainingService.explosivesTraining,
            5: this._trainingService.scienceTraining,
            6: this._trainingService.jackOfAllTradesTraining
          }
        } as TrainingTable,
        {
          Name: `Courier`,
          Assignments: ['Courier'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.flyerTraining,
            3: this._trainingService.spacecraftTraining,
            4: this._trainingService.engineerTraining,
            5: this._trainingService.athleticsTraining,
            6: this._trainingService.astrogationTraining
          }
        } as TrainingTable,
        {
          Name: `Surveyor`,
          Assignments: ['Surveyor'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.persuadeTraining,
            3: this._trainingService.pilotTraining,
            4: this._trainingService.navigationTraining,
            5: this._trainingService.diplomatTraining,
            6: this._trainingService.streetwiseTraining
          }
        } as TrainingTable,
        {
          Name: `Explorer`,
          Assignments: ['Explorer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.electronicsTraining,
            2: this._trainingService.pilotTraining,
            3: this._trainingService.engineerTraining,
            4: this._trainingService.scienceTraining,
            5: this._trainingService.stealthTraining,
            6: this._trainingService.reconTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Title`,
          Assignments: [`Courier`, `Surveyor`, `Explorer`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Scout`,
              Bonus: `${this._skillService.SkillNames.VaccSuit} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Pilot} 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: ``,
          Assignments: [``],
          Ranks: {
            0: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            1: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            2: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            3: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            4: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            5: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            6: {
              Name: ``,
              Bonus: ``,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `Psychologically damaged by your time in the scouts. Reduce your INT or SOC by 1.`,
        } as Mishap,
        3: {
          Description: `Your ship is damaged, and you have to hitchhike your way back across eht starts to the nearest
          scout base. Gain 1D Contacts and D3 Enemies.`,
        } as Mishap,
        4: {
          Description: `You inadvertently cause a conflict between the Imperium and a minor world or race. Gain a Rival
          and ${this._skillService.SkillNames.Diplomat} 1`,
        } as Mishap,
        5: {
          Description: `You have no idea what happened to you - they found your ship drifting on the fringes of friendly space.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `Your ship is ambushed by enemy vessels. Either run, and roll ${this._skillService.SkillNames.Pilot} 8+
          to escape, or treat with them and roll ${this._skillService.SkillNames.Persuade} 10+ to bargain with them. If
          you fail the check, then your ship is destroyed and you may not re-enlist in the Scouts at the end of this term.
          If you succeed, you survive and gain ${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsSensors}) 1.
          Either way, gain an Enemy.`,
        } as CareerEvent,
        4: {
          Description: `You survey an alien world. Gain one of ${this._skillService.SkillNames.Animals} (${this._skillService.SkillNames.AnimalsHandling}
          or ${this._skillService.SkillNames.AnimalsTraining}) 1, ${this._skillService.SkillNames.Survival} 1,
          ${this._skillService.SkillNames.Recon} 1, or ${this._skillService.SkillNames.Science} 1.`,
        } as CareerEvent,
        5: {
          Description: `You perform an exemplary service for the scouts. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        6: {
          Description: `You spend several years jumping from world to world in your scout ship. Gain one of
          ${this._skillService.SkillNames.Astrogation} 1, ${this._skillService.SkillNames.Electronics} 1,
          ${this._skillService.SkillNames.Navigation} 1, ${this._skillService.SkillNames.Pilot}
          (${this._skillService.SkillNames.PilotSmallCraft}) 1, or ${this._skillService.SkillNames.Mechanic} 1.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `When dealing with an alien race, you have an opportunity to gather extra intelligence about them.
          Roll either ${this._skillService.SkillNames.Electronics} 8+ or ${this._skillService.SkillNames.Deception} 8+.
          If you succeed, gain an Ally in the Imperium and DM+2 to your next Advancement roll. If you fail, roll on the
          Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        9: {
          Description: `Your scout ship is one of the first on the scene to rescue the survivors of a disaster. Roll either
          ${this._skillService.SkillNames.Medic} 8+ or ${this._skillService.SkillNames.Engineer} 8+. If you succeed,
          gain a Contact and DM+2 to your next Advancement check. If you fail, gain an Enemy.`,
        } as CareerEvent,
        10: {
          Description: `You spend a great deal of time on the fringes of Charted Space. Roll ${this._skillService.SkillNames.Survival} 8+
          or ${this._skillService.SkillNames.Pilot} 8+. If you succeed, gain a contact in an alien race and one level in
          any skill of your choice. If you fail, roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        11: {
          Description: `You serve as the courier for an important message from the Imperium. Either gain one level of
          ${this._skillService.SkillNames.Diplomat} or DM+4 to your next Advancement roll.`,
        } as CareerEvent,
        12: {
          Description: `You discover a world, item, or information of worth to the Imperium. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
    'Prisoner': {
      Name: `Prisoner`,
      Designation: '13',
      Description: `Every society has its bad apples, and even in the far future, punishments usually take place within
      faceless institutions where criminals can be conveniently forgotten.`,
      Qualification: {characteristic: ``, target: 0, special: 'One does not \'qualify\' for prison; you got sentenced' +
          'there for a crime you may or may not have committed.'} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Inmate`,
          Description: `You just try to get through your time in prison without getting into trouble.`,
          Survival: {characteristic: `END`, target: 7} as Qualification,
          Advancement: {characteristic: `STR`, target: 7} as Qualification,
        } as Assignment,
        {
          Name: `Thug`,
          Description: `You are part of a gang in prison, terrorizing the other inmates.`,
          Survival: {characteristic: `STR`, target: 8} as Qualification,
          Advancement: {characteristic: `END`, target: 6} as Qualification,
        } as Assignment,
        {
          Name: `Fixer`,
          Description: `You can arrange anything - for the right price.`,
          Survival: {characteristic: `INT`, target: 9} as Qualification,
          Advancement: {characteristic: `END`, target: 5} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 0,
          BenefitName: `Contact`,
        } as Benefit,
        2: {
          Cash: 0,
          BenefitName: `Blade`,
        } as Benefit,
        3: {
          Cash: 100,
          BenefitName: `${this._skillService.SkillNames.Deception} +1, ${this._skillService.SkillNames.Persuade} +1, or ${this._skillService.SkillNames.Stealth} +1`,
        } as Benefit,
        4: {
          Cash: 200,
          BenefitName: `Ally`,
        } as Benefit,
        5: {
          Cash: 500,
          BenefitName: `${this._skillService.SkillNames.Melee} +1, ${this._skillService.SkillNames.Recon} +1, or ${this._skillService.SkillNames.Streetwise} +1`,
        } as Benefit,
        6: {
          Cash: 1000,
          BenefitName: `STR +1 or END +1`,
        } as Benefit,
        7: {
          Cash: 2500,
          BenefitName: `${this._skillService.SkillNames.Deception} +1, ${this._skillService.SkillNames.Persuade} +1, or ${this._skillService.SkillNames.Stealth} +1`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Inmate', 'Thug', 'Fixer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.strengthTraining,
            2: this._trainingService.unarmedTraining,
            3: this._trainingService.enduranceTraining,
            4: this._trainingService.jackOfAllTradesTraining,
            5: this._trainingService.educationTraining,
            6: this._trainingService.gamblerTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Inmate', 'Thug', 'Fixer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.athleticsTraining,
            2: this._trainingService.deceptionTraining,
            3: this._trainingService.professionTraining,
            4: this._trainingService.streetwiseTraining,
            5: this._trainingService.unarmedTraining,
            6: this._trainingService.persuadeTraining
          }
        } as TrainingTable,
        {
          Name: ``,
          Assignments: [],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.dummyTraining,
            2: this._trainingService.dummyTraining,
            3: this._trainingService.dummyTraining,
            4: this._trainingService.dummyTraining,
            5: this._trainingService.dummyTraining,
            6: this._trainingService.dummyTraining
          }
        } as TrainingTable,
        {
          Name: `Inmate`,
          Assignments: ['Inmate'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.stealthTraining,
            2: this._trainingService.unarmedTraining,
            3: this._trainingService.streetwiseTraining,
            4: this._trainingService.survivalTraining,
            5: this._trainingService.athleticsStrengthTraining,
            6: this._trainingService.gamblerTraining
          }
        } as TrainingTable,
        {
          Name: `Thug`,
          Assignments: ['Thug'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.persuadeTraining,
            2: this._trainingService.unarmedTraining,
            3: this._trainingService.unarmedTraining,
            4: this._trainingService.bladeTraining,
            5: this._trainingService.athleticsStrengthTraining,
            6: this._trainingService.athleticsStrengthTraining
          }
        } as TrainingTable,
        {
          Name: `Fixer`,
          Assignments: ['Fixer'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.investigateTraining,
            2: this._trainingService.brokerTraining,
            3: this._trainingService.deceptionTraining,
            4: this._trainingService.streetwiseTraining,
            5: this._trainingService.stealthTraining,
            6: this._trainingService.adminTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Prisoner`,
          Assignments: [`Inmate`, `Thug`, `Fixer`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeUnarmed}) 1`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Athletics} 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.Advocate}`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `EDU +1`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: ``,
          Assignments: [``],
          Ranks: {
            0: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            1: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            2: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            3: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            4: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            5: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            6: {
              Name: ``,
              Bonus: ``,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `You are accused of assaulting a prison guard. Parole Threshold +2.`,
        } as Mishap,
        3: {
          Description: `A prison gang persecutes you. You may choose to fight back if you wish, but if you do not, you
           lose all Benefit rolls from your Prison career. If you fight back, roll ${this._skillService.SkillNames.Melee}
           (${this._skillService.SkillNames.MeleeUnarmed}) 8+. Fail, and you must roll twice on the Injury Table and
           take the lower result. Succeed, and you gain an Enemy and raise Parole Threshold by +1.`,
        } as Mishap,
        4: {
          Description: `A guard takes a dislike to you. Gain an Enemy and raise your Parole Threshold by +1.`,
        } as Mishap,
        5: {
          Description: `Disgraced. Word of your criminal past reaches your homeworld. Lose 1 SOC.`,
        } as Mishap,
        6: {
          Description: `Injured. Roll on the Injury Table.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `You have the opportunity to escape the prison. If you take this opportunity, roll either
          ${this._skillService.SkillNames.Stealth} 10+ or ${this._skillService.SkillNames.Deception} 10+. Succeed,
          and you leave this career. Fail, and raise your Parole Threshold by +2.`,
        } as CareerEvent,
        4: {
          Description: `You are assigned to difficult or backbreaking labour. Roll END 8+. If you fail, increase your
          Parole Threshold by +1. Succeed, and you may reduce your Parole Threshold by 1 and gain any one of the
          following skills: ${this._skillService.SkillNames.Athletics}, ${this._skillService.SkillNames.Mechanic} 1,
          ${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeUnarmed}) 1.`,
        } as CareerEvent,
        5: {
          Description: `You have the opportunity to join a gang. Make a ${this._skillService.SkillNames.Persuade} 8+
          or ${this._skillService.SkillNames.Melee} 8+ check to do so. If you fail, you gain an Enemy. Succeed, and
          you must raise your Parole Threshold by +1, but you gain DM+1 to all Survival rolls in this career and may
          gain any one of the following skills: ${this._skillService.SkillNames.Deception} 1, ${this._skillService.SkillNames.Persuade} 1,
          ${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeUnarmed}) 1, or
          ${this._skillService.SkillNames.Stealth} 1.`,
        } as CareerEvent,
        6: {
          Description: `Vocational Training. Roll EDU 8+ to gain any one skill except ${this._skillService.SkillNames.JackOfAllTrades}.`,
        } as CareerEvent,
        7: {
          Description: `Prison Event. Roll 1D:
          <br>1. Riot. A riot engulfs the prison. Roll 1D. On a 1-2, you are injured - roll on the Injury Table. On a 5-6,
          you are able to loot something useful; gain an extra Benefit roll this term.
          <br>2. New Contact. You make friends with another inmate; gain them as a Contact.
          <br>3. New Rival. You gain a new Rival from among the inmates or guards.
          <br>4. Transferred. You are moved to a different prison. Re-roll your Parole Threshold.
          <br>5. Good Behaviour. Reduce your Parole Threshold by -2.
          <br>6. You are attacked by another prisoner. Roll ${this._skillService.SkillNames.Melee} (${this._skillService.SkillNames.MeleeUnarmed}) 8+.
          If you fail, roll on the Injury Table.`,
        } as CareerEvent,
        8: {
          Description: `Parole hearing. Reduce your Parole Threshold by -1.`,
        } as CareerEvent,
        9: {
          Description: `You have the opportunity to hire a new lawyer. He costs Cr1000 x his
          ${this._skillService.SkillNames.Advocate} skill squared. Roll 2D + your lawyer's ${this._skillService.SkillNames.Advocate}
          skill; on an  8+, reduce your Parole Threshold by 1D.`,
        } as CareerEvent,
        10: {
          Description: `Special Duty. You are given a special responsibility in the prison. Gain one of
          ${this._skillService.SkillNames.Admin} 1, ${this._skillService.SkillNames.Advocate} 1,
          ${this._skillService.SkillNames.Electronics} (${this._skillService.SkillNames.ElectronicsComputers}) 1,
          ${this._skillService.SkillNames.Steward} 1.`,
        } as CareerEvent,
        11: {
          Description: `The warden takes an interest in your case. Reduce your Parole Threshold by -2.`,
        } as CareerEvent,
        12: {
          Description: `Heroism. You have the opportunity to save a guard or prison officer. If you take the risk, roll 2D.
          On a 7 or less, roll on the Injury Table. On 8+, gain an Ally and reduce your Parole Threshold by -2.`,
        } as CareerEvent,
      }
    } as Career,
    'Psion': {
      Name: `Psion`,
      Designation: 'X',
      Description: `A career for Travellers who choose to focus on their psionic potential instead of more conventional lifestyles.`,
      Qualification: {characteristic: `PSI`, target: 6, special: 'DM-1 for every previous career'} as Qualification,
      Commission: {characteristic: ``, target: 0} as Qualification,
      Assignments: [
        {
          Name: `Wild Talent`,
          Description: `You developed your powers without formal training.`,
          Survival: {characteristic: `SOC`, target: 6} as Qualification,
          Advancement: {characteristic: `INT`, target: 8} as Qualification,
        } as Assignment,
        {
          Name: `Adept`,
          Description: `You are a scholar of the psionic disciplines.`,
          Survival: {characteristic: `EDU`, target: 4} as Qualification,
          Advancement: {characteristic: `EDU`, target: 8} as Qualification,
        } as Assignment,
        {
          Name: `Psi-Warrior`,
          Description: `You combine combat training with psionic warfare.`,
          Survival: {characteristic: `END`, target: 6} as Qualification,
          Advancement: {characteristic: `END`, target: 6} as Qualification,
        } as Assignment
      ],
      BenefitTable: {
        1: {
          Cash: 1000,
          BenefitName: `Gun`,
        } as Benefit,
        2: {
          Cash: 2000,
          BenefitName: `2 Ship Shares`,
        } as Benefit,
        3: {
          Cash: 4000,
          BenefitName: `Contact`,
        } as Benefit,
        4: {
          Cash: 4000,
          BenefitName: `TAS Membership`,
        } as Benefit,
        5: {
          Cash: 8000,
          BenefitName: `Contact`,
        } as Benefit,
        6: {
          Cash: 8000,
          BenefitName: `Combat Implant`,
        } as Benefit,
        7: {
          Cash: 16000,
          BenefitName: `10 Ship Shares`,
        } as Benefit
      },
      TrainingTables: [
        {
          Name: `Personal Development`,
          Assignments: ['Wild Talent', 'Adept', 'Psi-Warrior'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.educationTraining,
            2: this._trainingService.intelligenceTraining,
            3: this._trainingService.strengthTraining,
            4: this._trainingService.dexterityTraining,
            5: this._trainingService.enduranceTraining,
            6: this._trainingService.psiTraining
          }
        } as TrainingTable,
        {
          Name: `Service Skills`,
          Assignments: ['Wild Talent', 'Adept', 'Psi-Warrior'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.telepathyTraining,
            2: this._trainingService.clairvoyanceTraining,
            3: this._trainingService.telekinesisTraining,
            4: this._trainingService.awarenessTraining,
            5: this._trainingService.teleportationTraining,
            6: {
              BenefitName: `Any Psi Talent`,
            } as Training
          }
        } as TrainingTable,
        {
          Name: `Advanced Education (Min. EDU 8)`,
          Assignments: ['Wild Talent', 'Adept', 'Psi-Warrior'],
          MinEDU: 8,
          Trainings: {
            1: this._trainingService.languageTraining,
            2: this._trainingService.artTraining,
            3: this._trainingService.electronicsTraining,
            4: this._trainingService.medicTraining,
            5: this._trainingService.scienceTraining,
            6: this._trainingService.mechanicTraining
          }
        } as TrainingTable,
        {
          Name: `Wild Talent`,
          Assignments: ['Wild Talent'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.telepathyTraining,
            2: this._trainingService.telekinesisTraining,
            3: this._trainingService.deceptionTraining,
            4: this._trainingService.stealthTraining,
            5: this._trainingService.streetwiseTraining,
            6: {
              BenefitName: `${this._skillService.SkillNames.Melee} or ${this._skillService.SkillNames.GunCombat}`,
              Type: 'skill',
              SkillNames: [
                this._skillService.SkillNames.MeleeUnarmed,
                this._skillService.SkillNames.MeleeBlade,
                this._skillService.SkillNames.MeleeBludgeon,
                this._skillService.SkillNames.GunCombatArchaic,
                this._skillService.SkillNames.GunCombatEnergy,
                this._skillService.SkillNames.GunCombatSlug,
              ]
            } as Training
          }
        } as TrainingTable,
        {
          Name: `Adept`,
          Assignments: ['Adept'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.telepathyTraining,
            2: this._trainingService.clairvoyanceTraining,
            3: this._trainingService.awarenessTraining,
            4: this._trainingService.medicTraining,
            5: this._trainingService.persuadeTraining,
            6: this._trainingService.scienceTraining
          }
        } as TrainingTable,
        {
          Name: `Psi-Warrior`,
          Assignments: ['Psi-Warrior'],
          MinEDU: 0,
          Trainings: {
            1: this._trainingService.telepathyTraining,
            2: this._trainingService.awarenessTraining,
            3: this._trainingService.teleportationTraining,
            4: this._trainingService.gunCombatTraining,
            5: this._trainingService.vaccSuitTraining,
            6: this._trainingService.reconTraining
          }
        } as TrainingTable
      ],
      RankTables: [
        {
          Name: `Wild Talent`,
          Assignments: [`Wild Talent`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Survivor`,
              Bonus: `${this._skillService.SkillNames.Survival} 1 or ${this._skillService.SkillNames.Streetwise} 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Witch`,
              Bonus: `${this._skillService.SkillNames.Deception}`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Adept`,
          Assignments: [`Adept`],
          Ranks: {
            0: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `Initiate`,
              Bonus: `${this._skillService.SkillNames.Science} (${this._skillService.SkillNames.SciencePsionicology}) 1`,
            } as Rank,
            2: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            3: {
              Name: `Acolyte`,
              Bonus: `Any Talent skill 1`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            6: {
              Name: `Master`,
              Bonus: `Any Talent skill 1`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: `Psi-Warrior`,
          Assignments: [`Psi-Warrior`],
          Ranks: {
            0: {
              Name: `Marine`,
              Bonus: `-`,
            } as Rank,
            1: {
              Name: `-`,
              Bonus: `${this._skillService.SkillNames.GunCombat} 1`,
            } as Rank,
            2: {
              Name: `Captain`,
              Bonus: `${this._skillService.SkillNames.Leadership} 1`,
            } as Rank,
            3: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            4: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
            5: {
              Name: `Force Commander`,
              Bonus: `${this._skillService.SkillNames.Tactics} 1`,
            } as Rank,
            6: {
              Name: `-`,
              Bonus: `-`,
            } as Rank,
          }
        } as RanksTable,
        {
          Name: ``,
          Assignments: [``],
          Ranks: {
            0: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            1: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            2: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            3: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            4: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            5: {
              Name: ``,
              Bonus: ``,
            } as Rank,
            6: {
              Name: ``,
              Bonus: ``,
            } as Rank,
          }
        } as RanksTable
      ],
      Mishaps: {
        1: {
          Description: `Severely injured (this is the same as a result of 2 on the Injury Table). Alternatively, roll twice on the Injury Table and take the lower result.`,
        } as Mishap,
        2: {
          Description: `You telepathically contact something dangerous. Lose one PSI. You also suffer from persistent and terrifying nightmares.`,
        } as Mishap,
        3: {
          Description: `An anti-psi cult or gang attempts to expose or attack you. Roll 1D:
          <br>1-2. Injured; roll on the Injury Table.
          <br>3-4. lose one SOC.
          <br>5-6. Nothing else happens, but you still must leave this career.`,
        } as Mishap,
        4: {
          Description: `You are asked to use your psionic powers in an unethical fashion. Accept, and you may continue
          in this career, but you gain an Enemy. Refuse, and you must leave the career.`,
        } as Mishap,
        5: {
          Description: `You are experimented on by a corporation, government, or other organization. You escape, but
          are forced to leave this career.`,
        } as Mishap,
        6: {
          Description: `Your gift causes a former ally to turn on you and betray you. One Ally or Contact becomes an Enemy.`,
        } as Mishap,
      },
      Events: {
        2: {
          Description: `Disaster! Roll on the Mishap Table, but you are not ejected from this career.`,
        } as CareerEvent,
        3: {
          Description: `Your psionic abilities make you uncomfortable to be around. One Contact or Ally becomes a Rival.`,
        } as CareerEvent,
        4: {
          Description: `Choose one of these skills, reflecting your time spent mastering mind and body. Gain one of
          ${this._skillService.SkillNames.Athletics} 1, ${this._skillService.SkillNames.Stealth} 1,
          ${this._skillService.SkillNames.Survival} 1, or ${this._skillService.SkillNames.Art} 1.`,
        } as CareerEvent,
        5: {
          Description: `You have a chance to use your powers unethically to better your standing.  If you accept, roll
          PSI 8+. If you succeed, gain an extra Benefit roll or +1 SOC. If you fail, lose one SOC.`,
        } as CareerEvent,
        6: {
          Description: `You make an unexpected connection outside your normal circles. Gain a Contact.`,
        } as CareerEvent,
        7: {
          Description: `Life Event. Roll on the Life Events Table.`,
        } as CareerEvent,
        8: {
          Description: `You achieve a new level of psionic strength. Increase your PSI by +1.`,
        } as CareerEvent,
        9: {
          Description: `You are given advanced training in a specialist field. Roll EDU 8+ to gain any one skill except
          ${this._skillService.SkillNames.JackOfAllTrades}.`,
        } as CareerEvent,
        10: {
          Description: `You pick up potentially useful information using your psychic powers. Gain DM+1 to any one Benefit roll.`,
        } as CareerEvent,
        11: {
          Description: `You gain a mentor. Gain an Ally and DM+4 to your next Advancement roll (in any career) thanks to his aid.`,
        } as CareerEvent,
        12: {
          Description: `You achieve a new level of discipline in your powers. You are automatically promoted.`,
        } as CareerEvent,
      }
    } as Career,
  }
  careerNames: string[] = [
    "Agent",
    "Army",
    "Citizen",
    "Drifter",
    "Entertainer",
    "Marine",
    "Merchant",
    "Navy",
    "Noble",
    "Rogue",
    "Scholar",
    "Scout",
    "Prisoner",
    "Psion"
  ];

  constructor(private _skillService: SkillService,
              private _trainingService: TrainingService) {
  }

  getCareer(careerName: string) {
    return this.careers[careerName];
  }
}
