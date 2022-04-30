import {Injectable} from '@angular/core';
import {Character} from "../models/Character";
import {Characteristics} from "../models/characteristics";
import {Equipment} from "../models/equipment";
import {Finances} from "../models/finances";
import {Connections} from "../models/connections";
import {SkillService} from "./skill.service";

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  private character = {
    name: '',
    age: 18,
    termNumber: 0,
    Rads: 0,
    Species: 'Human',
    SpeciesTraits: 'N/A',
    Homeworld: '',

    characteristics: {
      strength: 0,
      dexterity: 0,
      endurance: 0,
      intellect: 0,
      education: 0,
      socialStatus: 0
    } as Characteristics,

    Skills: this._skillService.getSkillset(),

    Equipment: {} as Equipment,
    Finances: {} as Finances,
    Connections: {
      Allies: [],
      Contacts: [],
      Rivals: [],
      Enemies: []
    } as Connections,
    log: [],
    currentUrl: 'character-creator/characteristics',
    nextCareer: ''
  } as Character;

  constructor(private _skillService: SkillService) {
  }

  getCharacteristics() {
    this.character = JSON.parse(localStorage.getItem('character') || '{}');
    return this.character.characteristics;
  }

  updateCharacteristics(set: Characteristics) {
    this.character.characteristics = set;
    this.saveCharacter();
  }

  getCharacter() {
    this.character = JSON.parse(localStorage.getItem('character') || '{}');
    return this.character;
  }

  updateCharacter(character: Character) {
    this.character = character;
    this.saveCharacter();
  }

  saveCharacter() {
    localStorage.setItem('character', JSON.stringify(this.character));
  }

  updateCurrentUrl(url: string) {
    this.character.currentUrl = url;
    this.saveCharacter();
  }

  getCurrentUrl() {
    return this.character.currentUrl;
  }

  startNewTerm() {
    this.character.termNumber++;

    this._skillService.newTerm();
    this.character.Skills = this._skillService.getSkillset();

    this.updateCharacter(this.character);
  }

  addLog(log: string) {
    this.character.log.push(log);
    this.saveCharacter();
  }
}
