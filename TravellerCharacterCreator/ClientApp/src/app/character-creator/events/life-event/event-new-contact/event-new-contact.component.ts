import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-new-contact',
  templateUrl: './event-new-contact.component.html',
  styleUrls: ['./event-new-contact.component.css']
})
export class EventNewContactComponent implements OnInit {
  @Output() eventComplete = new EventEmitter();

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I made a new Contact!');
  }

  submit() {
    let term = this._metadataService.getTerm();
    this._characterService.addContact(`Someone I met in term ${term}.`);
    this.eventComplete.emit();
  }
}
