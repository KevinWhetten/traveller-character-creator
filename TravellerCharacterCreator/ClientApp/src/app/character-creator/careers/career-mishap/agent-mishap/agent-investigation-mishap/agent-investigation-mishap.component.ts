import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";
import {SkillService} from "../../../../../services/data-services/skill.service";

@Component({
  selector: 'app-agent-investigation-mishap',
  templateUrl: './agent-investigation-mishap.component.html',
  styleUrls: ['./agent-investigation-mishap.component.scss']
})
export class AgentInvestigationMishapComponent implements OnInit {
  @Output() mishapComplete = new EventEmitter;
  advocateRoll: number = 0;
  hasError: boolean = false;
  errorMessage: string = '';
  success: boolean = false;

  constructor(private _characterService: CharacterService,
              private _skillService: SkillService) { }

  ngOnInit(): void {
  }

  getModifier() {
    let skill = this._characterService.getSkills()[this._skillService.SkillNames.Advocate];
    let jackOfAllTrades = this._characterService.getSkills()[this._skillService.SkillNames.JackOfAllTrades];
    if(skill){
      return skill;
    } else {
      if(!jackOfAllTrades){
        jackOfAllTrades = 0;
      }
      return -3 + jackOfAllTrades;
    }
  }

  submit() {
    if(this.advocateRoll > 0){
      if(this.advocateRoll == 2) {
        // TODO: Go to prison
      } else if(this.advocateRoll + this.getModifier() >= 8){
        this.success = true;
      } else {
        this.proceed();
      }
    } else {
      this.hasError = true;
      this.errorMessage = 'Roll must be between 2 and 12.';
    }
  }

  proceed() {
    this.mishapComplete.emit();
  }
}
