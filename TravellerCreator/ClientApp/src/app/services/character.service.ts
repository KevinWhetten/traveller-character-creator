import {Injectable} from '@angular/core';
import {Character} from "../models/Character";
import {Characteristics} from "../models/characteristics";
import {CharacterSkill} from "../models/character-skill";
import {SkillService} from "./data-services/skill.service";
import {BaseSkill, Subskill} from "../models/skill";
import {LoggingService} from "./metadata-services/logging.service";
import {CharacterMetadataService} from "./metadata-services/character-metadata.service";
import {Armor} from "../models/armor";
import {Weapon} from "../models/weapon";
import {Equipment} from "../models/equipment";

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  private character = new Character();
  private log: string;

  constructor(private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService,
              private _skillService: SkillService) {
    this.loadCharacter();
    if (!this.character.Skills)
      localStorage.setItem('character', JSON.stringify(new Character()));
  }

  // CHARACTER
  getCharacter() {
    this.loadCharacter();
    return this.character;
  }

  // NAME
  getName() {
    this.loadCharacter();
    return this.character.Name;
  }

  setName(name: string) {
    this.loadCharacter();
    this.character.Name = name;
    this._loggingService.addLog(`Name set to: ${name}`)
    this.saveCharacter();
  }

  // AGE
  getAge() {
    this.loadCharacter();
    return this.character.Age;
  }

  // RADS
  getRads() {
    this.loadCharacter();
    return this.character.Rads;
  }

  //region SPECIES
  getSpecies() {
    this.loadCharacter();
    return this.character.Species;
  }

  getSpeciesTraits() {
    return this.character.SpeciesTraits;
  }

  setSpecies(species: string) {
    this.loadCharacter();
    this.character.Species = species;
    this._loggingService.addLog(`Species set to: ${species}`)
    this.character.SpeciesTraits = '';
    this._loggingService.addLog(`Species Traits set to: ${this.character.SpeciesTraits}`)
    this.saveCharacter();
  }

  //endregion

  //region HOMEWORLD
  getHomeworld() {
    this.loadCharacter();
    return this.character.Homeworld;
  }

  setHomeworld(homeworld: string) {
    this.loadCharacter();
    this.character.Homeworld = homeworld;
    this._loggingService.addLog(`Homeworld set to: ${homeworld}`)
    this.saveCharacter();
  }

  //endregion

  //region CHARACTERISTICS
  getCharacteristics() {
    this.loadCharacter();
    return this.character.Characteristics;
  }

  setCharacteristics(set: Characteristics) {
    this.loadCharacter();
    this.character.Characteristics = set;
    this._loggingService.addLog(`Characteristics set to: [STR ${set.Strength.max}], [DEX ${set.Dexterity.max}], [END ${set.Endurance.max}], [INT ${set.Intellect.max}], [EDU ${set.Education.max}], [SOC ${set.SocialStanding.max}]`)
    this.saveCharacter();
  }

  setPsi(psi: number) {
    this.loadCharacter();
    this.character.Characteristics.Psi.max = psi;
    this.character.Characteristics.Psi.current = psi;
    this._loggingService.addLog(`PSI set to [PSI ${psi}].`);
    this.saveCharacter();
  }

  getStrength() {
    this.loadCharacter();
    return this.character.Characteristics.Strength;
  }

  getDexterity() {
    this.loadCharacter();
    return this.character.Characteristics.Dexterity;
  }

  getEndurance() {
    this.loadCharacter();
    return this.character.Characteristics.Endurance;
  }

  getIntellect() {
    this.loadCharacter();
    return this.character.Characteristics.Intellect;
  }

  getEducation() {
    this.loadCharacter();
    return this.character.Characteristics.Education;
  }

  getSocialStanding() {
    this.loadCharacter();
    return this.character.Characteristics.SocialStanding;
  }

  getPsi() {
    this.loadCharacter();
    return this.character.Characteristics.Psi;
  }

  increaseStrength(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Strength.max += number;
    this.character.Characteristics.Strength.current += number;
    this._loggingService.addLog(`Strength increased by ${number} to [STR ${this.character.Characteristics.Strength.max}]`);
    this.saveCharacter();
  }

  injureStrength(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Strength.current -= number;
    this._loggingService.addLog(`Strength reduced by ${number} to [STR ${this.character.Characteristics.Strength.current}]`);
    this.saveCharacter();
  }

  increaseDexterity(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Dexterity.current += number;
    this.character.Characteristics.Dexterity.max += number;
    this._loggingService.addLog(`Dexterity increased by ${number} to [DEX ${this.character.Characteristics.Dexterity.max}]`);
    this.saveCharacter();
  }

  injureDexterity(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Dexterity.current -= number;
    this._loggingService.addLog(`Dexterity reduced by ${number} to [DEX ${this.character.Characteristics.Dexterity.current}]`);
    this.saveCharacter();
  }

  increaseEndurance(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Endurance.current += number;
    this.character.Characteristics.Endurance.max += number;
    this._loggingService.addLog(`Endurance increased by ${number} to [END ${this.character.Characteristics.Endurance.max}]`);
    this.saveCharacter();
  }

  injureEndurance(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Endurance.current -= number;
    this._loggingService.addLog(`Endurance reduced by ${number} to [END ${this.character.Characteristics.Endurance.current}]`);
    this.saveCharacter();
  }

  increaseIntellect(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Intellect.current += number;
    this.character.Characteristics.Intellect.max += number;
    this._loggingService.addLog(`Intellect increased by ${number} to [INT ${this.character.Characteristics.Intellect.max}]`);
    this.saveCharacter();
  }

  increaseEducation(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Education.current += number;
    this.character.Characteristics.Education.max += number;
    this._loggingService.addLog(`Education increased by ${number} to [EDU ${this.character.Characteristics.Education.max}]`);
    this.saveCharacter();
  }

  increaseSocialStatus(number: number) {
    this.loadCharacter();
    this.character.Characteristics.SocialStanding.current += number;
    this.character.Characteristics.SocialStanding.max += number;
    this._loggingService.addLog(`Social Status increased by ${number} to [SOC ${this.character.Characteristics.SocialStanding.max}]`);
    this.saveCharacter();
  }

  increasePsi(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Psi.current += number;
    this.character.Characteristics.Psi.max += number;
    this._loggingService.addLog(`Social Status increased by ${number} to [SOC ${this.character.Characteristics.Psi.max}]`);
    this.saveCharacter();
  }

  //endregion

  //region SKILLS
  getSkills() {
    this.loadCharacter();
    return this.character.Skills;
  }

  getSkillNames() {
    this.loadCharacter();
    let skillNames = [] as string[];

    for (let skill of this._skillService.skills) {
      if (this.character.Skills[skill.Name] != undefined) {
        skillNames.push(skill.Name);
      }
    }

    return skillNames;
  }

  private addSkill(characterSkill: CharacterSkill) {
    this.loadCharacter();
    let skill = this._skillService.getSkill(characterSkill.Name);

    if (!(this.character.Skills[skill.Name] && this.character.Skills[skill.Name] >= characterSkill.Value)) {
      if ((skill as BaseSkill).Subskills) {
        let baseSkill = skill as BaseSkill;

        for (let subskill of baseSkill.Subskills) {
          if (!(this.character.Skills[subskill] && this.character.Skills[subskill] >= characterSkill.Value)) {
            this.character.Skills[subskill] = characterSkill.Value;
          }
        }
      } else if ((skill as Subskill).ParentSkill) {
        this.addSkill({Name: (skill as Subskill).ParentSkill, Value: 0} as CharacterSkill);
        this.loadCharacter();
      }
      this.character.Skills[skill.Name] = characterSkill.Value;

    }
    this._metadataService.addSkillThisTerm(characterSkill.Name);
    this.saveCharacter();
  }

  addSkills(characterSkills: CharacterSkill[]) {
    this.loadCharacter();
    let log = 'Learned skill(s): '
    for (let skill of characterSkills) {
      this.addSkill(skill);
      log += `[${skill.Name} ${skill.Value}]  `;
    }
    this._loggingService.addLog(log);
    this.saveCharacter();
  }

  private increaseSkill(skill: CharacterSkill) {
    this.loadCharacter();
    if (this.character.Skills[skill.Name] != undefined) {
      this.character.Skills[skill.Name] += skill.Value;
    } else {
      this.character.Skills[skill.Name] = skill.Value;
    }
    this.saveCharacter();
    this._metadataService.addSkillThisTerm(skill.Name);
    this.log += `[${skill.Name} ${this.character.Skills[skill.Name]}] `;
  }

  increaseSkills(characterSkills: CharacterSkill[]) {
    this.loadCharacter();
    this.log = 'Increased skill(s) to: '
    for (let characterSkill of characterSkills) {
      this.increaseSkill(characterSkill);
    }
    this._loggingService.addLog(this.log);
    this.saveCharacter();
  }

  //endregion

  //region CONNECTIONS
  getAllies() {
    this.loadCharacter();
    return this.character.Connections.Allies;
  }

  addAlly(description: string) {
    this.loadCharacter();
    this.character.Connections.Allies.push(description);
    this._loggingService.addLog(`Gained a new [Ally]: ${description}`)
    this.saveCharacter();
  }

  getContacts() {
    this.loadCharacter();
    return this.character.Connections.Contacts;
  }

  addContact(description: string) {
    this.loadCharacter();
    this.character.Connections.Contacts.push(description);
    this._loggingService.addLog(`Gained a new [Contact]: ${description}`)
    this.saveCharacter();
  }

  getRivals() {
    this.loadCharacter();
    return this.character.Connections.Rivals;
  }

  addRival(description: string) {
    this.loadCharacter();
    this.character.Connections.Rivals.push(description);
    this._loggingService.addLog(`Gained a new [Rival]: ${description}`)
    this.saveCharacter();
  }

  getEnemies() {
    this.loadCharacter();
    return this.character.Connections.Enemies;
  }

  addEnemy(description: string) {
    this.loadCharacter();
    this.character.Connections.Enemies.push(description);
    this._loggingService.addLog(`Gained a new [Enemy]: ${description}`);
    this.saveCharacter();
  }

  convertAllyToRival(contact: string) {
    this.character.Connections.Allies.splice(this.character.Connections.Allies.indexOf(contact), 1);
    this.character.Connections.Rivals.push(contact);
    this._loggingService.addLog(`Ally: [${contact}] became a Rival.`);
    this.saveCharacter();
  }

  convertAllyToEnemy(contact: string) {
    this.character.Connections.Allies.splice(this.character.Connections.Allies.indexOf(contact), 1);
    this.character.Connections.Enemies.push(contact);
    this._loggingService.addLog(`Ally: [${contact}] became an Enemy.`);
    this.saveCharacter();

  }

  convertContactToRival(contact: string) {
    this.character.Connections.Contacts.splice(this.character.Connections.Contacts.indexOf(contact), 1);
    this.character.Connections.Rivals.push(contact);
    this._loggingService.addLog(`Contact: [${contact}] became a Rival.`);
    this.saveCharacter();
  }

  convertContactToEnemy(contact: string) {
    this.character.Connections.Contacts.splice(this.character.Connections.Contacts.indexOf(contact), 1);
    this.character.Connections.Enemies.push(contact);
    this._loggingService.addLog(`Contact: [${contact}] became an Enemy.`);
    this.saveCharacter();
  }

  //endregion

  //region EQUIPMENT
  getWeapons() {
    this.loadCharacter();
    return this.character.Weapons;
  }

  addWeapon() {
    this.loadCharacter();
    if (this.character.Weapons) {
      this.character.Weapons.push({Name: 'Unknown'} as Weapon)
    } else {
      this.character.Weapons = [{Name: 'Unknown'} as Weapon];
    }
    this._loggingService.addLog(`Got a weapon!`);
    this.saveCharacter();
  }

  addArmour() {
    this.loadCharacter();
    if (this.character.Armor) {
      this.character.Armor.push({Type: 'Unknown'} as Armor);
    } else {
      this.character.Armor = [{Type: 'Unknown'} as Armor];
    }
    this._loggingService.addLog(`Got some Armour!`);
    this.saveCharacter();
  }

  getArmor() {
    this.loadCharacter();
    return this.character.Armor;
  }

  getAugments() {
    this.loadCharacter();
    return this.character.Augments;
  }

  addEquipment(name: string) {
    this.loadCharacter();
    if (this.character.Equipment) {
      this.character.Equipment.push({Name: name} as Equipment);
    } else {
      this.character.Equipment = [{Name: name} as Equipment];
    }
    this._loggingService.addLog(`Got Equipment: ${name}`);
    this.saveCharacter();
  }

  addTASMembership() {
    this.loadCharacter();
    if (this.character.Equipment) {
      this.character.Equipment.push({Name: 'TAS Membership'} as Equipment)
    } else {
      this.character.Equipment = [{Name: 'TAS Membership'} as Equipment];
    }
    this._loggingService.addLog(`Got a TAS Membership!`);
    this.saveCharacter();
  }

  //endregion

  //region FINANCES
  getPension() {
    this.loadCharacter();
    return this.character.Finances.Pension;
  }

  getDebt() {
    this.loadCharacter();
    return this.character.Finances.Debt;
  }

  getCash() {
    this.loadCharacter();
    return this.character.Finances.Cash;
  }

  getMonthlyShipPayments() {
    this.loadCharacter();
    return this.character.Finances.MonthlyShipPayment;
  }

  getLivingCost() {
    this.loadCharacter();
    return this.character.Finances.LivingCost;
  }

  addCash(cash: number) {
    this.loadCharacter();
    if (this.character.Finances.Cash) {
      this.character.Finances.Cash += cash;
    } else {
      this.character.Finances.Cash = cash;
    }
    this._loggingService.addLog(`Earned Cr${cash}!`);
    this.saveCharacter();
  }

  //endregion

  //region START NEW TERM
  startNewTerm() {
    this.loadCharacter();
    this.character.Age += 4;
    this._metadataService.startNewTerm();
    this._loggingService.addLog(`------------ TERM ${(this.character.Age - 14) / 4} - AGE ${this.character.Age} ------------`);
    this.saveCharacter();
  }

  //endregion

  // SAVE
  saveCharacter() {
    localStorage.setItem('character', JSON.stringify(this.character));
  }

  // LOAD
  private loadCharacter() {
    let json = localStorage.getItem('character') || '{}';
    this.character = JSON.parse(json);
    if (!this.character.Skills) {
      this.character = new Character();
    }
  }
}
