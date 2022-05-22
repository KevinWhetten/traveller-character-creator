import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-new-relationship',
  templateUrl: './event-new-relationship.component.html',
  styleUrls: ['./event-new-relationship.component.css']
})
export class EventNewRelationshipComponent implements OnInit {
  @Output() eventComplete = new EventEmitter();

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I began a new romantic relationship.');
  }

  submit() {
    this._characterService.addAlly('New romantic relationship.');
    this.eventComplete.emit();
  }
}
