import {Component} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {Router} from "@angular/router";
import {RollingService} from "../../services/data-services/rolling.service";
import {Characteristics} from "../../models/characteristics";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-home',
  templateUrl: './characteristics.component.html',
  styleUrls: ['characteristics.component.scss']
})
export class CharacteristicsComponent {
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
    this._characterService.setCharacteristics({
      Strength: this.strScore,
      Dexterity: this.dexScore,
      Endurance: this.endScore,
      Intellect: this.intScore,
      Education: this.eduScore,
      SocialStatus: this.socScore,
      Psi: -1
    } as Characteristics);
    this._characterMetadataService.setCurrentUrl('character-creator/background-skills');
  }
}
