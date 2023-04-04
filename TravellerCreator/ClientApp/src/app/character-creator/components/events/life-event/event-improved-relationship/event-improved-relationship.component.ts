import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-improved-relationship',
  templateUrl: './event-improved-relationship.component.html',
  styleUrls: ['./event-improved-relationship.component.css']
})
export class EventImprovedRelationshipComponent implements OnInit {
  @Output() eventComplete = new EventEmitter();

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('A relationship was improved!');
  }

  submit() {
    this._characterService.addAlly('Deepened romantic relationship.');
    this.eventComplete.emit();
  }
}
