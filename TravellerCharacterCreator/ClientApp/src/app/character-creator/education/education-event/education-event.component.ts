import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Character} from "../../../models/Character";
import {CharacterService} from "../../../services/character.service";
import {Router} from "@angular/router";
import {DmService} from "../../../services/dm.service";
import {SkillService} from "../../../services/skill.service";

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
  character: Character;
  socialStandingDm: number;
  hobbySkill: string;
  private log: string;

  constructor(private _router: Router,
              private _characterService: CharacterService,
              private _dmService: DmService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.character = this._characterService.getCharacter();
    this.socialStandingDm = this._dmService.getDm(this.character.characteristics.socialStatus)
  }

  submit() {
    this.submitted = true;
  }

  testPsi() {
    this._characterService.addLog('Decided to be tested for Psionics...');
    this._characterService.updateCurrentUrl('character-creator/education/university/psionic-test');
    this._router.navigate(['character-creator/education/university/psionic-test']);
  }

  moveOn() {
    this._characterService.addLog('Decided against being tested for Psionics.');
    this.graduate();
  }

  skipGraduation() {
    this._characterService.addLog('A tragedy occurred during my education years. I had to drop out early and didn\'t graduate.');
    this._characterService.updateCurrentUrl('character-creator/careers');
    this.character.termNumber++;
    this._router.navigate(['character-creator/careers']);
  }

  prank() {
    if (this.specificEventNumber == 2) {
      this.prankJail = true;
    } else if (this.specificEventNumber + this._dmService.getDm(this.character.characteristics.socialStatus) < 8) {
      this.prankEnemy = true;
    } else {
      this.prankRival = true;
    }
  }

  gainPrankRival() {
    this.character.Connections.Rivals.push('Someone I pranked during my Education.');
    this._characterService.addLog('I pulled a prank that went wrong. Someone got hurt, but not too bad. Whoever it is is now my [Rival]')
    this._characterService.updateCharacter(this.character);
    this.graduate();
  }

  gainPrankEnemy() {
    this.character.Connections.Enemies.push('Someone I pranked during my Education.');
    this._characterService.addLog('I pulled a prank that went seriously wrong. Someone got hurt. Whoever it is is now my [Enemy].');
    this._characterService.updateCharacter(this.character);
    this.graduate();
  }

  jailed() {
    this.character.Connections.Enemies.push('Someone I pranked during my Education.');
    this.character.nextCareer = 'prison';
    this._characterService.addLog('I pulled a prank that went seriously wrong. Someone got hurt. Whoever it is is now my [Enemy]. I also ended up going to prison!');
    this._characterService.updateCurrentUrl('character-creator/careers/prison');
    this._characterService.updateCharacter(this.character);
    this.graduate();
  }

  party() {
    this.log = 'I partied it up during my education!';
    if (this._skillService.updateSkill('Carouse', 1)) {
      this.log += ' I gained [Carouse 1].';
    } else {
      this.log += ' I already had [Carouse 1] or higher, so nothing happened.'
    }
    this._characterService.addLog(this.log);
    this._characterService.updateCharacter(this.character);
    this.graduate();
  }

  friends() {
    for (let i = 0; i < this.specificEventNumber; i++) {
      this.character.Connections.Allies.push('Someone in my clique/group during my education.');
    }
    this._characterService.addLog(`I had a close group of friends during my education. I gained [${this.specificEventNumber} Allies]`);
    this._characterService.updateCharacter(this.character);
    this.graduate();
  }

  lifeEvent() {
    this._characterService.updateCurrentUrl('character-creator/education/university/life-event');
    this._router.navigate(['character-creator/education/university/life-event']);
  }

  politicalMovement() {
    this.log = 'I joined a political movement during my education.';
    if (this.specificEventNumber + this._dmService.getDm(this.character.characteristics.socialStatus) >= 8) {
      this.politicalLeader = true;
    }
    else {
      this.politicalGrunt = true;
    }
  }

  becamePoliticalLeader () {
    this.character.Connections.Allies.push('One person in the political movement I joined during my education.');
    this.character.Connections.Enemies.push('One person outside of the political movement I joined during my education.');
    this.log += ' I quickly became a leading figure in the movement, and as a result I gained 1 [Ally] and 1 [Enemy].'
    this._characterService.updateCharacter(this.character);
    this._characterService.addLog(this.log);
    this.graduate();
  }

  notPoliticalLeader() {
    this.log += ' However, nothing really came of it.'
    this._characterService.addLog(this.log);
    this.graduate();
  }

  hobby() {
    this._skillService.updateSkill(this.hobbySkill, 0);
    this._characterService.addLog(`I picked up a hobby or other area of study! I gained [${this.hobbySkill} 0]`);
    this._characterService.updateCharacter(this.character);
    this.graduate();
  }

  tutor() {
    this.log = 'I had a tutor who rubbed me the wrong way. I worked hard to overturn their conclusions, '
    if(this.tutorSkill != '' && (this.specificEventNumber >= 2 && this.specificEventNumber <= 12)) {
      let skillScore = this._skillService.getSkillScore(this.tutorSkill);
      if (this.specificEventNumber + skillScore >= 9) {
        this.tutorBeaten = true;
      } else {
        this.tutorWins = true;
      }
    }
  }

  beatTutor(){
    let skillScore = this._skillService.getSkillScore(this.tutorSkill);
    this._skillService.updateSkill(this.tutorSkill, skillScore > 0 ? skillScore + 1 : 1);
    this.character.Connections.Rivals.push('My tutor who I 1-upped during my education.');
    this._characterService.updateCharacter(this.character);
    this.log += `and succeeded! I gained [${this.tutorSkill} ${skillScore > 0 ? skillScore + 1 : 1}], and the tutor became my [Rival].`;
    this._characterService.addLog(this.log);
    this.graduate();
  }

  lostToTutor() {
    this.log += 'but failed. Oh well.';
    this._characterService.addLog(this.log);
    this.graduate();
  }

  drafted() {
    this.character.termNumber++;
    this._characterService.addLog('I was drafted in the middle of my education. Unfortunately, I did not graduate.')
    this._characterService.updateCurrentUrl('character-creator/careers/draft');
    this._router.navigate(['character-creator/careers/draft']);

  }

  avoidDraft() {
    if(this.specificEventNumber + this._dmService.getDm(this.character.characteristics.socialStatus) >= 9) {
      this._characterService.addLog('Thanks to my social standing, I avoided the draft during my education.');
      this.graduate();
    }
    else {
      this.avoidDraftFailure = true;
    }
  }

  fleeDraft() {
    this.character.termNumber++;
    this._characterService.updateCharacter(this.character);
    this._characterService.addLog('I fled from the draft during my education.');
    this._characterService.updateCurrentUrl('character-creator/careers/drifter');
    this._router.navigate(['character-creator/careers/drifter']);
  }

  recognized() {
    this.character.characteristics.socialStatus++;
    this._characterService.updateCharacter(this.character);
    this._characterService.addLog(`I was widely recognized for my initiative and innovative approach to study! I gained [SOC ${this.character.characteristics.socialStatus}]`);
    this.graduate();
  }

  private graduate() {
    this.graduated.emit();
  }
}
