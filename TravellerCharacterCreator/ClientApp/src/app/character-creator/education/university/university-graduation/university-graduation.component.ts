import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {Router} from "@angular/router";
import {CareerService} from "../../../../services/data-services/career.service";
import {RollingService} from "../../../../services/data-services/rolling.service";
import {SkillService} from "../../../../services/data-services/skill.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-university-skills',
  templateUrl: './university-graduation.component.html',
  styleUrls: ['./university-graduation.component.css']
})
export class UniversityGraduationComponent implements OnInit {
  @Output() graduation = new EventEmitter();
  success: boolean = false;
  honors: boolean = false;
  failure: boolean = false;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _loggingService: LoggingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  submit(result: number) {
    if (result >= 11) {
      this.getHonorsBonus();
    } else if (result >= 7) {
      this.getGraduationBonus();
    } else {
      this._loggingService.addLog('I didn\'t graduate...');
      this.failure = true;
    }
  }

  private getGraduationBonus() {
    this._loggingService.addLog('----- Graduated! -----');
    this.success = true;
    this._characterService.increaseSkills(this._characterMetadataService.getUniversitySkills());
    this._characterService.increaseEducation(2);
    this._characterMetadataService.graduateUniversity();
  }

  private getHonorsBonus() {
    this._loggingService.addLog('----- Graduated with Honors! -----');
    this.honors = true;
    this._characterService.increaseSkills(this._characterMetadataService.getUniversitySkills());
    this._characterService.increaseEducation(2);
    this._characterMetadataService.graduateUniversityWithHonors();
  }

  moveOn() {
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }
}
