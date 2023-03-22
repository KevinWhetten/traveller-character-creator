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
    if (this._characterMetadataService.getTerm() == 2) {
      this.universityEntryDifficulty++;
    }
    if (this._characterMetadataService.getTerm() == 3) {
      this.universityEntryDifficulty += 2;
    }
    if (this._characterService.getCharacteristics().SocialStanding.current >= 9) {
      this.universityEntryDifficulty--;
    }
  }

  toCareer() {
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }

  getModifier() {
    let mod = 0;
    if(this._characterMetadataService.getTerm() == 2){
      mod++;
    }else if (this._characterMetadataService.getTerm() == 3){
      mod +=2;
    }
    if(this._characterService.getSocialStanding().current >= 9){
      mod++;
    }
    return mod;
  }

  submit(accepted: boolean) {
    if (accepted) {
      this._loggingService.addLog('I was accepted!');
      this._characterService.increaseEducation(1);
      this._characterMetadataService.setCurrentUrl('character-creator/education/university/skills');
    } else {
      this.failed = true;
      this._loggingService.addLog('But I was rejected...');
    }
  }
}
