import {Injectable} from '@angular/core';
import {Router} from "@angular/router";
import {CharacterSkill} from "../../models/character-skill";

interface Metadata {
  currentUrl: string;
  term: number;
  universitySkills: CharacterSkill[];
  skillsLearnedThisTerm: string[];
  eventChoice: number;
  jailed: boolean;
  universityTerm: number;
  graduatedUniversity: boolean;
  graduatedWithHonors: boolean;
  militaryAcademy: string;
  graduatedMilitaryAcademy: boolean;
  militaryAcademyTerm: number;
  careers: string[];
}

@Injectable({
  providedIn: 'root'
})
export class CharacterMetadataService {
  private metadata: Metadata;

  constructor(private _router: Router) {
    this.metadata = {
      currentUrl: 'character-creator/basic-info',
      term: 0,
      universitySkills: [],
      skillsLearnedThisTerm: [],
      eventChoice: 0,
      jailed: false,
      graduatedUniversity: false,
      graduatedWithHonors: false,
      universityTerm: 0,
      militaryAcademy: '',
      graduatedMilitaryAcademy: false,
      militaryAcademyTerm: 0,
      careers: []
    }
    this.save()
  }

  //region Current URL
  setCurrentUrl(url: string) {
    this.load();
    this.metadata.currentUrl = url;
    this._router.navigate([url]);
    this.save();
  }

  getCurrentUrl() {
    this.load();
    if (this.metadata.currentUrl) {
      return this.metadata.currentUrl;
    }
    return 'character-creator/basic-info';
  }

  //endregion

  //region Terms
  startNewTerm() {
    this.load();
    if (!this.metadata.term) {
      this.metadata.term = 0;
    }
    this.metadata.term++;
    this.metadata.skillsLearnedThisTerm = [];
    this.save();
  }

  getTerm() {
    this.load();
    return this.metadata.term;
  }

  getSkillsLearnedThisTerm() {
    this.load();
    return this.metadata.skillsLearnedThisTerm;
  }

  addSkillThisTerm(skill: string) {
    this.load();
    if (!this.metadata.skillsLearnedThisTerm) {
      this.metadata.skillsLearnedThisTerm = [];
    }
    this.metadata.skillsLearnedThisTerm.push(skill);
    this.save();
  }

  //endregion

  //region Careers
  getCareers() {
    this.load();
    return this.metadata.careers;
  }

  addCareer(Name: string) {
    this.load();
    this.metadata.careers.push(Name);
    this.save();
  }

  //endregion

  //region University
  getUniversitySkills() {
    this.load();
    return this.metadata.universitySkills;
  }

  setUniversitySkills(selectedSkills: CharacterSkill[]) {
    this.load();
    let skills = [] as CharacterSkill[];
    for (let selectedSkill of selectedSkills) {
      skills.push({Name: selectedSkill.Name, Value: 1});
    }
    this.metadata.universitySkills = skills;
    this.save();
  }

  graduatedUniversity() {
    this.load();
    this.metadata.universityTerm = this.metadata.term;
    this.metadata.graduatedUniversity = true;
    this.save();
  }

  graduatedUniversityWithHonors() {
    this.load();
    this.graduatedUniversity();
    this.metadata.graduatedWithHonors = true;
    this.save();
  }

  //endregion

  //region Military Academy
  setMilitaryAcademy(academy: string) {
    this.load();
    this.metadata.militaryAcademy = academy;
    this.save();
  }

  getMilitaryAcademy() {
    this.load();
    return this.metadata.militaryAcademy;
  }

  graduatedMilitaryAcademy() {
    this.load();
    this.metadata.militaryAcademyTerm = this.metadata.term;
    this.metadata.graduatedMilitaryAcademy = true;
    this.save();
  }

  graduatedMilitaryAcademyWithHonors() {
    this.load();
    this.graduatedMilitaryAcademy();
    this.metadata.graduatedWithHonors = true;
    this.save();
  }

  //endregion

  //region Events
  setEventNumber(num: number) {
    this.load();
    this.metadata.eventChoice = num;
    this.save();
  }

  getEventNumber() {
    this.load();
    return this.metadata.eventChoice;
  }

  setJailed(jailed: boolean) {
    this.load();
    this.metadata.jailed = jailed;
    this.save();
  }

  isJailed() {
    this.load();
    return this.metadata.jailed;
  }

  //endregion

  //region Save/Load
  private save() {
    localStorage.setItem('metadata', JSON.stringify(this.metadata));
  }

  private load() {
    this.metadata = JSON.parse(localStorage.getItem('metadata') || '{}');
  }

  //endregion
}
