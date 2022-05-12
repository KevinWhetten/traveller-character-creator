import { Component, OnInit } from '@angular/core';
import {Career, TrainingTable} from "../../../models/career";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CareerService} from "../../../services/data-services/career.service";

@Component({
  selector: 'app-career-basic-training',
  templateUrl: './career-basic-training.component.html',
  styleUrls: ['./career-basic-training.component.css']
})
export class CareerBasicTrainingComponent implements OnInit {
  private career: Career;
  serviceSkillsTable: TrainingTable;

  constructor(private _careerService: CareerService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this.career = this._careerService.getCareer(this._metadataService.getCurrentCareer());

    if(this._metadataService.getCareers().includes(this.career.Name)){
      this._metadataService.setCurrentUrl('character-creator/careers/skill-generation');
    } else {
      this.serviceSkillsTable = this.career.TrainingTables.find(x => x.Name == 'Service Skills') || {} as TrainingTable;
    }
  }

}
