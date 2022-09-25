import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-injury-scarred',
  templateUrl: './injury-scarred.component.html',
  styleUrls: ['./injury-scarred.component.css']
})
export class InjuryScarredComponent {
  @Output() injured = new EventEmitter;
  characteristic: string;

  constructor(private _characterService: CharacterService) { }

  submit() {
    switch(this.characteristic){
      case 'STR':
        this._characterService.injureStrength(2);
        break;
      case 'DEX':
        this._characterService.injureDexterity(2);
        break;
      case 'END':
        this._characterService.injureEndurance(2);
        break;
    }
    this.injured.emit();
  }
}
