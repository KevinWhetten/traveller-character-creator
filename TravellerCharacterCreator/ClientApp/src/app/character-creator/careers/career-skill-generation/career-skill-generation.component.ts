import {Component, OnInit} from '@angular/core';
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {Career, TrainingTable} from "../../../models/career";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterService} from "../../../services/character.service";

@Component({
  selector: 'app-career-skill-generation',
  templateUrl: './career-skill-generation.component.html',
  styleUrls: ['./career-skill-generation.component.scss']
})
export class CareerSkillGenerationComponent implements OnInit {
  career: Career;
  assignment: string;
  selectedTable: string;
  trainingSkill: string;
  trainingSubskill: string;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService) {
  }

  ngOnInit(): void {
    let careerName = this._metadataService.getCurrentCareer();
    this.career = this._careerService.getCareer(careerName);
    this.assignment = this._metadataService.getAssignment();
  }

  getEduScore() {
    return this._characterService.getEducation();
  }

  getEligibleTables() {
    let tables = this.career.TrainingTables;
    let eligibleTables = [] as TrainingTable[];
    for (let table of tables) {
      if(table.Assignments.includes(this.assignment) && table.MinEDU < this.getEduScore().current){
        eligibleTables.push(table);
      }
    }
    if(eligibleTables.length % 2 == 1){
      eligibleTables.push({Name: 'Dummy'} as TrainingTable);
    }
    return eligibleTables;
  }

  selectTable(table: TrainingTable) {
    this.selectedTable = table.Name;
    this.trainingSkill = table.Trainings[1].SkillNames[0];
  }

  changeSelection(skill: string) {
    this.trainingSkill = skill;
  }

  trainSkill() {
    if(this.trainingSkill.includes('STR')){
      this._characterService.increaseStrength(1);
    } else if(this.trainingSkill.includes('DEX')){
      this._characterService.increaseDexterity(1);
    } else if(this.trainingSkill.includes('END')){
      this._characterService.increaseEndurance(1);
    } else if(this.trainingSkill.includes('INT')){
      this._characterService.increaseIntellect(1);
    } else if(this.trainingSkill.includes('EDU')){
      this._characterService.increaseEducation(1);
    } else if(this.trainingSkill.includes('SOC')){
      this._characterService.increaseSocialStatus(1);
    } else if(this.trainingSkill.includes('PSI')){
      this._characterService.increasePsi(1);
    } else if(this.trainingSubskill){
      this._characterService.increaseSkills([{Name: this.trainingSubskill, Value: 1}]);
    } else {
      this._characterService.increaseSkills([{Name: this.trainingSkill, Value: 1}]);
    }

    this._metadataService.setCurrentUrl('character-creator/careers/survival');
  }
}
