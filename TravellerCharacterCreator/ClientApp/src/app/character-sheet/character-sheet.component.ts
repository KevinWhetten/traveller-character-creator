import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../services/character.service";
import {RollingService} from "../services/data-services/rolling.service";
import {SkillService} from "../services/data-services/skill.service";
import {BaseSkill, Skill, Subskill} from "../models/skill";
import {LoggingService} from "../services/metadata-services/logging.service";

@Component({
  selector: 'app-counter-component',
  templateUrl: './character-sheet.component.html',
  styleUrls: ['./character-sheet.component.scss']
})
export class CharacterSheetComponent implements OnInit {
  jackOfAllTrades = this._skillService.SkillNames.JackOfAllTrades;
  strengthDm: number;
  dexterityDm: number;
  enduranceDm: number;
  intellectDm: number;
  educationDm: number;
  socialDm: number;
  psiDm: number;

  constructor(public _characterService: CharacterService,
              public _skillService: SkillService,
              private _dmService: RollingService,
              public _loggingService: LoggingService) {
  }

  ngOnInit(): void {
    let characteristics = this._characterService.getCharacteristics()
    this.strengthDm = this._dmService.getDm(characteristics.Strength.current);
    this.dexterityDm = this._dmService.getDm(characteristics.Dexterity.current);
    this.enduranceDm = this._dmService.getDm(characteristics.Endurance.current);
    this.intellectDm = this._dmService.getDm(characteristics.Intellect.current);
    this.educationDm = this._dmService.getDm(characteristics.Education.current);
    this.socialDm = this._dmService.getDm(characteristics.SocialStanding.current);
    this.psiDm = this._dmService.getDm(characteristics.Psi.current);
  }

  getSubskills(skill: Skill): string[] {
    if ((<BaseSkill>skill).Subskills) {
      return (<BaseSkill>skill).Subskills;
    }
    return [];
  }

  isBaseSkill(skill: Skill) {
    return !((<Subskill>skill).ParentSkill);
  }

  getArmour() {
    return this._characterService.getArmor();
  }

  getPension() {
    return this._characterService.getPension();
  }

  getDebt() {
    return this._characterService.getDebt();
  }

  getCash() {
    return this._characterService.getCash();
  }

  getMonthlyShipPayment() {
    return this._characterService.getMonthlyShipPayments();
  }

  getLivingCost() {
    return this._characterService.getLivingCost();
  }

  getAllies(): string[] {
    return this._characterService.getAllies();
  }

  getContacts() {
    return this._characterService.getContacts();
  }

  getRivals() {
    return this._characterService.getRivals();
  }

  getEnemies() {
    return this._characterService.getEnemies();
  }

  getWeapons() {
    return this._characterService.getWeapons();
  }

  getAugments() {
    return this._characterService.getAugments();
  }

  getJackOfAllTrades() {
    let skill = this._characterService.getSkills()[this._skillService.SkillNames.JackOfAllTrades]
    return skill ? skill : 0;
  }
}
