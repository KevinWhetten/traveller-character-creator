import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {RollingService} from "../../../../../services/rolling.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-military-academy-graduation',
  templateUrl: './military-academy-graduation.component.html',
  styleUrls: ['./military-academy-graduation.component.css']
})
export class MilitaryAcademyGraduationComponent {
  @Output() graduation = new EventEmitter();
  graduationRoll: number;
  success: boolean = false;
  honors: boolean = false;
  failure: boolean = false;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _loggingService: LoggingService) {
  }

  submitGraduation(roll: number) {
    this.graduationRoll = roll;
    if (this.graduationRoll >= 11) {
      this.getHonorsBonus();
    } else if (this.graduationRoll >= 7) {
      this.getGraduationBonus();
    } else {
      this._loggingService.addLog('I didn\'t graduate from Military Academy.');
      this.failure = true;
    }
  }

  private getGraduationBonus() {
    this._characterService.increaseEducation(1);
    this._loggingService.addLog('I graduated from Military Academy');
    this._characterMetadataService.graduateMilitaryAcademy();
    this.success = true;
  }

  private getHonorsBonus() {
    this._characterService.increaseEducation(1);
    this._characterService.increaseSocialStatus(1);
    this._loggingService.addLog('I graduated from Military Academy with Honors!')
    this._characterMetadataService.graduateMilitaryAcademyWithHonors();
    this.honors = true;
  }

  moveOn() {
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }

  getAcademy() {
    return this._characterMetadataService.getMilitaryAcademy();
  }
}
