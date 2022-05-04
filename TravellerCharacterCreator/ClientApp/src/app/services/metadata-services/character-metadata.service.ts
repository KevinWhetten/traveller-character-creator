import {Injectable} from '@angular/core';
import {Router} from "@angular/router";
import {CharacterSkill} from "../../models/character-skill";

interface Metadata {
  currentUrl: string;
  term: number;
  universitySkills: CharacterSkill[];
  skillsLearnedThisTerm: string[];
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
      skillsLearnedThisTerm: []
    }
  }

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

  private save() {
    localStorage.setItem('metadata', JSON.stringify(this.metadata));
  }

  private load() {
    this.metadata = JSON.parse(localStorage.getItem('metadata') || '{}');
  }

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
}
