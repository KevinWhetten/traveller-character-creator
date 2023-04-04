import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {SkillService} from "../../../../services/data-services/skill.service";

@Component({
  selector: 'app-agent-learn-mishap',
  templateUrl: './agent-learn-mishap.component.html',
  styleUrls: ['./agent-learn-mishap.component.scss']
})
export class AgentLearnMishapComponent {
  @Output() mishapComplete = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _skillService: SkillService) { }

  submit() {
    this._characterService.addEnemy('I learned something I shouldn\'t have as an Agent, and now this person wants to kill me.');
    this._characterService.addSkills([{Name: this._skillService.SkillName.Deception, Value: 1}]);
    this.mishapComplete.emit();
  }
}
