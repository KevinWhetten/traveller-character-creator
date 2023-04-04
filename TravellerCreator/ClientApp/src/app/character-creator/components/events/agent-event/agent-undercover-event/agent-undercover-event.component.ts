import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {RollingService} from "../../../../../services/rolling.service";
import {CareerService} from "../../../../services/data-services/career.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-undercover-event',
  templateUrl: './agent-undercover-event.component.html',
  styleUrls: ['./agent-undercover-event.component.scss']
})
export class AgentUndercoverEventComponent {
  @Output() eventComplete = new EventEmitter;
  rolled: boolean = false;
  success: boolean = false;
  selectedCareer: string;
  selected: boolean;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _rollingService: RollingService) { }

  submit(passed: boolean) {
    this.rolled = true;
    if (passed) {
      this._loggingService.addLog('I went undercover to investigate an enemy, and I was successful!');
      this.success = true;
    } else {
      this._loggingService.addLog('I went undercover to investigate an enemy, and something went wrong...');
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
