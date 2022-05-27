import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-network-event',
  templateUrl: './agent-network-event.component.html',
  styleUrls: ['./agent-network-event.component.css']
})
export class AgentNetworkEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  contactsMade: number;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) { }

  ngOnInit(): void {
  }

  submit() {
    this._loggingService.addLog('I established a network of Contacts.');
    for(let i = 0; i < this.contactsMade; i++){
      this._characterService.addContact('Part of my Agent network');
    }
    this.eventComplete.emit();
  }
}
