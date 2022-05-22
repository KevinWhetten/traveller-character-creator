import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AlertType} from "../alert/alert.component";
import {CharacterService} from "../../services/character.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../services/data-services/rolling.service";

@Component({
  selector: 'app-skill-roll',
  templateUrl: './skill-roll.component.html',
  styleUrls: ['./skill-roll.component.scss']
})
export class SkillRollComponent implements OnInit {
  @Input() skill: string;
  @Input() target: number;
  @Output() rolled = new EventEmitter<boolean>();
  @Output() result = new EventEmitter<number>();
  @Output() rawResult = new EventEmitter<{rawResult: number, modifier: number}>();

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

  getModifier() {
    return this._characterService.getSkills()[this.skill];
  }

  submit() {
    if (2 <= this.roll && this.roll <= 12) {
      if (this.roll + this.getModifier() >= this.target) {
        this.rolled.emit(true);
      } else {
        this.rolled.emit(false);
      }
      this.result.emit(this.roll + this.getModifier());
      this.rawResult.emit({rawResult: this.roll, modifier: this.getModifier()});
    } else {
      this.hasError = true;
      this.errorMessage = 'The roll must be between 2 and 12.';
    }
  }
}
