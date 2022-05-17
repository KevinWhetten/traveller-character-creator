import { Component, OnInit } from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";
import {RollingService} from "../../../../../services/data-services/rolling.service";

@Component({
  selector: 'app-agent-undercover-event',
  templateUrl: './agent-undercover-event.component.html',
  styleUrls: ['./agent-undercover-event.component.scss']
})
export class AgentUndercoverEventComponent implements OnInit {
  eventRoll: number;
  rolled: boolean = false;
  success: boolean = false;
  deceptionDm: number = this._rollingService.getDm(this._characterService.getSkills()['Deception']);

  constructor(private _characterService: CharacterService,
              private _rollingService: RollingService) { }

  ngOnInit(): void {
  }

  submitRoll() {
    this.rolled = true;
    let modifier = this._characterService.getSkills()['Deception'];
    if(modifier == undefined){
      modifier = -3;
    }
    if(this.eventRoll + modifier >= 8){
      this.success = true;
    }
  }
}
