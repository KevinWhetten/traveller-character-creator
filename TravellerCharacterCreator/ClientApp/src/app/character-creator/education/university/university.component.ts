import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {DmService} from "../../../services/data-services/dm.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-university',
  templateUrl: './university.component.html',
  styleUrls: ['./university.component.css']
})
export class UniversityComponent implements OnInit {
  acceptanceRoll: number = 2;
  private universityEntryDifficulty: number = 7;
  failed: boolean = false;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: DmService,
              private _loggingService: LoggingService) {
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

  submitEntry() {
    if (this.acceptanceRoll + this._dmService.getDm(this._characterService.getCharacteristics().Education) >= this.universityEntryDifficulty) {
      this._loggingService.addLog('I was accepted!');
      this._characterService.increaseEducation(1);
      this._characterMetadataService.setCurrentUrl('character-creator/education/university/skills');
    } else {
      this.failed = true;
      this._loggingService.addLog('But I was rejected...');
    }
  }

  toCareer() {
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }
}
