import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-beyond-event',
  templateUrl: './agent-beyond-event.component.html',
  styleUrls: ['./agent-beyond-event.component.css']
})
export class AgentBeyondEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
  }

  proceed() {
    this._loggingService.addLog('I went above and beyond the call of duty!');
    this._metadataService.setAdvancementBonus(2);
    this.eventComplete.emit();
  }
}
