import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {RollingService} from "../../../services/data-services/rolling.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-university',
  templateUrl: './university.component.html',
  styleUrls: ['./university.component.scss']
})
export class UniversityComponent implements OnInit {
  acceptanceRoll: number = 2;
  private universityEntryDifficulty: number = 7;
  failed: boolean = false;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _loggingService: LoggingService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
    this._characterService.startNewTerm();
    if (this._characterMetadataService.getTerm() == 2) {
      this.universityEntryDifficulty++;
    }
    if (this._characterMetadataService.getTerm() == 3) {
      this.universityEntryDifficulty += 2;
    }
    if (this._characterService.getCharacteristics().SocialStatus >= 9) {
      this.universityEntryDifficulty--;
    }
  }

  toCareer() {
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }

  getModifier() {
    let score = this._characterService.getEducation();
    return this._rollingService.getDm(score);
  }

  submit() {
    if (this.acceptanceRoll + this._rollingService.getDm(this._characterService.getCharacteristics().Education) >= this.universityEntryDifficulty) {
      this._loggingService.addLog('I was accepted!');
      this._characterService.increaseEducation(1);
      this._characterMetadataService.setCurrentUrl('character-creator/education/university/skills');
    } else {
      this.failed = true;
      this._loggingService.addLog('But I was rejected...');
    }
  }
}
