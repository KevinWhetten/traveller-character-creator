import {Component, OnInit} from '@angular/core';
import {Career, TrainingTable} from "../../../../models/career";
import {CareerService} from "../../../../services/data-services/career.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {CharacterService} from "../../../../services/character.service";
import {CharacterSkill} from "../../../../models/character-skill";
import {Subskill} from "../../../../models/skill";
import {SkillService} from "../../../../services/data-services/skill.service";

@Component({
  selector: 'app-first-career-basic-training',
  templateUrl: './first-career-basic-training.component.html',
  styleUrls: ['./first-career-basic-training.component.css']
})
export class FirstCareerBasicTrainingComponent implements OnInit {
  private career: Career;
  serviceSkillsTable: TrainingTable;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.career = this._careerService.getCareer(this._metadataService.getCurrentCareer());

    if (this._metadataService.getCareers().includes(this.career.Name)) {
      this._metadataService.setCurrentUrl('character-creator/careers/skill-generation');
    } else {
      this.serviceSkillsTable = this.career.TrainingTables.find(x => x.Name == 'Service Skills') || {} as TrainingTable;
    }
  }

  getTrainings() {
    let skills = [] as string[];
    for (let i = 1; i <= 6; i++) {
      let serviceSkills = this.serviceSkillsTable.Trainings[i].SkillNames;
      for (let j = 0; j < serviceSkills.length; j++) {
        let skill = this._skillService.getSkill(serviceSkills[j]);
        if ((skill as Subskill).ParentSkill) {
          if (!skills.includes((skill as Subskill).ParentSkill)) {
            skills.push((skill as Subskill).ParentSkill);
          }
        } else {
          skills.push(serviceSkills[j]);
        }
      }
    }
    return skills;
  }

  submit() {
    this._characterService.addSkills(this.getTrainings().map(x => {
      return {Name: x, Value: 0} as CharacterSkill
    }));
    this._metadataService.addCareer(this.career.Name);
    this._metadataService.setCurrentUrl('character-creator/careers/skill-generation');
  }
}
