import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-injury-severely-injured',
  templateUrl: './injury-severely-injured.component.html',
  styleUrls: ['./injury-severely-injured.component.css']
})
export class InjurySeverelyInjuredComponent {
  @Output() injured = new EventEmitter;
  characteristic: string;
  roll: number;

  constructor(private _characterService: CharacterService) { }

  submit(result: number) {
    this.roll = result;
    switch(this.characteristic){
      case 'STR':
        this._characterService.injureStrength(this.roll);
        break;
      case 'DEX':
        this._characterService.injureDexterity(this.roll);
        break;
      case 'END':
        this._characterService.injureEndurance(this.roll);
        break;
    }
    this.injured.emit();
  }
}
