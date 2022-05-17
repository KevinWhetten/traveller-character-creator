import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";
import {RollingService} from "../../../../../services/data-services/rolling.service";
import {SkillService} from "../../../../../services/data-services/skill.service";

@Component({
  selector: 'app-agent-training-event',
  templateUrl: './agent-training-event.component.html',
  styleUrls: ['./agent-training-event.component.scss']
})
export class AgentTrainingEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  chosenSkill: string;
  eventRoll: number = 2;
  rolled: boolean = false;
  success: boolean = false;
  eduDm: number = this._rollingService.getDm(this._characterService.getEducation());

  constructor(private _characterService: CharacterService,
              private _rollingService: RollingService,
              private _skillService: SkillService) { }

  ngOnInit(): void {
  }

  submitRoll() {
    this.rolled = true;
    let educationScore = this._characterService.getEducation();
    let modifier = this._rollingService.getDm(educationScore);
    if(this.eventRoll + modifier >= 8){
      this.success = true;
    }
  }

  getGroups() {
    return this._skillService.getGroups(this._characterService.getSkillNames());
  }

  getGroupNames() {
    return this._skillService.getGroupNames(this._characterService.getSkillNames());
  }

  changeSkill(skillName: string) {
    this.chosenSkill = skillName;
  }

  submitSkill() {
    this._characterService.increaseSkills([{Name: this.chosenSkill, Value: 1}]);
    this.eventComplete.emit();
  }
}
