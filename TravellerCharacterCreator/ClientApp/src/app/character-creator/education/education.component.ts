import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../../services/character.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../services/metadata-services/logging.service";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent implements OnInit {

  constructor(private _characterMetadataService: CharacterMetadataService,
              private _characterService: CharacterService,
              private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  toUniversity() {
    this._loggingService.addLog('Decided to apply to University!');
    this._characterMetadataService.startNewTerm();
    this._characterMetadataService.setCurrentUrl('character-creator/education/university');
  }

  toMilitaryAcademy() {
    this._loggingService.addLog('Decided to apply to a Military Academy!');
    this._characterMetadataService.startNewTerm();
    this._characterMetadataService.setCurrentUrl('character-creator/education/military-academy');
  }

  skip() {
    this._characterMetadataService.startNewTerm();
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }
}
