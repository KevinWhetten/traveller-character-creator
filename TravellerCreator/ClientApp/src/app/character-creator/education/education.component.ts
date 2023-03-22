import {Component} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../services/metadata-services/logging.service";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent {

  constructor(private _characterMetadataService: CharacterMetadataService,
              private _characterService: CharacterService,
              private _loggingService: LoggingService) {
  }

  toUniversity() {
    this._loggingService.addLog('Decided to apply to University!');
    this._characterService.startNewTerm();
    this._characterMetadataService.setCurrentUrl('character-creator/education/university');
  }

  toMilitaryAcademy() {
    this._loggingService.addLog('Decided to apply to a Military Academy!');
    this._characterService.startNewTerm();
    this._characterMetadataService.setCurrentUrl('character-creator/education/military-academy');
  }

  skip() {
    this._characterService.startNewTerm();
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }
}
