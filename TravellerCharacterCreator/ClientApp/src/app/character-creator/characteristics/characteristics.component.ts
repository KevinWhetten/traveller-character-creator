import {Component} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {Router} from "@angular/router";
import {DmService} from "../../services/dm.service";
import {Characteristics} from "../../models/characteristics";

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

  constructor(private _router: Router,
              private _characterService: CharacterService,
              private _dmsService: DmService) {
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
    this._characterService.updateCharacteristics({
      strength: this.strScore,
      dexterity: this.dexScore,
      endurance: this.endScore,
      intellect: this.intScore,
      education: this.eduScore,
      socialStatus: this.socScore} as Characteristics);
    this._characterService.updateCurrentUrl('character-creator/background-skills');
    this._characterService.addLog(`Characteristics set [Str: ${this.strScore}, Dex: ${this.dexScore}, End: ${this.endScore}, Int: ${this.intScore}, Edu: ${this.eduScore}, Soc: ${this.socScore}]`);
    this._router.navigate(['character-creator/background-skills']);
  }
}
