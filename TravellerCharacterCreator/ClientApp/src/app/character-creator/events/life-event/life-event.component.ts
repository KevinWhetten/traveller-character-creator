import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../../services/data-services/rolling.service";
import {LoggingService} from "../../../services/metadata-services/logging.service";
import {SkillService} from "../../../services/data-services/skill.service";

@Component({
  selector: 'app-life-event',
  templateUrl: './life-event.component.html',
  styleUrls: ['./life-event.component.css']
})
export class LifeEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  eventResult: number = 2;
  submitted: boolean = false;
  story: string;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _loggingService: LoggingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this._loggingService.addLog('----- Life Event -----');
  }

  submit() {
    this._characterMetadataService.setEventNumber(this.eventResult);
    this.eventComplete.emit();
  }

  submitRoll() {
    this.submitted = true;
  }
}
