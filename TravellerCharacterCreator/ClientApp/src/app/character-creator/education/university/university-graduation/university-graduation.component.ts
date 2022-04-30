import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {Router} from "@angular/router";
import {UniversityService} from "../../../../services/university.service";
import {Character} from "../../../../models/Character";
import {CareerService} from "../../../../services/career.service";
import {DmService} from "../../../../services/dm.service";
import {SkillService} from "../../../../services/skill.service";

@Component({
  selector: 'app-university-skills',
  templateUrl: './university-graduation.component.html',
  styleUrls: ['./university-graduation.component.css']
})
export class UniversityGraduationComponent implements OnInit {
  @Output() graduation = new EventEmitter();
  graduationRoll: number;
  private character: Character;
  success: boolean = false;
  honors: boolean = false;
  failure: boolean = false;

  constructor(private _router: Router,
              private _careerService: CareerService,
              private _characterService: CharacterService,
              private _dmService: DmService,
              private _skillService: SkillService,
              private _universityService: UniversityService) {
  }

  ngOnInit(): void {
    this.character = this._characterService.getCharacter();
  }

  updateGraduation($event: number) {
    this.graduationRoll = $event;
  }

  submitGraduation() {
    let graduationResult = this.graduationRoll + this._dmService.getDm(this.character.characteristics.intellect);

    if (graduationResult >= 11) {
      this.getHonorsBonus();
    } else if (graduationResult >= 7) {
      this.getGraduationBonus();
    } else {
      this.failure = true;
    }
    this.character.currentUrl = 'character-creator/careers';
    this._characterService.updateCharacter(this.character);
  }

  private getGraduationBonus() {
    this._characterService.addLog(`I graduated from University!`);
    this._characterService.addLog(`I gained Skills: [${this._universityService.getMajorSkill().name} 2] and [${this._universityService.getMinorSkill().name} 1]`);
    this._characterService.addLog(`I gained [+2 EDU]`);
    this._characterService.addLog(`I gained +1 to qualify for the following careers: Agent, Army, Citizen (corporate), Entertainer (journalist), Marines, Navy, Scholar, and Scouts.`);
    this._characterService.addLog(`I gained a +1 Commission roll to be taken if I take a military career next. Success means I enter the career at officer rank (01).`);
    this.success = true;
    this.increaseSkills();
    this.character.characteristics.education += 2;
    this.addCareerBonuses(1);
    this.addCommissions();
  }

  private getHonorsBonus() {
    this._characterService.addLog(`I graduated from University with Honors!`);
    this._characterService.addLog(`I gained Skills: [${this._universityService.getMajorSkill().name} 2] and [${this._universityService.getMinorSkill().name} 1]`);
    this._characterService.addLog(`I gained [+2 EDU]`);
    this._characterService.addLog(`I gained +2 to qualify for the following careers: Agent, Army, Citizen (corporate), Entertainer (journalist), Marines, Navy, Scholar, and Scouts.`);
    this._characterService.addLog(`I gained a +2 Commission roll to be taken if I take a military career next. Success means I enter the career at officer rank (01).`);
    this.honors = true;
    this.increaseSkills();
    this.character.characteristics.education += 2;
    this.addCareerBonuses(2);
    this.addCommissions(2);
  }

  private increaseSkills() {
    let majorSkill = this._universityService.getMajorSkill();
    let minorSkill = this._universityService.getMinorSkill();
    this._skillService.updateSkill(majorSkill.name, majorSkill.score + 1);
    this._skillService.updateSkill(minorSkill.name, minorSkill.score + 1);
  }

  private addCareerBonuses(amount: number) {
    this._careerService.addPermanentBonusToApply('Agent', amount);
    this._careerService.addPermanentBonusToApply('Corporate', amount);
    this._careerService.addPermanentBonusToApply('Journalist', amount);
    this._careerService.addPermanentBonusToApply('Scholar', amount);
    this._careerService.addBonusToNextApplication('Army', amount);
    this._careerService.addBonusToNextApplication('Marines', amount);
    this._careerService.addBonusToNextApplication('Navy', amount);
    this._careerService.addBonusToNextApplication('Scouts', amount);
  }

  private addCommissions(amount: number = 0) {
    this._careerService.addCommissionRoll('Army', amount);
    this._careerService.addCommissionRoll('Marines', amount);
    this._careerService.addCommissionRoll('Navy', amount);
    this._careerService.addCommissionRoll('Scouts', amount);
  }

  moveOn() {
    this._router.navigate(['character-creator/careers']);
  }
}
