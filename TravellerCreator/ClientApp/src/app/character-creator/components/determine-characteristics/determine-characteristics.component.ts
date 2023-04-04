import {Component} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {RollingService} from "../../../services/rolling.service";
import {Characteristics} from "../../models/characteristics";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {AlertType} from "../../../controls/alert/alert.component";

@Component({
  selector: 'app-home',
  templateUrl: './determine-characteristics.component.html',
  styleUrls: ['determine-characteristics.component.scss']
})
export class DetermineCharacteristicsComponent {
  hasWarning: boolean = false;
  warning = AlertType.Warning;

  strDm = -2;
  dexDm = -2;
  endDm = -2;
  intDm = -2;
  eduDm = -2;
  socDm = -2;

  strScore: number = 2;
  dexScore: number = 2;
  endScore: number = 2;
  intScore: number = 2;
  eduScore: number = 2;
  socScore: number = 2;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmsService: RollingService) {
  }

  strChange() {
    this.strDm = this._dmsService.getDm(this.strScore);
  }

  dexChange() {
    this.dexDm = this._dmsService.getDm(this.dexScore);
  }

  endChange() {
    this.endDm = this._dmsService.getDm(this.endScore);
  }

  intChange() {
    this.intDm = this._dmsService.getDm(this.intScore);
  }

  eduChange() {
    this.eduDm = this._dmsService.getDm(this.eduScore);
  }

  socChange() {
    this.socDm = this._dmsService.getDm(this.socScore);
  }

  submit() {
    if (this.hasWarning || this.strDm + this.dexDm + this.endDm + this.intDm + this.eduDm + this.socDm >= 0) {
      this._characterService.setCharacteristics({
        Strength: {max: this.strScore, current: this.strScore},
        Dexterity: {max: this.dexScore, current: this.dexScore},
        Endurance: {max: this.endScore, current: this.endScore},
        Intellect: {max: this.intScore, current: this.intScore},
        Education: {max: this.eduScore, current: this.eduScore},
        SocialStanding: {max: this.socScore, current: this.socScore},
        Psi: {max: -1, current: -1},
      } as Characteristics);
      this._characterMetadataService.setCurrentUrl('character-creator/background-skills');
    } else {
      this.hasWarning = true;
    }
  }

  getDescription(characteristic: string) {
    switch(characteristic) {
      case 'STR':
        return 'A Traveller\'s physical strength, fitness, and forcefulness.';
      case 'DEX':
        return 'Physical coordination and agility; reflexes.';
      case 'END':
        return 'A Traveller\'s stamina, determination, and ability to sustain damage.';
      case 'INT':
        return 'A Traveller\'s intelligence and quickness of mind.';
      case 'EDU':
        return 'A measure of a Traveller\'s learning and experience.';
      case 'SOC':
        return 'A Traveller\'s place in society.';
      default:
        return '';
    }
  }
}
