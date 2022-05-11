import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {RollingService} from "../../../services/data-services/rolling.service";
import {SkillService} from "../../../services/data-services/skill.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CharacterSkill} from "../../../models/character-skill";
import {LoggingService} from "../../../services/metadata-services/logging.service";
import {PercentageService} from "../../../services/data-services/percentage.service";

@Component({
  selector: 'app-education-event',
  templateUrl: './education-event.component.html',
  styleUrls: ['./education-event.component.css']
})
export class EducationEventComponent implements OnInit {
  @Output() graduated = new EventEmitter();
  @Output() psionicTest = new EventEmitter();
  @Output() lifeEventOccurs = new EventEmitter();
  eventResult: number = 2;
  submitted: boolean = false;
  prankJail: boolean = false;
  prankEnemy: boolean = false;
  prankRival: boolean = false;
  friendGroup: boolean = false;
  politicalLeader: boolean = false;
  politicalGrunt: boolean = false;
  tutorSkill: string = '';
  tutorBeaten: boolean = false;
  tutorWins: boolean = false;
  avoidDraftAttempt: boolean = false;
  avoidDraftFailure: boolean = false;
  specificEventNumber: number = 0;
  socialStandingDm: number;
  hobbySkill: string;
  private log: string;
  story: string;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _loggingService: LoggingService,
              public _percentageService: PercentageService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.socialStandingDm = this._dmService.getDm(this._characterService.getSocialStatus());
    this.eventResult = this._characterMetadataService.getEventNumber();
    if (this.eventResult > 0) {
      this.submitted = true;
    }
  }

  submit() {
    this._loggingService.addLog('----- Education Event -----');
    this.submitted = true;
    this._characterMetadataService.setEventNumber(this.eventResult);
  }

  lifeEvent() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/life-event');
  }

  graduate() {
    this.graduated.emit();
  }
}
