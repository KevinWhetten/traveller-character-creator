import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {RollingService} from "../../../../services/rolling.service";
import {SkillService} from "../../../services/data-services/skill.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../services/metadata-services/logging.service";
import {PageService} from "../../../services/page.service";

@Component({
  selector: 'app-education-event',
  templateUrl: './education-event.component.html',
  styleUrls: ['./education-event.component.css']
})
export class EducationEventComponent {
  @Output() graduated = new EventEmitter();
  @Output() psionicTest = new EventEmitter();
  @Output() lifeEventOccurs = new EventEmitter();
  eventResult: number = 2;
  submitted: boolean = false;
  story: string;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _loggingService: LoggingService,
              private _pageService: PageService,
              private _skillService: SkillService) {
  }

  submit() {
    this._pageService.disableNav();
    this._loggingService.addLog('----- Education Event -----');
    this.submitted = true;
    this._characterMetadataService.setEventNumber(this.eventResult);
  }

  graduate() {
    this._pageService.enableNav();
    this.graduated.emit();
  }
}
