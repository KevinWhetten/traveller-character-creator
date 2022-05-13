import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";
import {SkillService} from "../../../../../services/data-services/skill.service";

@Component({
  selector: 'app-agent-senior-event',
  templateUrl: './agent-senior-event.component.html',
  styleUrls: ['./agent-senior-event.component.css']
})
export class AgentSeniorEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  choice: string;

  constructor(private _characterService: CharacterService,
              private _skillService: SkillService) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.choice == 'investigate'){
      this._characterService.increaseSkills([{Name: this._skillService.SkillNames.Investigate, Value: 1}]);
      this.eventComplete.emit();
    }
    else if(this.choice == 'advancement'){
      // TODO: Set Advancement
      this.eventComplete.emit();
    }
  }
}
