import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-mission-event',
  templateUrl: './agent-mission-event.component.html',
  styleUrls: ['./agent-mission-event.component.scss']
})
export class AgentMissionEventComponent {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService) { }

  proceed() {
    this._loggingService.addLog('I completed a mission for my superiors.');
    this._metadataService.addBenefitBonus(1);
    this.eventComplete.emit();
  }
}
