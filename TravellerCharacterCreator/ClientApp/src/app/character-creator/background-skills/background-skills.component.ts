import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {Router} from "@angular/router";
import {Skill} from "../../models/skill";
import {DmService} from "../../services/dm.service";
import {SkillService} from "../../services/skill.service";

@Component({
  selector: 'app-background-skills',
  templateUrl: './background-skills.component.html',
  styleUrls: ['./background-skills.component.scss']
})
export class BackgroundSkillsComponent implements OnInit {
  skillNum: number;
  selectedSkills: Skill[] = [];
  availableSkillList: Skill[] = [];
  availableSkillNames = ['Admin', 'Animals', 'Art', 'Athletics', 'Carouse',
    'Drive', 'Electronics', 'Flyer', 'Language', 'Mechanic',
    'Medic', 'Profession', 'Science', 'Seafarer', 'Streetwise',
    'Survival', 'Vacc Suit'];
  selectedSkillNum: number = 0;
  private hasError: boolean;

  constructor(private _router: Router,
              private _characterService: CharacterService,
              private _dmsService: DmService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.skillNum = this._dmsService.getDm(this._characterService.getCharacteristics().education) + 3;
    for (let x of this.availableSkillNames) {
      this.availableSkillList.push(this._skillService.getSkill(x));
    }
  }

  change(checkbox: Event, skill: Skill) {
    if (checkbox) {
      this.selectedSkills.push(skill);
      this.selectedSkillNum++;
    } else {
      const index = this.selectedSkills.indexOf(skill, 0);
      if (index > -1) {
        this.selectedSkills.splice(index, 1);
      }
      this.selectedSkillNum--;
    }
  }

  submit() {
    if (this.selectedSkillNum != this.skillNum) {
      this.hasError = true;
    } else {
      this.updateSkills();
      this._characterService.updateCurrentUrl('character-creator/education');
      this._characterService.addLog(this.getLog());
      this._router.navigate(['character-creator/education']);
    }
  }

  private updateSkills() {
    let skillUpdates: { skillName: string, value: number }[] = [];
    for (let x of this.selectedSkills) {
      skillUpdates.push({skillName: x.name, value: 0});
    }
    this._skillService.updateSkills(skillUpdates);
    this._characterService.updateCurrentUrl("character-creator/education/university/graduate")
  }

  private getLog() {
    let log = 'Selected background skills: [';
    for (let x of this.selectedSkills) {
      log += `${x} 0, `
    }
    log = log.substring(0, log.lastIndexOf(', '));
    log += ']';
    return log;
  }
}
