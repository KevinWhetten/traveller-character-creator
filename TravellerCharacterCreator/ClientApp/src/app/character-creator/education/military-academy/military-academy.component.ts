import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../services/character.service";
import {Character} from "../../../models/Character";
import {Router} from "@angular/router";
import {DmService} from "../../../services/dm.service";
import {PercentageService} from "../../../services/percentage.service";
import {SkillService} from "../../../services/skill.service";

@Component({
  selector: 'app-military-academy',
  templateUrl: './military-academy.component.html',
  styleUrls: ['./military-academy.component.css']
})
export class MilitaryAcademyComponent implements OnInit {
  applied: boolean = false;
  acceptanceRoll: number;
  armyAcademy: boolean = false;
  marinesAcademy: boolean = false;
  navyAcademy: boolean = false;
  entryRoll: number = 2;
  armyChance: number;
  marinesChance: number;
  navyChance: number;
  private character: Character;
  private endDm: number;
  private intDm: number;
  armySkills = ['Athletics', 'Drive', 'GunCombat', 'HeavyWeapons',
    'Melee', 'Recon', 'VaccSuit'];
  marinesSkills = ['Athletics', 'GunCombat', 'HeavyWeapons', 'Tactics',
    'Stealth', 'VaccSuit'];
  navySkills = ['Athletics', 'Gunner', 'GunCombat', 'Mechanic',
    'Pilot', 'VaccSuit'];

  constructor(private _router: Router,
              private _characterService: CharacterService,
              private _dmService: DmService,
              private _percentageService: PercentageService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.character = this._characterService.getCharacter();
    this.endDm = this._dmService.getDm(this.character.characteristics.endurance) - (2 * (this.character.termNumber - 1));
    this.intDm = this._dmService.getDm(this.character.characteristics.intellect) - (2 * (this.character.termNumber - 1));
    this.armyChance = this._percentageService.get2d6Percentage(8, this.endDm);
    this.marinesChance = this._percentageService.get2d6Percentage(9, this.endDm);
    this.navyChance = this._percentageService.get2d6Percentage(9, this.intDm);
  }

  army() {
    if (this.acceptanceRoll + this.endDm >= 8) {
      this.armyAcademy = true;
      let skills = [];
      for (let skill of this.armySkills) {
        skills.push({skillName: skill, value: 0});
      }
      this._skillService.updateSkills(skills);
    }
  }

  marines() {
    if (this.acceptanceRoll + this.endDm >= 9) {
      this.marinesAcademy = true;
      let skills = [];
      for (let skill of this.marinesSkills) {
        skills.push({skillName: skill, value: 0});
      }
      this._skillService.updateSkills(skills);
    }
  }

  navy() {
    if (this.acceptanceRoll + this.intDm > 9) {
      this.navyAcademy = true;
      let skills = [];
      for (let skill of this.navySkills) {
        skills.push({skillName: skill, value: 0});
      }
      this._skillService.updateSkills(skills);
    }
  }

  apply(branch: string) {
    if (branch == 'Army') {
      this.army();
    } else if (branch == 'Marines') {
      this.marines();
    } else if (branch == 'Navy') {
      this.navy();
    }
    this.applied = true;
  }

  moveOn() {
    if (!this.armyAcademy && !this.marinesAcademy && !this.navyAcademy) {
      this.character.currentUrl = 'character-creator/careers';
      this._characterService.updateCharacter(this.character);
      this._router.navigate(['character-creator/careers']);
    }
    this.character.currentUrl = 'character-creator/education/military-academy/event';
    this._characterService.updateCharacter(this.character);
    this._router.navigate(['character-creator/education/military-academy/event']);
  }
}
