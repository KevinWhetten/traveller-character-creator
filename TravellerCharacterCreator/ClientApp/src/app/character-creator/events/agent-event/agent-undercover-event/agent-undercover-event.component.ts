import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {RollingService} from "../../../../services/data-services/rolling.service";
import {CareerService} from "../../../../services/data-services/career.service";

@Component({
  selector: 'app-agent-undercover-event',
  templateUrl: './agent-undercover-event.component.html',
  styleUrls: ['./agent-undercover-event.component.scss']
})
export class AgentUndercoverEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  rolled: boolean = false;
  success: boolean = false;
  selectedCareer: string;
  selected: boolean;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _rollingService: RollingService) { }

  ngOnInit(): void {
  }

  submit(passed: boolean) {
    this.rolled = true;
    if(passed){
      this.success = true;
    }
  }

  getSelectedCareer() {
    return this._careerService.getCareer(this.selectedCareer);
  }

  selectTable() {
    this.selected = true;
  }

  proceed() {
    this.eventComplete.emit();
  }
}
