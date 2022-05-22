import {Component, Input, OnInit} from '@angular/core';
import {Characteristic} from "../../models/characteristics";
import {CharacterService} from "../../services/character.service";
import {RollingService} from "../../services/data-services/rolling.service";

@Component({
  selector: 'app-characteristics',
  templateUrl: './characteristics.component.html',
  styleUrls: ['./characteristics.component.css']
})
export class CharacteristicsComponent implements OnInit {

  constructor(private _characterService: CharacterService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
  }

  getStrength() {
    return this._characterService.getCharacteristics().Strength.current;
  }

  getDexterity() {
    return this._characterService.getCharacteristics().Dexterity.current;
  }

  getEndurance() {
    return this._characterService.getCharacteristics().Endurance.current;
  }

  getIntellect() {
    return this._characterService.getCharacteristics().Intellect.current;
  }

  getEducation() {
    return this._characterService.getCharacteristics().Education.current;
  }

  getSocialStanding() {
    return this._characterService.getCharacteristics().SocialStanding.current;
  }

  getPsi() {
    return this._characterService.getCharacteristics().Psi.current;
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
