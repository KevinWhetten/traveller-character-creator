import {Component, OnInit} from '@angular/core';
import {Character} from "../models/Character";
import {CharacterService} from "../services/character.service";
import {DmService} from "../services/dm.service";

@Component({
  selector: 'app-counter-component',
  templateUrl: './character-sheet.component.html',
  styleUrls: ['./character-sheet.component.scss']
})
export class CharacterSheetComponent implements OnInit {
  character: Character;
  jackOfAllTrades = 'Jack-of-All-Trades';
  strengthDm: number;
  dexterityDm: number;
  enduranceDm: number;
  intellectDm: number;
  educationDm: number;
  socialDm: number;

  constructor(private _characterService: CharacterService,
              private _dmService: DmService) {
  }

  ngOnInit(): void {
    this.character = this._characterService.getCharacter();
    this.strengthDm = this._dmService.getDm(this.character.characteristics.strength);
    this.dexterityDm = this._dmService.getDm(this.character.characteristics.dexterity);
    this.enduranceDm = this._dmService.getDm(this.character.characteristics.endurance);
    this.intellectDm = this._dmService.getDm(this.character.characteristics.intellect);
    this.educationDm = this._dmService.getDm(this.character.characteristics.education);
    this.socialDm = this._dmService.getDm(this.character.characteristics.socialStatus);
  }
}
