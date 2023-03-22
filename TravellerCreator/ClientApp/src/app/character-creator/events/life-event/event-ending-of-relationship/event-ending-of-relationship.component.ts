import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-ending-of-relationship',
  templateUrl: './event-ending-of-relationship.component.html',
  styleUrls: ['./event-ending-of-relationship.component.css']
})
export class EventEndingOfRelationshipComponent implements OnInit {
  @Output() eventComplete = new EventEmitter();
  connection: string;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('A romantic relationship ended. Badly.');
  }

  submit() {
    if(this.connection == 'rival'){
      this._characterService.addRival('A romantic relationship ended. Badly.');
      this.eventComplete.emit();
    } else if (this.connection == 'enemy'){
      this._characterService.addEnemy('A romantic relationship ended. Badly.');
      this.eventComplete.emit();
    }
  }
}
