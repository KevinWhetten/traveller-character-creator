import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AlertType} from "../alert/alert.component";
import {CharacterService} from "../../services/character.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../services/data-services/rolling.service";

@Component({
  selector: 'app-roll',
  templateUrl: './roll.component.html',
  styleUrls: ['./roll.component.scss']
})
export class RollComponent implements OnInit {
  @Input() modifier: number = 0;
  @Input() numberOfDice: number = 2;
  @Output() result = new EventEmitter<number>();

  roll: number;
  hasError: boolean;
  errorMessage: string;
  error = AlertType.Error;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
  }

  submit() {
    if (this.numberOfDice <= this.roll && this.roll <= this.numberOfDice * 6) {
      this.result.emit(this.roll);
    } else {
      this.hasError = true;
      this.errorMessage = `The roll must be between ${this.numberOfDice} and ${this.numberOfDice * 6}.`;
    }
  }
}
