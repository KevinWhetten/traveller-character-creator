import {Injectable} from '@angular/core';
import {Router} from "@angular/router";
import {CharacterSkill} from "../../models/character-skill";

class Metadata {
  // Page Metadata
  currentUrl: string = '';
  // Character Metadata
  term: number = 0;
  // Education
  universitySkills: CharacterSkill[] = [];
  educationTerm: number = 0;
  graduatedUniversity: boolean = false;
  graduatedWithHonors: boolean = false;
  militaryAcademy: string = '';
  graduatedMilitaryAcademy: boolean = false;
  militaryAcademyTerm: number = 0;
  // Skills
  skillsLearnedThisTerm: string[] = [];
  // Events
  eventChoice: number = 0;
  // Mishaps
  mishapNumber: number = 0;
  // Careers
  qualificationDm: number = 0;
  advancementDm: number = 0;
  careers: string[] = [];
  currentCareer: string = '';
  currentCareerTerms: number = 0;
  currentAssignment: string = '';
  promotedThisTerm: boolean = false;
  careerRank: number = 0;
  jailed: boolean = false;
  benefitBonuses: number[];
  // Benefits
  cashRolls: number = 3;
  lostBenefits: number = 0;
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
  getQualificationBonus(career: string): number {
    this.load()
    let bonus = 0;
    if (this.metadata.graduatedUniversity && this.metadata.graduatedWithHonors) {
      if (career == 'Agent' || career == 'Army' || career == 'Citizen' || career == 'Entertainer' || career == 'Marine'
        || career == 'Navy' || career == 'Scholar' || career == 'Scout') {
        if (this.metadata.graduatedWithHonors) {
          bonus += 2;
        } else {
          bonus += 1;
        }
      }
    }
    if (this.metadata.graduatedMilitaryAcademy) {
      if (career == this.metadata.militaryAcademy) {
        bonus += 12;
      }
    }
    if (this.metadata.qualificationDm) {
      bonus += this.metadata.qualificationDm;
    }
    return bonus;
  }

  getAdvancementBonus() {
    this.load();
    return this.metadata.advancementDm;
  }

  setAdvancementBonus(bonus: number) {
    this.load();
    this.metadata.advancementDm = bonus;
    this.save();
  }

  getCommissionBonus(career: string) {
    this.load();
    let bonus = 0;

    if (this.metadata.term == this.metadata.educationTerm + 1) {
      if (this.metadata.graduatedUniversity && this.metadata.graduatedWithHonors) {
        bonus += 2;
      }

      if (this.metadata.graduatedMilitaryAcademy && career == this.metadata.militaryAcademy) {
        if (this.metadata.graduatedWithHonors) {
          bonus += 12;
        } else {
          bonus += 2;
        }
      }
    }

    return bonus;
  }

  getCareers() {
    this.load();
    if (this.metadata.careers)
      return this.metadata.careers;
    else return [];
  }

  addCareer(Name: string) {
    this.load();
    if (this.metadata.careers) {
      this.metadata.careers.push(Name);
    } else {
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

  getAssignment() {
    this.load();
    return this.metadata.currentAssignment;
  }

  promote() {
    this.load();
    if (this.metadata.careerRank) {
      this.metadata.careerRank++;
    } else {
      this.metadata.careerRank = 1;
    }
    this.metadata.promotedThisTerm = true;
    this.save();
  }

  getPromotedThisTerm() {
    return this.metadata.promotedThisTerm;
  }

  getRank() {
    this.load();
    return this.metadata.careerRank;
  }

  getCurrentCareerTerms() {
    this.load();
    if (this.metadata.currentCareerTerms) {
      return this.metadata.currentCareerTerms;
    }
    return 1;
  }

  getCashRolls() {
    this.load();
    return this.metadata.cashRolls ? this.metadata.cashRolls : 3;
  }

  loseBenefits(number: number) {
    this.load();
    if (this.metadata.lostBenefits) {
      this.metadata.lostBenefits += number;
    } else {
      this.metadata.lostBenefits = number;
    }
    this.save();
  }

  setQualificationDm(number: number) {
    this.load();
    if (!this.metadata.qualificationDm || this.metadata.qualificationDm < number) {
      this.metadata.qualificationDm = number;
    }
    this.save();
  }

  addBenefitBonus(number: number) {
    this.load();
    if (this.metadata.benefitBonuses) {
      this.metadata.benefitBonuses.push(number);
    } else {
      this.metadata.benefitBonuses = [number];
    }
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

  graduateUniversity() {
    this.load();
    this.metadata.educationTerm = this.metadata.term;
    this.metadata.graduatedUniversity = true;
    this.save();
  }

  graduatedUniversity() {
    this.load();
    return this.metadata.graduatedUniversity;
  }

  graduateUniversityWithHonors() {
    this.load();
    this.graduateUniversity();
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

  graduateMilitaryAcademy() {
    this.load();
    this.metadata.militaryAcademyTerm = this.metadata.term;
    this.metadata.graduatedMilitaryAcademy = true;
    this.save();
  }

  graduatedMilitaryAcademy() {
    this.load();
    return this.metadata.graduatedMilitaryAcademy;
  }

  graduateMilitaryAcademyWithHonors() {
    this.load();
    this.graduateMilitaryAcademy();
    this.metadata.graduatedWithHonors = true;
    this.save();
  }

  graduatedWithHonors() {
    this.load();
    return this.metadata.graduatedWithHonors;
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

  setMishapNumber(choice: number) {
    this.load();
    this.metadata.mishapNumber = choice;
    this.save();
  }

  getMishapNumber() {
    this.load();
    return this.metadata.mishapNumber;
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
