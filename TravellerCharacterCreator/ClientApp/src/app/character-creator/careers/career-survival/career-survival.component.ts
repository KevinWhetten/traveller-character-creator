import { Component, OnInit } from '@angular/core';
import {Assignment, Career} from "../../../models/career";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterService} from "../../../services/character.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../../services/data-services/rolling.service";

@Component({
  selector: 'app-career-survival',
  templateUrl: './career-survival.component.html',
  styleUrls: ['./career-survival.component.scss']
})
export class CareerSurvivalComponent implements OnInit {
  career: Career;
  assignmentName: string;
  assigment: Assignment;
  qualificationRoll: number;

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

    if(this.assigment.Survival.characteristic.includes('STR')){
      let mod = this._rollingService.getDm(this._characterService.getStrength())
      if(mod > modifier){
        modifier = mod;
      }
    } else if(this.assigment.Survival.characteristic.includes('DEX')){
      let mod = this._rollingService.getDm(this._characterService.getDexterity())
      if(mod > modifier){
        modifier = mod;
      }
    } else if(this.assigment.Survival.characteristic.includes('END')){
      let mod = this._rollingService.getDm(this._characterService.getEndurance())
      if(mod > modifier){
        modifier = mod;
      }
    } else if(this.assigment.Survival.characteristic.includes('INT')){
      let mod = this._rollingService.getDm(this._characterService.getIntellect())
      if(mod > modifier){
        modifier = mod;
      }
    } else if(this.assigment.Survival.characteristic.includes('EDU')){
      let mod = this._rollingService.getDm(this._characterService.getEducation())
      if(mod > modifier){
        modifier = mod;
      }
    } else if(this.assigment.Survival.characteristic.includes('SOC')){
      let mod = this._rollingService.getDm(this._characterService.getSocialStatus())
      if(mod > modifier){
        modifier = mod;
      }
    }
    return modifier;
  }

  submit() {
    if(this.qualificationRoll + this.getModifier() >= this.career.Qualification.target){
      this._metadataService.setCurrentUrl('character-creator/careers/event');
    } else {
      this._metadataService.setCurrentUrl('character-creator/careers/mishap');
    }
  }
}
