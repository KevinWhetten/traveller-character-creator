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
  @Input() numberOfRolls: number = 1;
  @Input() select: string = '';
  @Output() rollResult = new EventEmitter<number>();

  hasError: boolean;
  errorMessage: string;
  error = AlertType.Error;
  rolls: number[] = [];

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
    for(let i = 0; i < this.numberOfRolls; i++){
      this.rolls.push(0);
    }
  }

  getRange() {
    let range = [] as number[];
    for(let i = 0; i < this.numberOfRolls; i++){
      range.push(i + 1);
    }
    return range;
  }

  submit() {
    let roll;
    if(this.select == 'lowest'){
      roll = this.findLowest();
    } else if (this.select == 'highest'){
      roll = this.findHighest();
    } else {
      roll = this.rolls[0];
    }

    if (this.numberOfDice <= roll && roll <= this.numberOfDice * 6) {
      this.rollResult.emit(roll);
    } else {
      this.hasError = true;
      this.errorMessage = `The roll must be between ${this.numberOfDice} and ${this.numberOfDice * 6}.`;
    }
  }

  findLowest(){
    return this.rolls.sort()[0];
  }

  findHighest(){
    return this.rolls.sort()[this.rolls.length - 1];
  }
}
