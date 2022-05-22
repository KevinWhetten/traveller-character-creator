import {Component, OnInit} from '@angular/core';
import {Assignment, Career} from "../../../models/career";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterService} from "../../../services/character.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../../services/data-services/rolling.service";

@Component({
  selector: 'app-career-advancement',
  templateUrl: './career-advancement.component.html',
  styleUrls: ['./career-advancement.component.scss']
})
export class CareerAdvancementComponent implements OnInit {
  career: Career;
  assignmentName: string;
  assigment: Assignment;
  advancementRoll: number;
  advanced: boolean = false;
  rolled: boolean = false;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
    this.career = this._careerService.getCareer(this._metadataService.getCurrentCareer());
    this.assignmentName = this._metadataService.getAssignment();
    this.assigment = this.career.Assignments.find(x => x.Name == this.assignmentName) || {} as Assignment;
  }

  getModifier() {
    let modifier = -3;

    if (this.assigment.Advancement.characteristic.includes('STR')) {
      let mod = this._rollingService.getDm(this._characterService.getStrength().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.assigment.Advancement.characteristic.includes('DEX')) {
      let mod = this._rollingService.getDm(this._characterService.getDexterity().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.assigment.Advancement.characteristic.includes('END')) {
      let mod = this._rollingService.getDm(this._characterService.getEndurance().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.assigment.Advancement.characteristic.includes('INT')) {
      let mod = this._rollingService.getDm(this._characterService.getIntellect().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.assigment.Advancement.characteristic.includes('EDU')) {
      let mod = this._rollingService.getDm(this._characterService.getEducation().current)
      if (mod > modifier) {
        modifier = mod;
      }
    } else if (this.assigment.Advancement.characteristic.includes('SOC')) {
      let mod = this._rollingService.getDm(this._characterService.getSocialStanding().current)
      if (mod > modifier) {
        modifier = mod;
      }
    }
    return modifier;
  }

  submit(passed: boolean) {
    this.rolled = true;
    if (passed) {
      this.advanced = true;
      this._metadataService.promote();
    }
  }

  getRank() {
    return this._metadataService.getRank();
  }

  getRankName() {
    let rank = this.getRank();
    let rankTable = this.career.RankTables.find(x => x.Assignments.includes(this.assignmentName));
    if (rankTable) {
      return rankTable.Ranks[rank].Name;
    }
    return '-';
  }

  proceed() {
    this._metadataService.setCurrentUrl('character-creator/careers/leaving')
  }
}
