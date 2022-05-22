import {Component, OnInit} from '@angular/core';
import {Assignment, Career} from "../../../models/career";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CareerService} from "../../../services/data-services/career.service";
import {AlertType} from "../../../controls/alert/alert.component";
import {CharacterService} from "../../../services/character.service";
import {RollingService} from "../../../services/data-services/rolling.service";

@Component({
  selector: 'app-career-assignment',
  templateUrl: './career-assignment.component.html',
  styleUrls: ['./career-assignment.component.scss']
})
export class CareerAssignmentComponent implements OnInit {
  career: Career;
  assignmentName: string;
  hasError: boolean = false;
  errorMessage: string = '';
  error = AlertType.Error;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
    let careerName = this._metadataService.getCurrentCareer()
    this.career = this._careerService.getCareer(careerName);
  }

  submit() {
    if (this.assignmentName != undefined && this.assignmentName != '') {
      this._metadataService.setAssignment(this.assignmentName);
      this._metadataService.setCurrentUrl('character-creator/careers/basic-training')
    } else {
      this.hasError = true;
      this.errorMessage = 'You must select an assignment!';
    }
  }

  getSkills(assignment: Assignment) {
    let assignmentTable = this.career.TrainingTables.find(x => x.Name == assignment.Name);
    let skills = [] as string[];
    if (assignmentTable) {
      for (let i = 1; i <= 6; i++) {
        skills.push(assignmentTable.Trainings[i].BenefitName);
      }
    }
    return skills.join(', ');
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
