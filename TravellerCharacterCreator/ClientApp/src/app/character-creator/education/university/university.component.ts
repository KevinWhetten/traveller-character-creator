import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../../../services/character.service";
import {Character} from "../../../models/Character";
import {DmService} from "../../../services/dm.service";

@Component({
  selector: 'app-university',
  templateUrl: './university.component.html',
  styleUrls: ['./university.component.css']
})
export class UniversityComponent implements OnInit {
  acceptanceRoll: number = 2;
  private character: Character;
  private universityEntryDifficulty: number = 7;

  constructor(private _router: Router,
              private _characterService: CharacterService,
              private _dmService: DmService) {
  }

  ngOnInit(): void {
    this.character = this._characterService.getCharacter();
    if (this.character.termNumber == 2) {
      this.universityEntryDifficulty++;
    }
    if (this.character.termNumber == 3) {
      this.universityEntryDifficulty += 2;
    }
    if (this.character.characteristics.socialStatus >= 9) {
      this.universityEntryDifficulty--;
    }
  }

  submitEntry() {
    if(this.acceptanceRoll + this._dmService.getDm(this.character.characteristics.education) >= this.universityEntryDifficulty){
      let characteristics = this._characterService.getCharacteristics();
      characteristics.education++;
      this._characterService.updateCharacteristics(characteristics);
      this._characterService.updateCurrentUrl('character-creator/education/university/skills');
      this._characterService.addLog('Accepted to University!');
      this._router.navigate(['character-creator/education/university/skills']);
    }
  }
}
