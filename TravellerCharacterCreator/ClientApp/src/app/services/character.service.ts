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
    this._loggingService.addLog(`Characteristics set to: [STR ${set.Strength}], [DEX ${set.Dexterity}], [END ${set.Endurance}], [INT ${set.Intellect}], [EDU ${set.Education}], [SOC ${set.SocialStatus}]`)
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

  getSocialStatus() {
    this.loadCharacter();
    return this.character.Characteristics.SocialStatus;
  }

  increaseStrength(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Strength += number;
    this._loggingService.addLog(`Strength increased to [STR ${this.character.Characteristics.Strength}]`);
    this.saveCharacter();
  }

  increaseDexterity(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Dexterity += number;
    this._loggingService.addLog(`Dexterity increased to [DEX ${this.character.Characteristics.Dexterity}]`);
    this.saveCharacter();
  }

  increaseEndurance(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Endurance += number;
    this._loggingService.addLog(`Endurance increased to [END ${this.character.Characteristics.Endurance}]`);
    this.saveCharacter();
  }

  increaseIntellect(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Intellect += number;
    this._loggingService.addLog(`Intellect increased to [INT ${this.character.Characteristics.Intellect}]`);
    this.saveCharacter();
  }

  increaseEducation(number: number) {
    this.loadCharacter();
    this.character.Characteristics.Education += number;
    this._loggingService.addLog(`Education increased to [EDU ${this.character.Characteristics.Education}]`);
    this.saveCharacter();
  }

  increaseSocialStatus(number: number) {
    this.loadCharacter();
    this.character.Characteristics.SocialStatus += number;
    this._loggingService.addLog(`Social Status increased to [SOC ${this.character.Characteristics.SocialStatus}]`);
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
  addAlly(description: string) {
    this.loadCharacter();
    this.character.Connections.Allies.push(description);
    this._loggingService.addLog(`Gained a new [Ally]: ${description}`)
    this.saveCharacter();
  }

  addContact(description: string) {
    this.loadCharacter();
    this.character.Connections.Contacts.push(description);
    this._loggingService.addLog(`Gained a new [Contact]: ${description}`)
    this.saveCharacter();
  }

  addRival(description: string) {
    this.loadCharacter();
    this.character.Connections.Rivals.push(description);
    this._loggingService.addLog(`Gained a new [Rival]: ${description}`)
    this.saveCharacter();
  }

  addEnemy(description: string) {
    this.loadCharacter();
    this.character.Connections.Enemies.push(description);
    this._loggingService.addLog(`Gained a new [Enemy]: ${description}`)
    this.saveCharacter();
  }
  //endregion

  //region EQUIPMENT
  addWeapon() {
    this.loadCharacter();
    if(this.character.Weapons) {
      this.character.Weapons.push({Name: 'Unknown'} as Weapon)
    } else {
      this.character.Weapons = [{Name: 'Unknown'} as Weapon];
    }
    this.saveCharacter();
  }

  addArmour() {
    this.loadCharacter();
    if(this.character.Armor) {
      this.character.Armor.push({Type: 'Unknown'} as Armor);
    } else {
      this.character.Armor = [{Type: 'Unknown'} as Armor];
    }
    this.saveCharacter();
  }

  addTASMembership() {
    this.loadCharacter();
    if(this.character.Equipment) {
      this.character.Equipment.push({Name: 'TAS Membership'} as Equipment)
    } else {
      this.character.Equipment = [{Name: 'TAS Membership'} as Equipment];
    }
    this.saveCharacter();
  }

  //endregion

  //region FINANCES
  addCash(cash: number) {
    this.loadCharacter();
    if(this.character.Finances.Cash) {
      this.character.Finances.Cash += cash;
    } else {
      this.character.Finances.Cash = cash;
    }
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
