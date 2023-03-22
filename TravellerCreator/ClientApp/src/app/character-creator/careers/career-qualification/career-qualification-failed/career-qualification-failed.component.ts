import { Component} from '@angular/core';
import {CareerService} from "../../../../services/data-services/career.service";
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../../../services/data-services/rolling.service";

@Component({
  selector: 'app-career-qualification-failed',
  templateUrl: './career-qualification-failed.component.html',
  styleUrls: ['./career-qualification-failed.component.css']
})
export class CareerQualificationFailedComponent {

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) { }

  draft() {
    this._metadataService.setCurrentUrl('character-creator/careers/draft');
  }

  drift() {
    this._metadataService.setCurrentCareer('Drifter');
    this._metadataService.setCurrentUrl('character-creator/careers/assignment');
  }

}
