import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-conspiracy-event',
  templateUrl: './agent-conspiracy-event.component.html',
  styleUrls: ['./agent-conspiracy-event.component.scss']
})
export class AgentConspiracyEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
  }

  submit() {
    this._loggingService.addLog('I uncovered a major conspiracy against my employers.');
    this._metadataService.promote();
    this.eventComplete.emit();
  }
}
