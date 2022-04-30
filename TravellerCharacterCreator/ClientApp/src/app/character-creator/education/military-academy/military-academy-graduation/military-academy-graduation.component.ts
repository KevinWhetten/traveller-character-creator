import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Character} from "../../../../models/Character";
import {Router} from "@angular/router";
import {CharacterService} from "../../../../services/character.service";
import {MilitaryAcademyService} from "../../../../services/military-academy.service";
import {CareerService} from "../../../../services/career.service";
import {DmService} from "../../../../services/dm.service";

@Component({
  selector: 'app-military-academy-graduation',
  templateUrl: './military-academy-graduation.component.html',
  styleUrls: ['./military-academy-graduation.component.css']
})
export class MilitaryAcademyGraduationComponent implements OnInit {
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
              public _militaryAcademyService: MilitaryAcademyService,) {
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
    this.success = true;
  }

  private getHonorsBonus() {
    this.honors = true;
  }

  moveOn() {
    this._router.navigate(['character-creator/careers']);
  }
}
