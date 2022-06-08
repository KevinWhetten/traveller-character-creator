import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CharacterService} from "../../../services/character.service";

@Component({
  selector: 'app-psionic-test',
  templateUrl: './psionic-test.component.html',
  styleUrls: ['./psionic-test.component.css']
})
export class PsionicTestComponent implements OnInit {
  @Output() tested = new EventEmitter;
  @Output() eventComplete = new EventEmitter;
  rolled: boolean = false;
  psi: number = 0;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService) {
  }

  ngOnInit(): void {
  }

  recordPsi(rolledNum: number) {
    this.rolled = true;
    this.psi = rolledNum + this.getModifier();
    this.tested.emit();
  }

  getModifier() {
    return (this._metadataService.getTerm() - 1) * -1;
  }

  completeTesting() {
    if (this.psi > 0) {
      this._characterService.setPsi(this.psi);
    }
    else {
      this._characterService.setPsi(0);
    }
    this.eventComplete.emit();
  }
}
