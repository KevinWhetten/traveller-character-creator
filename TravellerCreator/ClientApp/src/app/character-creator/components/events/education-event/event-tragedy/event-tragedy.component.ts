import {Component, OnInit} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {PageService} from "../../../../services/page.service";

@Component({
  selector: 'app-event-tragedy',
  templateUrl: './event-tragedy.component.html',
  styleUrls: ['./event-tragedy.component.css']
})
export class EventTragedyComponent implements OnInit {

  constructor(private _characterMetadataService: CharacterMetadataService,
              private _loggingService: LoggingService,
              private _pageService: PageService) {
  }

  ngOnInit(): void {
    if (this._loggingService.getLastLog() != 'My time in education was not a happy one, and I suffered a deep tragedy. I crashed and failed to graduate.') {
      this._loggingService.addLog('My time in education was not a happy one, and I suffered a deep tragedy. I crashed and failed to graduate.');
    }
  }

  skipGraduation() {
    this._pageService.enableNav();
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }
}
