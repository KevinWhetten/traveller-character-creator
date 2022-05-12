import { Component, OnInit } from '@angular/core';
import {Career, TrainingTable} from "../../../models/career";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterSkill} from "../../../models/character-skill";
import {CharacterService} from "../../../services/character.service";

@Component({
  selector: 'app-career-basic-training',
  templateUrl: './career-basic-training.component.html',
  styleUrls: ['./career-basic-training.component.css']
})
export class CareerBasicTrainingComponent implements OnInit {
  private career: Career;
  serviceSkillsTable: TrainingTable;
  basicTrainingSkill: string;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    if(!this._metadataService.getCareers() || this._metadataService.getCareers().length == 0){
      this._metadataService.setCurrentUrl('character-creator/careers/basic-training/first-career')
    }

    this.career = this._careerService.getCareer(this._metadataService.getCurrentCareer());

    if(this._metadataService.getCareers().includes(this.career.Name)){
      this._metadataService.setCurrentUrl('character-creator/careers/skill-generation');
    } else {
      this.serviceSkillsTable = this.career.TrainingTables.find(x => x.Name == 'Service Skills') || {} as TrainingTable;
    }
  }

  getTrainings() {
    let skills = [] as string[];
    for (let i = 1; i <= 6; i++) {
      let serviceSkills = this.serviceSkillsTable.Trainings[i].SkillNames;
      for(let j = 0; j < serviceSkills.length; j++) {
        skills.push(serviceSkills[j]);
      }
    }
    return skills;
  }

  submit() {
    this._characterService.addSkills([{Name: this.basicTrainingSkill, Value: 0} as CharacterSkill]);
    this._metadataService.addCareer(this.career.Name);
    this._metadataService.setCurrentUrl('character-creator/careers/skill-generation');
  }

  skillIsUntrained(skill: string): boolean {
    return this._characterService.getSkills()[skill] == undefined
  }
}
