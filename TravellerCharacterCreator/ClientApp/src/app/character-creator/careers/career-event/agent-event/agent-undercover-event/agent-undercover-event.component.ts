import { Component, OnInit } from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";

@Component({
  selector: 'app-agent-undercover-event',
  templateUrl: './agent-undercover-event.component.html',
  styleUrls: ['./agent-undercover-event.component.css']
})
export class AgentUndercoverEventComponent implements OnInit {
  eventRoll: number;
  rolled: boolean = false;
  success: boolean = false;

  constructor(private _characterService: CharacterService) { }

  ngOnInit(): void {
  }

  submit() {
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
