import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {SkillService} from "../../../services/data-services/skill.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CharacterSkill} from "../../../models/character-skill";
import {PageService} from "../../../services/page.service";

@Component({
  selector: 'app-military-academy',
  templateUrl: './military-academy.component.html',
  styleUrls: ['./military-academy.component.css']
})
export class MilitaryAcademyComponent implements OnInit {
  applied: boolean = false;
  armyAcademy: boolean = false;
  marinesAcademy: boolean = false;
  navyAcademy: boolean = false;
  armySkills = [this._skillService.SkillNames.Athletics, this._skillService.SkillNames.Drive, this._skillService.SkillNames.GunCombat, this._skillService.SkillNames.HeavyWeapons,
    this._skillService.SkillNames.Melee, this._skillService.SkillNames.Recon, this._skillService.SkillNames.VaccSuit];
  marinesSkills = [this._skillService.SkillNames.Athletics, this._skillService.SkillNames.GunCombat, this._skillService.SkillNames.HeavyWeapons, this._skillService.SkillNames.Tactics,
    this._skillService.SkillNames.Stealth, this._skillService.SkillNames.VaccSuit];
  navySkills = [this._skillService.SkillNames.Athletics, this._skillService.SkillNames.Gunner, this._skillService.SkillNames.GunCombat, this._skillService.SkillNames.Mechanic,
    this._skillService.SkillNames.Pilot, this._skillService.SkillNames.VaccSuit];

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _pageService: PageService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  army(passed: boolean) {
    this._pageService.disableNav();
    this.applied = true;
    if (passed) {
      this.armyAcademy = true;
      this._characterMetadataService.setMilitaryAcademy('Army');
      let skills = [];
      for (let skill of this.armySkills) {
        skills.push({Name: skill, Value: 0} as CharacterSkill);
      }
      this._characterService.addSkills(skills);
    }
  }

  marines(passed: boolean) {
    this._pageService.disableNav();
    this.applied = true;
    if (passed) {
      this.marinesAcademy = true;
      this._characterMetadataService.setMilitaryAcademy('Marines');
      let skills = [];
      for (let skill of this.marinesSkills) {
        skills.push({Name: skill, Value: 0} as CharacterSkill);
      }
      this._characterService.addSkills(skills);
    }
  }

  navy(passed: boolean) {
    this._pageService.disableNav();
    this.applied = true;
    if (passed) {
      this.navyAcademy = true;
      this._characterMetadataService.setMilitaryAcademy('Navy');
      let skills = [];
      for (let skill of this.navySkills) {
        skills.push({Name: skill, Value: 0} as CharacterSkill);
      }
      this._characterService.addSkills(skills);
    }
  }

  moveOn() {
    this._pageService.enableNav();
    if (!this.armyAcademy && !this.marinesAcademy && !this.navyAcademy) {
      this._characterMetadataService.setCurrentUrl('character-creator/careers');
    }
    this._characterMetadataService.setCurrentUrl('character-creator/education/military-academy/event');
  }
}
