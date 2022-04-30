import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {Router} from "@angular/router";
import {SkillService} from "../../../../services/skill.service";

@Component({
  selector: 'app-accepted-university',
  templateUrl: './university-skills.component.html',
  styleUrls: ['./university-skills.component.css']
})
export class UniversitySkillsComponent implements OnInit {
  private level1Skill: string = '';
  private level0Skill: string = '';

  constructor(private _router: Router,
              private _characterService: CharacterService,
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
    let selectedSkills = [{skillName: this.level0Skill, value: 0}, {skillName: this.level1Skill, value: 1}];
    this._skillService.updateSkills(selectedSkills);
    this._characterService.addLog(`Learned skills [${this.level0Skill} 0, ${this.level1Skill} 1] at university!`);
    this._router.navigate(['character-creator/education/university/event']);
  }
}
