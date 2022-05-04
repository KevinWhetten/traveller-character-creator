import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../services/character.service";
import {DmService} from "../services/data-services/dm.service";
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

  constructor(public _characterService: CharacterService,
              public _skillService: SkillService,
              private _dmService: DmService,
              public _loggingService: LoggingService) {
  }

  ngOnInit(): void {
    let characteristics =this._characterService.getCharacteristics()
    this.strengthDm = this._dmService.getDm(characteristics.Strength);
    this.dexterityDm = this._dmService.getDm(characteristics.Dexterity);
    this.enduranceDm = this._dmService.getDm(characteristics.Endurance);
    this.intellectDm = this._dmService.getDm(characteristics.Intellect);
    this.educationDm = this._dmService.getDm(characteristics.Education);
    this.socialDm = this._dmService.getDm(characteristics.SocialStatus);
  }

  getSubskills(skill: Skill): string[] {
    if((<BaseSkill>skill).Subskills) {
      return (<BaseSkill>skill).Subskills;
    }
    return [];
  }

  isBaseSkill(skill: Skill) {
    let flag = !((<Subskill>skill).ParentSkill);
    return flag;
  }
}
