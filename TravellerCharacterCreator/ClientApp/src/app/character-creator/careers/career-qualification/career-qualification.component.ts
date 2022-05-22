import {Component, OnInit} from '@angular/core';
import {Career} from "../../../models/career";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CharacterService} from "../../../services/character.service";
import {RollingService} from "../../../services/data-services/rolling.service";
import {AlertType} from "../../../controls/alert/alert.component";

@Component({
  selector: 'app-career-qualification',
  templateUrl: './career-qualification.component.html',
  styleUrls: ['./career-qualification.component.scss']
})
export class CareerQualificationComponent implements OnInit {
  career: Career;
  qualificationRoll: number;
  hasError: boolean;
  errorMessage: string;
  error = AlertType.Error;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
    this.career = this._careerService.getCareer(this._metadataService.getCurrentCareer());

    if (this.career.Name == 'Drifter') {
      this._metadataService.setCurrentUrl('character-creator/careers/assignment');
    }
  }

  getModifier() {
    let modifier = -3;

    if (this.career.Qualification.characteristic.includes('STR')) {
      let mod = this._rollingService.getDm(this._characterService.getStrength().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.career.Qualification.characteristic.includes('DEX')) {
      let mod = this._rollingService.getDm(this._characterService.getDexterity().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.career.Qualification.characteristic.includes('END')) {
      let mod = this._rollingService.getDm(this._characterService.getEndurance().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.career.Qualification.characteristic.includes('INT')) {
      let mod = this._rollingService.getDm(this._characterService.getIntellect().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.career.Qualification.characteristic.includes('EDU')) {
      let mod = this._rollingService.getDm(this._characterService.getEducation().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.career.Qualification.characteristic.includes('SOC')) {
      let mod = this._rollingService.getDm(this._characterService.getSocialStanding().current)
      if (mod > modifier) {
        modifier = mod;
      }
    }
    return modifier;
  }

  submit(passed: boolean) {
    if (passed) {
      this._metadataService.setCurrentUrl('character-creator/careers/assignment');
    } else {
      this._metadataService.setCurrentUrl('character-creator/careers/qualification/failed');
    }
  }
}
