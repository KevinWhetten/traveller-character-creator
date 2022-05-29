import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {Router} from "@angular/router";
import {SkillService} from "../../../../services/data-services/skill.service";
import {CharacterSkill} from "../../../../models/character-skill";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {AlertType} from "../../../../controls/alert/alert.component";

@Component({
  selector: 'app-accepted-university',
  templateUrl: './university-skills.component.html',
  styleUrls: ['./university-skills.component.css']
})
export class UniversitySkillsComponent implements OnInit {
  private level1Skill: string = '';
  private level0Skill: string = '';
  hasError: boolean = false;
  errorMessage: string;
  private universitySkills: string[] = [this._skillService.SkillName.Admin, this._skillService.SkillName.Advocate,
    this._skillService.SkillName.Animals, this._skillService.SkillName.AnimalsTraining, this._skillService.SkillName.AnimalsVeterinary,
    this._skillService.SkillName.Art, this._skillService.SkillName.ArtPerformer, this._skillService.SkillName.ArtHolography,
    this._skillService.SkillName.ArtInstrument, this._skillService.SkillName.ArtVisualMedia, this._skillService.SkillName.ArtWrite,
    this._skillService.SkillName.Astrogation, this._skillService.SkillName.Electronics, this._skillService.SkillName.ElectronicsComms,
    this._skillService.SkillName.ElectronicsComputers, this._skillService.SkillName.ElectronicsRemoteOps,
    this._skillService.SkillName.ElectronicsSensors, this._skillService.SkillName.Engineer, this._skillService.SkillName.EngineerMDrive,
    this._skillService.SkillName.EngineerJDrive, this._skillService.SkillName.EngineerLifeSupport, this._skillService.SkillName.EngineerPower,
    this._skillService.SkillName.Language, this._skillService.SkillName.Language1, this._skillService.SkillName.Language2,
    this._skillService.SkillName.Language3, this._skillService.SkillName.Language4, this._skillService.SkillName.Language5,
    this._skillService.SkillName.Medic, this._skillService.SkillName.Navigation, this._skillService.SkillName.Profession,
    this._skillService.SkillName.Profession1, this._skillService.SkillName.Profession2, this._skillService.SkillName.Profession3,
    this._skillService.SkillName.Profession4, this._skillService.SkillName.Profession5, this._skillService.SkillName.Science,
    this._skillService.SkillName.ScienceArchaeology, this._skillService.SkillName.ScienceAstronomy, this._skillService.SkillName.ScienceBiology,
    this._skillService.SkillName.ScienceChemistry, this._skillService.SkillName.ScienceCosmology, this._skillService.SkillName.ScienceCybernetics,
    this._skillService.SkillName.ScienceEconomics, this._skillService.SkillName.ScienceGenetics, this._skillService.SkillName.ScienceHistory,
    this._skillService.SkillName.ScienceLinguistics, this._skillService.SkillName.SciencePhilosophy, this._skillService.SkillName.SciencePhysics,
    this._skillService.SkillName.SciencePlanetology, this._skillService.SkillName.SciencePsionicology, this._skillService.SkillName.SciencePsychology,
    this._skillService.SkillName.ScienceRobotics, this._skillService.SkillName.ScienceSophontology, this._skillService.SkillName.ScienceXenology];

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  level0SkillChanged(skill: string) {
    this.level0Skill = skill;
  }

  level1SkillChanged(skill: string) {
    this.level1Skill = skill;
  }

  saveSkills() {
    if (this.level0Skill && this.level1Skill) {
      let selectedSkills = [{Name: this.level0Skill, Value: 0}, {Name: this.level1Skill, Value: 1}] as CharacterSkill[];
      this._characterService.addSkills(selectedSkills);
      this._characterMetadataService.setUniversitySkills(selectedSkills);
      this._characterMetadataService.setCurrentUrl('character-creator/education/university/event');
    } else {
      this.hasError = true;
      this.errorMessage = 'Please select skills.';
    }
  }

  getBasicUniversitySkillGroups() {
    return this._skillService.getBasicGroups(this.universitySkills);
  }

  getFullUniversitySkillGroups() {
    return this._skillService.getGroups(this.universitySkills);
  }

  getFullUniversityGroupNames() {
    return this._skillService.getGroupNames(this.universitySkills);
  }

  getError() {
    return AlertType.Error;
  }
}
