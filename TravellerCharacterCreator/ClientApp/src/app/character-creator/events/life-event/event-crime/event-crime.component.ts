import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-event-crime',
  templateUrl: './event-crime.component.html',
  styleUrls: ['./event-crime.component.css']
})
export class EventCrimeComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this._loggingService.addLog('You were the victim of (or committed) a crime!');
  }

  loseBenefit() {
    this._metadataService.loseBenefits(1);
    this.eventComplete.emit();
  }

  prisoner() {
    this._metadataService.setJailed(true);
    this.eventComplete.emit();
  }
}
