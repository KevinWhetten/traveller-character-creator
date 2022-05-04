import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {SkillService} from "../../services/data-services/skill.service";
import {CharacterService} from "../../services/character.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-skill-select',
  templateUrl: './skill-select.component.html',
  styleUrls: ['./skill-select.component.scss']
})
export class SkillSelectComponent implements OnInit {
  @Input() groups: Record<string, string[]> = {};
  @Input() label: string = 'Choose one';
  @Input() type: string = 'full';
  @Input() targetSkillLevel: number = 50;
  @Output() onChange = new EventEmitter<string>();
  groupNames: string[];
  selectedSkill: string;
  characterSkills = this._characterService.getSkills();
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
              private readonly _skillService: SkillService) {
  }

  ngOnInit(): void {
    if (this.groups) {
      if (this.type == 'basic') {
        this.populateBasicSkills();
      }
      if (this.type == 'basic-university') {
        this.populateBasicUniversitySkills();
      }
      if (this.type == 'full') {
        this.populateFullSkills();
      }
      if (this.type == 'full-university') {
        this.populateFullUniversitySkills();
      }
      if (this.type == 'this-term') {
        this.populateThisTermSkills();
      }
    }
  }

  change() {
    this.onChange.emit(this.selectedSkill);
  }

  private populateFullSkills() {
    this.groups = this._skillService.getGroups(this._skillService.skills.map(x => x.Name));
    this.groupNames = this._skillService.getGroupNames(this._skillService.skills.map(x => x.Name));
    this.removeJackOfAllTrades();
  }

  private populateBasicSkills() {
    this.groups = this._skillService.getBasicGroups(this._skillService.skills.map(x => x.Name));
    this.groupNames = ['Basic Skills'];
    this.removeJackOfAllTradesFromBasicList();
  }

  private populateFullUniversitySkills() {
    this.groups = this._skillService.getGroups(this.universitySkills);
    this.groupNames = this._skillService.getGroupNames(this.universitySkills);
  }

  private populateBasicUniversitySkills() {
    this.groups = this._skillService.getBasicGroups(this.universitySkills);
    this.groupNames = ['Basic Skills'];
  }

  private populateThisTermSkills() {
    let skillsLearnedThisTerm = this._characterMetadataService.getSkillsLearnedThisTerm();
    this.groups = this._skillService.getGroups(skillsLearnedThisTerm);
    this.groupNames = this._skillService.getGroupNames(skillsLearnedThisTerm);
  }

  private removeJackOfAllTrades() {
    if (this.groups[this._skillService.SkillNames.JackOfAllTrades]) {
      this.groups[this._skillService.SkillNames.JackOfAllTrades] = [];
    }
  }

  private removeJackOfAllTradesFromBasicList() {
    if (this.groups['Basic Skills'].indexOf(this._skillService.SkillNames.JackOfAllTrades) >= 0) {
      this.groups['Basic Skills'].splice(this.groups['Basic Skills'].indexOf(this._skillService.SkillNames.JackOfAllTrades), 1)
    }
  }

  getSkillDescription(skill: string) {
    return this._skillService.getSkill(skill).Description;
  }

  getAvailableSkills(skills: string[]) {
    return skills.filter(x => this.characterSkills[x] == undefined || this.characterSkills[x] < this.targetSkillLevel)
  }
}
