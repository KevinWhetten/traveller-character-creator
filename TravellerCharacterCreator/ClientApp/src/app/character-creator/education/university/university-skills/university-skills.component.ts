import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {Router} from "@angular/router";
import {SkillService} from "../../../../services/data-services/skill.service";
import {CharacterSkill} from "../../../../models/character-skill";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-accepted-university',
  templateUrl: './university-skills.component.html',
  styleUrls: ['./university-skills.component.css']
})
export class UniversitySkillsComponent implements OnInit {
  private level1Skill: string = '';
  private level0Skill: string = '';

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  level0SkillChanged(skill: string) {
    this.level0Skill = skill;
  }

  level1SkillChanged(skill: string) {
    this.level1Skill = skill;
  }

  saveSkills() {
    let selectedSkills = [{Name: this.level0Skill, Value: 0}, {Name: this.level1Skill, Value: 1}] as CharacterSkill[];
    this._characterService.addSkills(selectedSkills);
    this._characterMetadataService.setUniversitySkills(selectedSkills);
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/event');
  }
}
