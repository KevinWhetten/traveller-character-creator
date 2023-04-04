import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-event-travel',
  templateUrl: './event-travel.component.html',
  styleUrls: ['./event-travel.component.css']
})
export class EventTravelComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I moved to another world.');
  }

  submit() {
    this._metadataService.setQualificationDm(2);
    this.eventComplete.emit();
  }
}
