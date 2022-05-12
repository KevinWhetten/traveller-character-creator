import {Injectable} from '@angular/core';
import {Router} from "@angular/router";
import {CharacterSkill} from "../../models/character-skill";

class Metadata {
  currentUrl: string = '';
  term: number = 0;
  universitySkills: CharacterSkill[] = [];
  skillsLearnedThisTerm: string[] = [];
  eventChoice: number = 0;
  jailed: boolean = false;
  universityTerm: number = 0;
  graduatedUniversity: boolean = false;
  graduatedWithHonors: boolean = false;
  militaryAcademy: string = '';
  graduatedMilitaryAcademy: boolean = false;
  militaryAcademyTerm: number = 0;
  careers: string[] = [];
  currentCareer: string = '';
  currentAssignment: string = '';
}

@Injectable({
  providedIn: 'root'
})
export class CharacterMetadataService {
  private metadata: Metadata = new Metadata();

  constructor(private _router: Router) {
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
    if (this.metadata.careers)
      return this.metadata.careers;
    else return [];
  }

  addCareer(Name: string) {
    this.load();
    if(this.metadata.careers) {
      this.metadata.careers.push(Name);
    }else {
      this.metadata.careers = [Name];
    }
    this.save();
  }

  setCurrentCareer(careerName: string) {
    this.load();
    this.metadata.currentCareer = careerName;
    this.save();
  }

  getCurrentCareer() {
    this.load();
    return this.metadata.currentCareer;
  }

  setAssignment(assignmentName: string) {
    this.load();
    this.metadata.currentAssignment = assignmentName;
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
