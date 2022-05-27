import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {RollingService} from "../../../../services/data-services/rolling.service";
import {SkillService} from "../../../../services/data-services/skill.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-training-event',
  templateUrl: './agent-training-event.component.html',
  styleUrls: ['./agent-training-event.component.scss']
})
export class AgentTrainingEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  chosenSkill: string;
  rolled: boolean = false;
  success: boolean = false;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _rollingService: RollingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  submit(passed: boolean) {
    this.rolled = true;
    this.success = passed;
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
    this._loggingService.addLog('I received advanced training in a specialist field.');
    this._characterService.increaseSkills([{Name: this.chosenSkill, Value: 1}]);
    this.eventComplete.emit();
  }

  proceed() {
    this._loggingService.addLog('I received advanced training in a specialist field, but apparently I wasn\'t paying attention.');
    this.eventComplete.emit();
  }
}
