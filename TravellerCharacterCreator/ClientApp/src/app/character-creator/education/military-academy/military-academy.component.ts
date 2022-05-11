import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {Character} from "../../../models/Character";
import {Router} from "@angular/router";
import {RollingService} from "../../../services/data-services/rolling.service";
import {PercentageService} from "../../../services/data-services/percentage.service";
import {SkillService} from "../../../services/data-services/skill.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CharacterSkill} from "../../../models/character-skill";

@Component({
  selector: 'app-military-academy',
  templateUrl: './military-academy.component.html',
  styleUrls: ['./military-academy.component.css']
})
export class MilitaryAcademyComponent implements OnInit {
  applied: boolean = false;
  acceptanceRoll: number;
  armyAcademy: boolean = false;
  marinesAcademy: boolean = false;
  navyAcademy: boolean = false;
  armyChance: number;
  marinesChance: number;
  navyChance: number;
  private endDm: number = 0;
  private intDm: number = 0;
  armySkills = [this._skillService.SkillNames.Athletics, this._skillService.SkillNames.Drive, this._skillService.SkillNames.GunCombat, this._skillService.SkillNames.HeavyWeapons,
    this._skillService.SkillNames.Melee, this._skillService.SkillNames.Recon, this._skillService.SkillNames.VaccSuit];
  marinesSkills = [this._skillService.SkillNames.Athletics, this._skillService.SkillNames.GunCombat, this._skillService.SkillNames.HeavyWeapons, this._skillService.SkillNames.Tactics,
    this._skillService.SkillNames.Stealth, this._skillService.SkillNames.VaccSuit];
  navySkills = [this._skillService.SkillNames.Athletics, this._skillService.SkillNames.Gunner, this._skillService.SkillNames.GunCombat, this._skillService.SkillNames.Mechanic,
    this._skillService.SkillNames.Pilot, this._skillService.SkillNames.VaccSuit];

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _percentageService: PercentageService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this._characterService.startNewTerm();
    this.endDm = this._dmService.getDm(this._characterService.getEndurance()) - (2 * (this._characterMetadataService.getTerm() - 1));
    this.intDm = this._dmService.getDm(this._characterService.getIntellect()) - (2 * (this._characterMetadataService.getTerm() - 1));
    this.armyChance = this._percentageService.get2d6Percentage(8, this.endDm);
    this.marinesChance = this._percentageService.get2d6Percentage(9, this.endDm);
    this.navyChance = this._percentageService.get2d6Percentage(9, this.intDm);
  }

  army() {
    if (this.acceptanceRoll + this.endDm >= 8) {
      this.armyAcademy = true;
      this._characterMetadataService.setMilitaryAcademy('Army');
      let skills = [];
      for (let skill of this.armySkills) {
        skills.push({Name: skill, Value: 0} as CharacterSkill);
      }
      this._characterService.addSkills(skills);
    }
  }

  marines() {
    if (this.acceptanceRoll + this.endDm >= 9) {
      this.marinesAcademy = true;
      this._characterMetadataService.setMilitaryAcademy('Marines');
      let skills = [];
      for (let skill of this.marinesSkills) {
        skills.push({Name: skill, Value: 0} as CharacterSkill);
      }
      this._characterService.addSkills(skills);
    }
  }

  navy() {
    if (this.acceptanceRoll + this.intDm > 9) {
      this.navyAcademy = true;
      this._characterMetadataService.setMilitaryAcademy('Navy');
      let skills = [];
      for (let skill of this.navySkills) {
        skills.push({Name: skill, Value: 0} as CharacterSkill);
      }
      this._characterService.addSkills(skills);
    }
  }

  apply(branch: string) {
    if (branch == 'Army') {
      this.army();
    } else if (branch == 'Marines') {
      this.marines();
    } else if (branch == 'Navy') {
      this.navy();
    }
    this.applied = true;
  }

  moveOn() {
    if (!this.armyAcademy && !this.marinesAcademy && !this.navyAcademy) {
      this._characterMetadataService.setCurrentUrl('character-creator/careers');
    }
    this._characterMetadataService.setCurrentUrl('character-creator/education/military-academy/event');
  }
}
