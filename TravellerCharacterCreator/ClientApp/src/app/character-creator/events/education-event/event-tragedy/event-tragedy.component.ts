import {Component, OnInit} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-event-tragedy',
  templateUrl: './event-tragedy.component.html',
  styleUrls: ['./event-tragedy.component.css']
})
export class EventTragedyComponent implements OnInit {
  story: string;

  constructor(private _characterMetadataService: CharacterMetadataService,
    private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  skipGraduation() {
    this._loggingService.addLog('My time in education was not a happy one, and I suffered a deep tragedy. I crashed and failed to graduate.');
    if (this.story) {
      this._loggingService.addLog(this.story);
    }
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }
}
