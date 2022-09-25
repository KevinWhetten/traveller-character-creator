import {Component} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {RollingService} from "../../services/data-services/rolling.service";

@Component({
  selector: 'app-characteristics',
  templateUrl: './characteristics.component.html',
  styleUrls: ['./characteristics.component.css']
})
export class CharacteristicsComponent {

  constructor(private _characterService: CharacterService,
              private _rollingService: RollingService) {
  }

  getStrength() {
    return this._characterService.getCharacteristics().Strength.current;
  }

  getMaxStrength() {
    return this._characterService.getCharacteristics().Strength.max;
  }

  getDexterity() {
    return this._characterService.getCharacteristics().Dexterity.current;
  }

  getMaxDexterity() {
    return this._characterService.getCharacteristics().Dexterity.max;
  }

  getEndurance() {
    return this._characterService.getCharacteristics().Endurance.current;
  }

  getMaxEndurance() {
    return this._characterService.getCharacteristics().Endurance.max;
  }

  getIntellect() {
    return this._characterService.getCharacteristics().Intellect.current;
  }

  getMaxIntellect() {
    return this._characterService.getCharacteristics().Intellect.max;
  }

  getEducation() {
    return this._characterService.getCharacteristics().Education.current;
  }

  getMaxEducation() {
    return this._characterService.getCharacteristics().Education.max;
  }

  getSocialStanding() {
    return this._characterService.getCharacteristics().SocialStanding.current;
  }

  getMaxSocialStanding() {
    return this._characterService.getCharacteristics().SocialStanding.max;
  }

  getPsi() {
    return this._characterService.getCharacteristics().Psi.current;
  }

  getMaxPsi() {
    return this._characterService.getCharacteristics().Psi.max;
  }

  getStrengthDm() {
    return this._rollingService.getDm(this.getStrength());
  }

  getDexterityDm() {
    return this._rollingService.getDm(this.getDexterity());
  }

  getEnduranceDm() {
    return this._rollingService.getDm(this.getEndurance());
  }

  getIntellectDm() {
    return this._rollingService.getDm(this.getIntellect());
  }

  getEducationDm() {
    return this._rollingService.getDm(this.getEndurance());
  }

  getSocialStandingDm() {
    return this._rollingService.getDm(this.getSocialStanding());
  }

  getPsiDm() {
    return this._rollingService.getDm(this.getPsi());
  }
}
