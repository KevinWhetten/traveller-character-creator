import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../../../../services/rolling.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {SkillService} from "../../../../services/data-services/skill.service";

@Component({
  selector: 'app-event-unusual-event',
  templateUrl: './event-unusual-event.component.html',
  styleUrls: ['./event-unusual-event.component.css']
})
export class EventUnusualEventComponent {
  @Output() eventComplete = new EventEmitter;
  eventResult: number = 1;
  submitted: boolean = false;
  story: string;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _loggingService: LoggingService,
              private _skillService: SkillService) {
  }

  submit() {
    this._loggingService.addLog('----- Unusual Event -----');
    this._characterMetadataService.setEventNumber(this.eventResult);
    this.eventComplete.emit();
  }

  submitRoll() {
    this.submitted = true;
  }
}
