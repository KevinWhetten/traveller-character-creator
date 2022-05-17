import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {SkillService} from "../../services/data-services/skill.service";
import {AlertType} from "../alert/alert.component";

@Component({
  selector: 'app-skill-checkbox-select',
  templateUrl: './skill-checkbox-select.component.html',
  styleUrls: ['./skill-checkbox-select.component.scss']
})
export class SkillCheckboxSelectComponent implements OnInit {
  @Input() skillNum: number;
  @Input() availableSkillList: string[];
  @Output() choseSkills = new EventEmitter();
  hasError: boolean = false;
  selectedSkillNum: number = 0;
  selectedSkills: string[] = [];
  checked: Record<string, boolean> = {};
  error = AlertType.Error;

  constructor(public _skillService: SkillService) {
    for(let skill in this.availableSkillList){
      this.checked[skill] = false;
    }
  }

  ngOnInit(): void {
  }

  submit() {
    if(this.skillNum == this.selectedSkillNum){
      this.choseSkills.emit(this.selectedSkills);
    } else {
      this.hasError = true;
    }
  }

  change(skill: string) {
    if (!this.checked[skill]) {
      this.checked[skill] = true;
      this.selectedSkills.push(skill);
      this.selectedSkillNum++;
    } else {
      const index = this.selectedSkills.indexOf(skill, 0);
      if (index > -1) {
        this.checked[skill] = false;
        this.selectedSkills.splice(index, 1);
      }
      this.selectedSkillNum--;
    }
  }

  getDescription(skill: string) {
    let description = this._skillService.getDescription(skill);
    return description;
  }

  getMessage() {
    return `Pick the specified number of skills. ${this.skillNum - this.selectedSkillNum} skills remaining.`;
  }
}
