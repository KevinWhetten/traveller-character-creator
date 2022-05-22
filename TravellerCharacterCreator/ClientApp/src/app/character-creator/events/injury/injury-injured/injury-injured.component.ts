import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-injury-injured',
  templateUrl: './injury-injured.component.html',
  styleUrls: ['./injury-injured.component.css']
})
export class InjuryInjuredComponent implements OnInit {
  @Output() injured = new EventEmitter;
  characteristic: string;

  constructor(private _characterService: CharacterService) { }

  ngOnInit(): void {
  }

  submit() {
    switch(this.characteristic){
      case 'STR':
        this._characterService.injureStrength(-1);
        break;
      case 'DEX':
        this._characterService.injureDexterity(-1);
        break;
      case 'END':
        this._characterService.injureEndurance(-1);
        break;
    }
    this.injured.emit();
  }
}
