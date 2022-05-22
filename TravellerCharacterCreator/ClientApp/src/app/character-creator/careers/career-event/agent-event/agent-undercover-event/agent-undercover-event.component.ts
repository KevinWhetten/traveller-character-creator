import { Component, OnInit } from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";
import {RollingService} from "../../../../../services/data-services/rolling.service";

@Component({
  selector: 'app-agent-undercover-event',
  templateUrl: './agent-undercover-event.component.html',
  styleUrls: ['./agent-undercover-event.component.scss']
})
export class AgentUndercoverEventComponent implements OnInit {
  rolled: boolean = false;
  success: boolean = false;

  constructor(private _characterService: CharacterService,
              private _rollingService: RollingService) { }

  ngOnInit(): void {
  }

  submit(passed: boolean) {
    this.rolled = true;
    if(passed){
      this.success = true;
    }
  }
}
