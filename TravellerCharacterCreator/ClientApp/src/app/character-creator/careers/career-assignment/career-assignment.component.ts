import {Component, OnInit} from '@angular/core';
import {Assignment, Career} from "../../../models/career";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CareerService} from "../../../services/data-services/career.service";
import {AlertType} from "../../../controls/alert/alert.component";

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
              private _metadataService: CharacterMetadataService) {
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
}
