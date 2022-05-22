import {Component, Input, OnInit} from '@angular/core';
import {Career} from "../../../../models/career";
import {CharacterService} from "../../../../services/character.service";
import {RollingService} from "../../../../services/data-services/rolling.service";

@Component({
  selector: 'app-career-progress',
  templateUrl: './career-progress.component.html',
  styleUrls: ['./career-progress.component.scss']
})
export class CareerProgressComponent implements OnInit {
  @Input() career: Career;

  constructor(private _characterService: CharacterService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
  }

  getModifier(characteristic: string) {
    let score = 0;
    if (characteristic.includes('STR')) {
      score = this._characterService.getStrength().current > score ? this._characterService.getStrength().current : score;
    }
    if (characteristic.includes('DEX')) {
      score = this._characterService.getDexterity().current > score ? this._characterService.getDexterity().current : score;
    }
    if (characteristic.includes('END')) {
      score = this._characterService.getEndurance().current > score ? this._characterService.getEndurance().current : score;
    }
    if (characteristic.includes('INT')) {
      score = this._characterService.getIntellect().current > score ? this._characterService.getIntellect().current : score;
    }
    if (characteristic.includes('EDU')) {
      score = this._characterService.getEducation().current > score ? this._characterService.getEducation().current : score;
    }
    if (characteristic.includes('SOC')) {
      score = this._characterService.getSocialStanding().current > score ? this._characterService.getSocialStanding().current : score;
    }
    if (characteristic.includes('PSI')) {
      score = this._characterService.getPsi().current > score ? this._characterService.getPsi().current : score;
    }
    return this._rollingService.getDm(score);
  }
}
