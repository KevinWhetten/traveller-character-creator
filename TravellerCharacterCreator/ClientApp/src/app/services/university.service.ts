import {Injectable} from '@angular/core';
import {Skill} from "../models/skill";

@Injectable({
  providedIn: 'root'
})
export class UniversityService {

  constructor() {
  }

  getMajorSkill(): Skill {
    return {} as Skill;
  }

  getMinorSkill(): Skill {
    return {} as Skill;
  }
}
