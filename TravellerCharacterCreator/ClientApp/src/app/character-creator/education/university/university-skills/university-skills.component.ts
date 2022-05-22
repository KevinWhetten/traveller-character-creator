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
  private universitySkills: string[] = [this._skillService.SkillNames.Admin, this._skillService.SkillNames.Advocate,
    this._skillService.SkillNames.Animals, this._skillService.SkillNames.AnimalsTraining, this._skillService.SkillNames.AnimalsVeterinary,
    this._skillService.SkillNames.Art, this._skillService.SkillNames.ArtPerformer, this._skillService.SkillNames.ArtHolography,
    this._skillService.SkillNames.ArtInstrument, this._skillService.SkillNames.ArtVisualMedia, this._skillService.SkillNames.ArtWrite,
    this._skillService.SkillNames.Astrogation, this._skillService.SkillNames.Electronics, this._skillService.SkillNames.ElectronicsComms,
    this._skillService.SkillNames.ElectronicsComputers, this._skillService.SkillNames.ElectronicsRemoteOps,
    this._skillService.SkillNames.ElectronicsSensors, this._skillService.SkillNames.Engineer, this._skillService.SkillNames.EngineerMDrive,
    this._skillService.SkillNames.EngineerJDrive, this._skillService.SkillNames.EngineerLifeSupport, this._skillService.SkillNames.EngineerPower,
    this._skillService.SkillNames.Language, this._skillService.SkillNames.Language1, this._skillService.SkillNames.Language2,
    this._skillService.SkillNames.Language3, this._skillService.SkillNames.Language4, this._skillService.SkillNames.Language5,
    this._skillService.SkillNames.Medic, this._skillService.SkillNames.Navigation, this._skillService.SkillNames.Profession,
    this._skillService.SkillNames.Profession1, this._skillService.SkillNames.Profession2, this._skillService.SkillNames.Profession3,
    this._skillService.SkillNames.Profession4, this._skillService.SkillNames.Profession5, this._skillService.SkillNames.Science,
    this._skillService.SkillNames.ScienceArchaeology, this._skillService.SkillNames.ScienceAstronomy, this._skillService.SkillNames.ScienceBiology,
    this._skillService.SkillNames.ScienceChemistry, this._skillService.SkillNames.ScienceCosmology, this._skillService.SkillNames.ScienceCybernetics,
    this._skillService.SkillNames.ScienceEconomics, this._skillService.SkillNames.ScienceGenetics, this._skillService.SkillNames.ScienceHistory,
    this._skillService.SkillNames.ScienceLinguistics, this._skillService.SkillNames.SciencePhilosophy, this._skillService.SkillNames.SciencePhysics,
    this._skillService.SkillNames.SciencePlanetology, this._skillService.SkillNames.SciencePsionicology, this._skillService.SkillNames.SciencePsychology,
    this._skillService.SkillNames.ScienceRobotics, this._skillService.SkillNames.ScienceSophontology, this._skillService.SkillNames.ScienceXenology];

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
