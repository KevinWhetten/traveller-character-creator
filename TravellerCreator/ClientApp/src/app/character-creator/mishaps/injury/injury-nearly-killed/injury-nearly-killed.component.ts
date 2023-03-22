import {Component, EventEmitter, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-injury-nearly-killed',
  templateUrl: './injury-nearly-killed.component.html',
  styleUrls: ['./injury-nearly-killed.component.css']
})
export class InjuryNearlyKilledComponent {
  @Output() injured = new EventEmitter;
  characteristic: string;

  constructor(private _characterService: CharacterService) {
  }

  rolled(numberRolled: number) {
    if (this.characteristic) {
      switch (this.characteristic) {
        case 'STR':
          this._characterService.injureStrength(numberRolled);
          this._characterService.injureDexterity(2);
          this._characterService.injureEndurance(2);
          break;
        case 'DEX':
          this._characterService.injureStrength(2);
          this._characterService.injureDexterity(numberRolled);
          this._characterService.injureEndurance(2);
          break;
        case 'END':
          this._characterService.injureStrength(2);
          this._characterService.injureDexterity(2);
          this._characterService.injureEndurance(numberRolled);
          break;
      }
      this.injured.emit();
    }
  }
}
