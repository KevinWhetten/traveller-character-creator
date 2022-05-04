import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {Router} from "@angular/router";
import {DmService} from "../../../services/data-services/dm.service";
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

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: DmService,
              private _loggingService: LoggingService,
              public _percentageService: PercentageService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.socialStandingDm = this._dmService.getDm(this._characterService.getSocialStatus());
  }

  submit() {
    this._loggingService.addLog('----- Education Event -----');
    this.submitted = true;
  }

  testPsi() {
    this._loggingService.addLog('I was approached by an underground (and highly illegal) psionic group who sensed potential in me.  I got tested for Psi, and I can enter the \'Psion\' career in any subsequent term.');
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/psionic-test');
  }

  moveOn() {
    this._loggingService.addLog('I was approached by an underground (and highly illegal) psionic group who sensed potential in me, but decided against getting tested.');
    this.graduate();
  }

  skipGraduation() {
    this._loggingService.addLog('My time in education was not a happy one, and I suffered a deep tragedy. I crashed and failed to graduate.');
    this._characterMetadataService.setCurrentUrl('character-creator/careers');
  }

  prank() {
    this.log = 'A supposedly harmless prank went wrong and someone got hurt...';
    if (this.specificEventNumber == 2) {
      this.log = ' I must take the Prisoner career next term.';
      this.prankJail = true;
    } else if (this.specificEventNumber + this._dmService.getDm(this._characterService.getCharacteristics().SocialStatus) < 8) {
      this.prankEnemy = true;
    } else {
      this.prankRival = true;
    }
    this._loggingService.addLog(this.log);
  }

  gainPrankRival() {
    this._characterService.addRival('Someone I pranked during my Education.');
    this.graduate();
  }

  gainPrankEnemy() {
    this._characterService.addEnemy('Someone I pranked during my Education.');
    this.graduate();
  }

  jailed() {
    this._characterService.addEnemy('Someone I pranked during my Education.');
    this._characterMetadataService.setCurrentUrl('character-creator/careers/prison');
    this.graduate();
  }

  party() {
    this._loggingService.addLog('I took advantage of youth, and partied as much as I studied.');
    this._characterService.addSkills([{Name: this._skillService.SkillNames.Carouse, Value: 0} as CharacterSkill]);
    this.graduate();
  }

  friends() {
    this._loggingService.addLog('I became involved in a tightly knit clique or group and made a pact to remain friends forever, wherever in the galaxy we may end up.');
    for (let i = 0; i < this.specificEventNumber; i++) {
      this._characterService.addAlly('Someone in my clique/group during my education.');
    }
    this.graduate();
  }

  lifeEvent() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/life-event');
  }

  politicalMovement() {
    this.log = 'I joined a political movement.';
    if (this.specificEventNumber + this._dmService.getDm(this._characterService.getCharacteristics().SocialStatus) >= 8) {
      this.log += ' And became a leading figure!';
      this.politicalLeader = true;
    }
    else {
      this.politicalGrunt = true;
    }
    this._loggingService.addLog(this.log);
  }

  becamePoliticalLeader () {
    this._characterService.addAlly('One person in the political movement I joined during my education.');
    this._characterService.addEnemy('One person outside of the political movement I joined during my education.');
    this.graduate();
  }

  notPoliticalLeader() {
    this.graduate();
  }

  hobby() {
    this._loggingService.addLog('I developed a healthy interest in a hobby or other area of study.');
    this.graduate();
  }

  tutor() {
    this.log = 'A newly arrived tutor rubbed me the wrong way, and I worked hard to overturn their conclusions.';
    if(this.tutorSkill != '' && (this.specificEventNumber >= 2 && this.specificEventNumber <= 12)) {
      let skillScore = this._characterService.getSkills()[this.tutorSkill];
      if (this.specificEventNumber + skillScore >= 9) {
        this.log += ' I provided a truly elegant proof that soon became accepted as the standard approach!';
        this.tutorBeaten = true;
      } else {
        this.log += ' Nothing really came of it.';
        this.tutorWins = true;
      }
      this._loggingService.addLog(this.log);
    }
  }

  beatTutor(){
    this._characterService.increaseSkills([{Name: this.tutorSkill, Value: 1}]);
    this._characterService.addRival('My tutor who I 1-upped during my education.');
    this.graduate();
  }

  lostToTutor() {
    this.graduate();
  }

  drafted() {
    // Add Term
    this._loggingService.addLog('War came and a wide-ranging draft was instigated. I chose to be drafted, so I didn\'t graduate.');
    this._characterMetadataService.setCurrentUrl('character-creator/careers/draft');
  }

  avoidDraft() {
    this.log ='War came and a wide-ranging draft was instigated. I tried to get enough strings pulled to avoid the draft and complete my education,';
    if(this.specificEventNumber + this._dmService.getDm(this._characterService.getCharacteristics().SocialStatus) >= 9) {
      this.log += ' and succeeded!';
      this.graduate();
    }
    else {
      this.log += ' but failed.';
      this.avoidDraftFailure = true;
    }
    this._loggingService.addLog(this.log);
  }

  fleeDraft() {
    this._loggingService.addLog('War came and a wide-ranging draft was instigated. I chose to be flee and become a Drifter next term. I also didn\'t graduate.');
    this._characterMetadataService.setCurrentUrl('character-creator/careers/drifter');
  }

  recognized() {
    this._loggingService.addLog('I gained wide-ranging recognition of my initiative and innovative approach to study.');
    this._characterService.increaseSocialStatus(1);
    this.graduate();
  }

  private graduate() {
    this.graduated.emit();
  }
}
